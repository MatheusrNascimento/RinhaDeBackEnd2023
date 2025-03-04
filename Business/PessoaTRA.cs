using MongoDB.Bson;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.DTOs;
using RinhaDeBackEnd2023.Repository;
using RinhaDeBackEnd2023.Repository.Interfaces;

namespace RinhaDeBackEnd2023.Business
{
    public class PessoaTRA
    {
        private readonly IRedisCacheRepository _cacheService;
        private readonly MongoClient _mongoClient;

        public PessoaTRA(IRedisCacheRepository redisCacheRepository)
        {
            _cacheService = redisCacheRepository;
            _mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"));
        }

        public PessoaTRA() {}

        public async Task InsertNewPerson(Pessoa person)
        {
            try
            {
                var database = _mongoClient.GetDatabase("RinhaDeBackend2023");

                var personRepository = new PersonMongoRepository(database, "Person");

                await personRepository.InsertAsync(person);

                if (await _cacheService.GetAsync<Pessoa>(person.Id.ToString()) is not null)
                    return;

                _cacheService.SetAsync(person.Id.ToString(), person, TimeSpan.FromMinutes(1000));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Pessoa> GetPersonById(string id)
        {
            try
            {
                PessoaDTO pessoa = await _cacheService.GetAsync<PessoaDTO>(id);
                
                if (pessoa is not null)
                    return new Pessoa(pessoa.apelido, pessoa.nome, pessoa.nascimento, pessoa.stack);

                var database = _mongoClient.GetDatabase("RinhaDeBackend2023");
                var repository = new PersonMongoRepository(database, "Person");
                
                return await repository.FindOneAsync(person => person.Id == Guid.Parse(id));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Pessoa>> GetPersonByTag(string tag)
        {
            try
            {
                var database = _mongoClient.GetDatabase("RinhaDeBackend2023");
                var repository = new PersonMongoRepository(database, "Person");

                return await repository.FindPersonByTagAsync(tag);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
