using MongoDB.Driver;

namespace DesafioStone.Infra.BancoDados
{
    public class MongoDbContext<T> where T : class
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<T> _collection;

        public IMongoCollection<T> Open(string collectionName)
        {
            _client = new MongoClient(); // ConfigurationManager.AppSettings["MongoDBConn"].ToString()
            _db = _client.GetDatabase("Imobilizados"); // ConfigurationManager.AppSettings["MongoDBName"].ToString()
            _collection = _db.GetCollection<T>(collectionName);

            return _collection;
        }
    }
}