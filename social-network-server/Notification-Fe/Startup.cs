using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Notification_Bl;
using Notification_Fe.Provider;
using NotificationFe.Hubs;

namespace Notification_Fe
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            EnableJwtToken(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ServerOnly", policy => policy.RequireClaim("IsServer", "True"));
            });

            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, UsersProvider>();
            DependencyInjectionBl.RegisterTypes(services);
        }

        private void EnableJwtToken(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configure JWT Bearer Auth to expect our security key
                options.TokenValidationParameters = GetJwtVlidationParameters();

                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ExtractTokenFromRequest,
                    OnTokenValidated = AddUserIdClaim
                };
            });
        }

        private Task AddUserIdClaim(TokenValidatedContext context)
        {
            JwtSecurityToken token = (JwtSecurityToken)context.SecurityToken;
            string userId = token.Subject;
            var claims = new List<Claim>
                        {
                        new Claim("UserId", userId)
                        };
            var appIdentity = new ClaimsIdentity(claims);
            context.Principal.AddIdentity(appIdentity);
            return Task.CompletedTask;
        }

        private Task ExtractTokenFromRequest(MessageReceivedContext context)
        {
            //allow auth with default bearer header
            string accessToken = context.Request.Headers["Authorization"];
            if (accessToken != null)
                accessToken = accessToken.Split(' ')[1];
            else
            {
                //allow auth with costum header
                accessToken = context.Request.Headers["x-auth-token"];
                if (accessToken == null)
                {
                    //all auth from query
                    accessToken = context.Request.Query["access_token"];
                }
            }

            //validate token isnt empty and request destination
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/NotificationsHub") ||
                path.StartsWithSegments("/api/Notification")))

            {
                // add token to context for validation
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }

        private TokenValidationParameters GetJwtVlidationParameters()
        {
            var keyStr = Configuration.GetValue<string>("key");
            var key = Encoding.UTF8.GetBytes(keyStr);

            return new TokenValidationParameters
            {
                LifetimeValidator = (before, expires, token, param) =>
                {
                    return expires > DateTime.UtcNow;
                },
                AudienceValidator = (auds, token, param) =>
                {
                    return auds.Contains("social network");
                },
                ValidateAudience = true,
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("/NotificationsHub");
            });
            app.UseMvc();
        }
    }
}
