using MongoDB.Driver;

namespace RinhaDeBackEnd2023.Repository
{
    public class MongoDBRepository
    {
        private static volatile MongoDBRepository _instanceContext;
        private static readonly object padlock = new object();
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private MongoUrl _mongoUrl;

        public MongoDBRepository()
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
                _mongoUrl = new MongoUrl(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to MongoDB", ex);
            }
        }

        public MongoDBRepository(string dataBase)
        {
            string connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
            ConnectToMongo(connectionString, dataBase);
        }

        public MongoDBRepository(string connectionString, string dataBase)
        {
            ConnectToMongo(connectionString, dataBase);
        }

        public static MongoDBRepository CreateInstance(string connectionString, string database)
        {
            if (_instanceContext == null)
            {
                lock (padlock)
                {
                    if (_instanceContext == null)
                        _instanceContext = new MongoDBRepository(connectionString, database);
                }
            }
            return _instanceContext;
        }

        private void ConnectToMongo(string connectionString, string database)
        {
            try
            {
                _mongoUrl = new MongoUrl(connectionString);
                _mongoClient = new MongoClient(_mongoUrl);
                _mongoDatabase = _mongoClient.GetDatabase(database);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to MongoDB", ex);
            }
        }
    }
}
