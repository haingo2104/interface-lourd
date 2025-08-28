using MongoDB.Driver;
using System.Collections.Generic;
using MadaTransportConnect.Models;
using MadaTransportConnect.Data;

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
    }
}
