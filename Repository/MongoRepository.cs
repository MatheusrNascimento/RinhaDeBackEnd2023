using System.Linq.Expressions;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Repository.Interfaces;

namespace RinhaDeBackEnd2023.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> filter, T entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter = null)
        {
            filter ??= _ => true;
            return await _collection.Find(filter).ToListAsync();
        }
    }

}