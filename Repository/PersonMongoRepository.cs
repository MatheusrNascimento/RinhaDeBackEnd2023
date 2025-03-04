using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository.Interfaces;

namespace RinhaDeBackEnd2023.Repository
{
    public class PersonMongoRepository : IRepository<Pessoa>
    {
        private readonly IMongoCollection<Pessoa> _collection;

        public PersonMongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<Pessoa>(collectionName);
        }

        public async Task InsertAsync(Pessoa entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Expression<Func<Pessoa, bool>> filter, Pessoa entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Expression<Func<Pessoa, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<Pessoa> FindOneAsync(Expression<Func<Pessoa, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pessoa>> FindAllAsync(Expression<Func<Pessoa, bool>> filter = null)
        {
            filter ??= _ => true;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Pessoa>> FindPersonByTagAsync(string tag)
        {
            var filtro = Builders<Pessoa>.Filter.Or(
            Builders<Pessoa>.Filter.Regex(u => u.nome, new BsonRegularExpression(tag, "i")),
            Builders<Pessoa>.Filter.Regex(u => u.apelido, new BsonRegularExpression(tag, "i")),
            Builders<Pessoa>.Filter.AnyEq(u => u.stack, tag));

            return await _collection.Find(filtro).ToListAsync();
        }
    }

}