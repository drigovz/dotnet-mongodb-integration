using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbIntegration.Domain.Entities;
using MongoDbIntegration.Domain.Interfaces.MongoConfig;
using MongoDbIntegration.Domain.Interfaces.Repository;
using System.Linq.Expressions;

namespace MongoDbIntegration.Infra.Data.Repository
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : BaseEntity
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IMongoDbSettings _settings;

        public BaseRepository(IMongoDbSettings settings)
        {
            _settings = settings;

            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(BaseRepository<TDocument>.GetCollectionName(typeof(TDocument)));
        }

        private protected static string GetCollectionName(Type documentType) =>
            ((BsonCollectionAttribute)documentType
              .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
              .FirstOrDefault()
            )?.CollectionName;

        public virtual IQueryable<TDocument> AsQueryable() =>
            _collection.AsQueryable();

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public Task? InsertOneAsync(TDocument document)
        {
            try
            {
                return Task.Run(() => _collection.InsertOneAsync(document));
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}
