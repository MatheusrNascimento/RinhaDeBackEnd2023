using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository.Interfaces;

namespace RinhaDeBackEnd2023.Repository
{
    public class PersonMongoRepository : IRepository<Person>
    {
        private readonly IMongoCollection<Person> _collection;

        public PersonMongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<Person>(collectionName);
        }

        public async Task InsertAsync(Person entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Expression<Func<Person, bool>> filter, Person entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Expression<Func<Person, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<Person> FindOneAsync(Expression<Func<Person, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> FindAllAsync(Expression<Func<Person, bool>> filter = null)
        {
            filter ??= _ => true;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Person>> FindPersonByTagAsync(string tag)
        {
            var filtro = Builders<Person>.Filter.Or(
            Builders<Person>.Filter.Regex(u => u.nome, new BsonRegularExpression(tag, "i")),
            Builders<Person>.Filter.Regex(u => u.apelido, new BsonRegularExpression(tag, "i")),
            Builders<Person>.Filter.AnyEq(u => u.stack, tag));

            return await _collection.Find(filtro).ToListAsync();
        }
    }

}