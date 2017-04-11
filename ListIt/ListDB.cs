using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace ListIt
{
    public class ListDB
    {
        protected static IMongoClient client;
        protected static IMongoDatabase db;
        public ListDB()
        {
            var creds = MongoCredential.CreateMongoCRCredential("ListDB", "ListUser", "$l!stdb#");
            //Server settings
            var settings = new MongoClientSettings
            {
                Credentials = new[] { creds },
                Server = new MongoServerAddress("localhost")
            };
            try
            {
                client = new MongoClient(settings);
                db = client.GetDatabase("ListDB");
                db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to database." + e.StackTrace);
                //DOES NOT CATCH ANYTHING, NOT POSSIBLE
            }
        }
        public void connectAndUpdate()
        {

        }
        public class User
        {
            [BsonId]
            public ObjectId ID { get; set; }
            [BsonElement("Name")]
            public string Name { get; set; }
            [BsonElement("Password Hash")]
            public string pwdHash { get; set; }
            [BsonElement("Units")]
            public IList<Unit> Units { get; set; }
        }
        public class Unit
        {
            [BsonElement("Unit Name")]
            public string UnitName { get; set; }
            [BsonElement("Unit Data")]
            public string UnitData { get; set; }
        }
    }
}