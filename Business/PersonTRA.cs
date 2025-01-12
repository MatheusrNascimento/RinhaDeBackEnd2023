using MongoDB.Driver;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository;

namespace RinhaDeBackEnd2023.Business
{
    public class PersonTRA
    {
        public static async Task InsertNewPerson(Person person)
        {
            try
            {
                var client = new MongoClient("mongodb+srv://matheusrodriguesnascimento92:WiCsogaL9pChPpcD@typescriptapi.7z20b.mongodb.net/client");
                var database = client.GetDatabase("RinhaDeBackend2023");

                var personRepository = new MongoRepository<Person>(database, "Person");

                await personRepository.InsertAsync(person);
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
