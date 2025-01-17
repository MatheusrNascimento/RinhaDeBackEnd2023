using MongoDB.Bson;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository;

namespace RinhaDeBackEnd2023.Business
{
    public class PessoaTRA
    {
        private static readonly MongoClient _client  = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"));
        public static async Task InsertNewPerson(Pessoa person)
        {
            try
            {
                var database = _client.GetDatabase("RinhaDeBackend2023");

                var personRepository = new MongoRepository<Pessoa>(database, "Person");

                await personRepository.InsertAsync(person);
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<Pessoa> GetPersonById(string id) 
        {
            try
            {
                var database = _client.GetDatabase("RinhaDeBackend2023");
                var repository = new MongoRepository<Pessoa>(database, "Person");

                Pessoa person = await repository.FindOneAsync(person => person.Id == Guid.Parse(id));

                return person;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
