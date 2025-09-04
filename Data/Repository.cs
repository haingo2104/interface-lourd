using MongoDB.Driver;
using System.Collections.Generic;
using MadaTransportConnect.Models;
using MadaTransportConnect.Data;
using MongoDB.Bson;

namespace MadaTransportConnect.Data
{
    public class Repository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(MongoDbContext context, string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName);
        }

        public void Insert(T entity)
        {
            _collection.InsertOne(entity);
        }

        public List<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }
        public T GetById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return _collection.Find(filter).FirstOrDefault();
        }
        public void Update(string id, T entity)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            _collection.ReplaceOne(filter, entity);
        }

    }
}
