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
            try
            {
                await _collection.InsertManyAsync(documents);
            }
            catch (Exception ex)
            {
                await Task.FromException<MongoCommandException>(ex);
            }
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            try
            {
                await _collection.ReplaceOneAsync(x => x.Id == document.Id, document);
            }
            catch (Exception ex)
            {
                await Task.FromException<MongoCommandException>(ex);
            }
        }

        public Task<TDocument?> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
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
