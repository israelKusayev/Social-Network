using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using log4net;
using Social_Common.Interfaces.Managers;

namespace SocialBl.Managers
{
    public class AmazonS3Uploader : IAmazonS3Uploader
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string bucketUrl = ConfigurationManager.AppSettings["s3Key"];
        private static readonly string bucketName = "social-network-posts-images";

        public string UploadFile(string image, string guid)
        {
            if (string.IsNullOrWhiteSpace(image)) return null;

            var s3Client = new AmazonS3Client(RegionEndpoint.EUCentral1);
            try
            {
                // get image format
                string format = Regex.Match(image, @"^data:image\/([a-zA-Z]+);").Groups[1].Value;

                //remove metadata from image string64
                string result = Regex.Replace(image, @"^data:image\/[a-zA-Z]+;base64,", String.Empty);
                byte[] bytes = Convert.FromBase64String(result);
                string key = guid + "." + format;

                using (s3Client)
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead,
                        Key = key
                    };
                    using (var ms = new MemoryStream(bytes))
                    {
                        request.InputStream = ms;
                        var res = s3Client.PutObject(request);
                    }
                }
                return bucketUrl + key;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return null;
            }
        }
    }
}
