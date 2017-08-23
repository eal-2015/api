using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.API.Controllers
{
    /// <summary>
    /// Base Controller
    /// <para>Remember or inherit for using it functionalities</para>
    /// </summary>
    public abstract class Facade : Controller
    {
        #region Fields

        private string DbUrl = "localhost";
        private string DbName = "test";
        private int DbPort = 27017;
        private MongoClient _client;

        #endregion

        #region Properties

        protected MongoClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new MongoClient(new MongoClientSettings
                    {
                        Server = new MongoServerAddress(DbUrl, DbPort)
                    });
                }

                return _client;
            }
        }

        protected string DatabaseName
        {
            get { return DbName; }
            set { DbName = value; }
        }

        protected string DatabaseUrl
        {
            get { return DbUrl; }
            set { DbUrl = value; }
        }

        protected int DatabasePort
        {
            get { return DbPort; }
            set { DbPort = value; }
        }

        protected MongoDatabaseSettings DatabaseSetttings { get; set; }

        protected bool IsSingular { get; set; }

        #endregion

        #region Core Functions

        protected ObjectId GetObjectId(string id)
        {
            ObjectId oId;
            ObjectId.TryParse(id, out oId);
            return oId;
        }

        protected IMongoDatabase GetDatabase(string name, MongoDatabaseSettings settings = null)
        {
            return Client.GetDatabase(name, settings);
        }

        protected IMongoCollection<T> Collection<T>(MongoCollectionSettings settings = null)
        {
            return Collection<T>(ModelName<T>(), settings);
        }
        protected IMongoCollection<T> Collection<T>(string name, MongoCollectionSettings settings = null)
        {
            var db = Client.GetDatabase(DatabaseName, DatabaseSetttings);
            return db.GetCollection<T>(name, settings);
        }

        protected Task<T> GetModelAsync<T>(Expression<Func<T, bool>> filter, MongoCollectionSettings settings = null)
        {
            var collection = Collection<T>(ModelName<T>(), settings);
            return collection.Find(filter).FirstAsync();
        }

        protected Task<List<T>> GetModelsAsync<T>(MongoCollectionSettings settings = null)
        {
            return GetModelsAsync<T>(ModelName<T>(), settings);
        }

        protected Task<List<T>> GetModelsAsync<T>(Expression<Func<T, bool>> filter, MongoCollectionSettings settings = null, FindOptions options = null)
        {
            var collection = Collection<T>(ModelName<T>(), settings);
            return collection.Find(filter, options).ToListAsync();
        }

        protected Task<List<T>> GetModelsAsync<T>(string collectionName, MongoCollectionSettings settings = null)
        {
            var collection = Collection<T>(collectionName, settings);
            return collection.Find(new BsonDocument()).ToListAsync();
        }

        protected List<T> GetModels<T>(MongoCollectionSettings settings = null)
        {
            return GetModels<T>(ModelName<T>(), settings);
        }

        protected List<T> GetModels<T>(string collectionName, MongoCollectionSettings settings = null)
        {
            var collection = Collection<T>(collectionName, settings);
            return collection.Find(new BsonDocument()).ToList();
        }


        protected Task AddModelAsync<T>(T item, InsertOneOptions options = null)
        {
            var collection = Collection<T>();
            return collection.InsertOneAsync(item, options);
        }

        protected Task AddModelAsync<T>(string collectionName, T item, InsertOneOptions options = null)
        {
            var collection = Collection<T>(collectionName);
            return collection.InsertOneAsync(item, options);
        }

        #endregion

        #region Local Helpers

        private string ModelName<T>()
        {
            var name = typeof(T).Name;
            return !IsSingular ? name + "s" : name;
        }

        #endregion

    }
}
