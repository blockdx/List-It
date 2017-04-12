using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson.Serialization.Attributes;
using System.Windows.Forms;

namespace ListIt
{
    public class ListDB
    {
        protected static IMongoClient client;
        protected static IMongoDatabase db;
        public ListDB()
        {
            var creds = MongoCredential.CreateCredential("ListDB", "ListDBAdmin", "phms.listdb");
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
                //db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to database." + e.StackTrace);
                //DOES NOT CATCH ANYTHING, NOT POSSIBLE
            }
            
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
        public static async void newUser(String Name, String pwdHash)
        {
            var document = new BsonDocument
            {
                { "username", Name },
                { "pwdhash", pwdHash }
            };

            var collection = db.GetCollection<BsonDocument>("Users"); //get collection
            await collection.InsertOneAsync(document);
        }
        public static async Task<bool> checkUserExists(String username)
        {
            var collection = db.GetCollection<BsonDocument>("Users");
            var filter = Builders<BsonDocument>.Filter.Eq("username", username);
            using (var cursor = await collection.Find(filter).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static async Task<bool> logIn(String username, String pwdHash)
        {
            var collection = db.GetCollection<BsonDocument>("Users");
            var filter = Builders<BsonDocument>.Filter.Eq("username", username);
            using (var cursor = await collection.Find(filter).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        if (pwdHash == doc[2].ToString())
                        {
                            //password hashes match
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            return false;
        }
        public IList<String> getData()
        {
            IList<String> data = new List<String>();
            return data;
        }
        public static string hashIt(String password)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            var pwdHash = "";
            foreach (byte b in data)
            {
                pwdHash += b.ToString("X2");
            }
            return pwdHash;
        }
    }
}