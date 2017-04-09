using System;

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

        //Get a reference to the Client Object
        mongoClient = new MongoClient(settings);
        mongoServer = mongoClient.GetServer();
	}
    public class User
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("Lists")]
        public List<List> Lists { get; set; }
    }
    public class List
    {
        [BsonElement("List Name")]
        public string ListName { get; set; }
        [BsonElement("technology")]
        public string List { get; set; }
    }
}
