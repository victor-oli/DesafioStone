using System;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Infra.BancoDados;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioStone.Infra.Repositorios
{
    public class ComputadorRepositorio : IComputadorRepositorio
    {
        private IMongoCollection<Computador> _computadores = new MongoDbContext<Computador>().Open("Computador");

        public ObjectId Adicionar(Computador computador)
        {
            _computadores.InsertOne(computador);

            return computador.Id;
        }

        public void Atualizar(Computador computador)
        {
            _computadores.UpdateOne(x => x.Id == computador.Id,
                Builders<Computador>.Update
                .Set(x => x.Descricao, computador.Descricao)
                .Set(x => x.Andar, computador.Andar)
                .Set(x => x.Ativo, computador.Ativo));
        }

        public Computador Buscar(ObjectId id)
        {
            return _computadores.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Desativar(Computador computador)
        {
            UpdateDefinition<Computador> update = Builders<Computador>.Update.Set("Ativo", false);

            _computadores.UpdateOne(x => x.Id == computador.Id, update);
        }

        public void Dispose()
        {
            _computadores = null;
        }
    }
}