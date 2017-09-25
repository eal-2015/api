using MongoDB.Driver;


namespace LiveHelper
{
    public class Mongo
    {
        public IMongoCollection<Measurement> ConnectToMeasurement(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<Measurement>(collectionName); // is made if not already there
        }
        public IMongoCollection<Anomalies> ConnectToAnomaly(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://10.190.80.25:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<Anomalies>(collectionName); // is made if not already there
        }
    }
}
