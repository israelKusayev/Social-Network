using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SocialDal.Repositories.Neo4j
{
    public abstract class Neo4jBaseRepository
    {
        protected IDriver _driver;
        public Neo4jBaseRepository()
        {
            string neo4jUrl = ConfigurationManager.AppSettings["neo4jUrl"];
            string neo4jUn = ConfigurationManager.AppSettings["neo4jUn"];
            string neo4jPass = ConfigurationManager.AppSettings["neo4jPass"];
            _driver = GraphDatabase.Driver(neo4jUrl, AuthTokens.Basic(neo4jUn, neo4jPass));
        }

        ~Neo4jBaseRepository()
        {
            _driver.Dispose();
        }

        protected IStatementResult Query(string query)
        {
            using (ISession session = _driver.Session())
            {
                return session.Run(query);
            }
        }

        protected void Create(object obj)
        {
            string type = obj.GetType().Name;
            string json = ObjectToJson(obj);

            string query = $"create(p:{type}{json})";
            Query(query);
        }

        protected static List<T> RecordsToList<T>(IEnumerable<IRecord> records) where T : new()
        {
            var list = new List<T>();
            foreach (IRecord record in records)
            {
                var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                list.Add(JsonConvert.DeserializeObject<T>(nodeProps));
            }
            return list;
        }


        protected static List<T> UnestedRecordToList<T>(IEnumerable<IRecord> records) where T : new()
        {
            var list = new List<T>();
            foreach (IRecord record in records)
            {
                var nodeProps = JsonConvert.SerializeObject(record.Values);
                list.Add(JsonConvert.DeserializeObject<T>(nodeProps));
            }
            return list;
        }

        protected static T RecordToObj<T>(IRecord record) where T : new()
        {
            var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
            return JsonConvert.DeserializeObject<T>(nodeProps);
        }

        protected static string ObjectToJson(object obj)
        {
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, obj);
            }
            return stringWriter.ToString();
        }
    }
}
