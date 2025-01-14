using MongoDB.Bson;
using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository;

namespace RinhaDeBackEnd2023.Business
{
    public class PersonTRA
    {
        private static readonly MongoClient _client  = new MongoClient("mongodb+srv://matheusrodriguesnascimento92:WiCsogaL9pChPpcD@typescriptapi.7z20b.mongodb.net/client");
        public static async Task InsertNewPerson(Person person)
        {
            try
            {
                var database = _client.GetDatabase("RinhaDeBackend2023");

                var personRepository = new MongoRepository<Person>(database, "Person");

                await personRepository.InsertAsync(person);
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<Person> GetPersonById(string id) 
        {
            try
            {
                var database = _client.GetDatabase("RinhaDeBackend2023");
                var repository = new MongoRepository<Person>(database, "Person");

                Person person = await repository.FindOneAsync(person => person.Id == Guid.Parse(id));

                return person;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
