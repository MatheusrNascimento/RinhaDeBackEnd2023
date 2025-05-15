using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.DTOs;
using RinhaDeBackEnd2023.Repository;
using RinhaDeBackEnd2023.Repository.Interfaces;

namespace RinhaDeBackEnd2023.Business
{
    public class PersonTRA
    {
        private readonly IRedisCacheRepository _cacheService;
        private readonly MongoClient _mongoClient;

        public PersonTRA(IRedisCacheRepository redisCacheRepository)
        {
            _cacheService = redisCacheRepository;
            _mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"));
        }

        public async Task InsertNewPerson(Person person)
        {
            var database = _mongoClient.GetDatabase("RinhaDeBackend2023");

            var personRepository = new PersonMongoRepository(database, "Person");

            await personRepository.InsertAsync(person);

            if (await _cacheService.GetAsync<Person>(person.Id.ToString()) is not null)
                return;

            _cacheService.SetAsync(person.Id.ToString(), person, TimeSpan.FromMinutes(1000));
        }

        public async Task<Person> GetPersonById(string id)
        {
            jsonPersonRequest pessoa = await _cacheService.GetAsync<jsonPersonRequest>(id);

            if (pessoa is not null)
                return new Person(pessoa.apelido, pessoa.nome, pessoa.nascimento, pessoa.stack);

            var database = _mongoClient.GetDatabase("RinhaDeBackend2023");
            var repository = new PersonMongoRepository(database, "Person");

            return await repository.FindOneAsync(person => person.Id == Guid.Parse(id));
        }

        public async Task<IEnumerable<Person>> GetPersonByTag(string tag)
        {

            var database = _mongoClient.GetDatabase("RinhaDeBackend2023");
            var repository = new PersonMongoRepository(database, "Person");

            return await repository.FindPersonByTagAsync(tag);
        }
    }
}
