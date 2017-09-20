using MongoDB.Driver;

namespace ModelHelper
{
    public class Mongo
    {
        /*
         * How it would be nice if the type of object the collection is could be abstract
         * and then determined when instantiating the class.
         * That would be perfect, but <T> say NO!
         */

        public IMongoCollection<Station> ConnectToStation(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<Station>(collectionName); // is made if not already there
        }

        public IMongoCollection<Measurement> ConnectToMeasurement(string databaseName, string collectionName)
        {
            MongoClient client = new MongoClient("mongodb://10.190.80.25:27017");
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there
            return database.GetCollection<Measurement>(collectionName); // is made if not already there
        }


    }
}
