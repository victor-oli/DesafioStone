using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using MongoDB.Driver;
using System.Configuration;
using System.Linq;

namespace DesafioStone.Infra.Repositorios
{
    public class ComputadorRepositorio : IComputadorRepositorio
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<Computador> _computadores;

        public ComputadorRepositorio()
        {
            _client = new MongoClient(ConfigurationManager.AppSettings["MongoDBConn"].ToString());
            _db = _client.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"].ToString());
            _computadores = _db.GetCollection<Computador>("Computador");
        }

        public string Adicionar(Computador computador)
        {
            _computadores.InsertOne(computador);

            return computador.Id.ToString();
        }

        public bool Desativar(Computador computador)
        {
            return false;
        }

        public void Dispose()
        {
            _client = null;
            _db = null;
            _computadores = null;
        }
    }
}