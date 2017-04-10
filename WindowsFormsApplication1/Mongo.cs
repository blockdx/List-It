using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson.Serialization.Attributes;

public class Mongo
{
	public Mongo()
	{
        var creds = MongoCredential.CreateMongoCRCredential("ListDB", "ListAdmin", "Kimball1");
        //Server settings
        var settings = new MongoClientSettings
        {
            Credentials = new[] { creds },
            Server = new MongoServerAddress("localhost")
        };

        MongoClient mongoClient = new MongoClient(settings);
        var db = mongoClient.GetDatabase("ListDB");
	}
    public class User
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Lists")]
        public List<List> Lists { get; set; }
    }
    public class List
    {
        [BsonElement("List Name")]
        public string ListName { get; set; }
        [BsonElement("List")]
        public string ListData { get; set; }//
    }
}
