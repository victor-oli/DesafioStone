using System;
using System.Collections.Generic;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Infra.BancoDados;
using MongoDB.Bson;
using MongoDB.Driver;
using DesafioStone.Dominio.ObjectosValor;

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

        public IEnumerable<Computador> BuscarTodosLiberados()
        {
            var lista = _computadores.Find(new BsonDocument()).ToEnumerable();
            var listaFiltrada = new List<Computador>();

            foreach (Computador item in lista)
            {
                if (item.Ocorrencias[item.Ocorrencias.Count - 1].Liberado == true)
                    listaFiltrada.Add(item);
            }

            return listaFiltrada;
        }

        public IEnumerable<Computador> BuscarTodosNaoLiberados()
        {
            var lista = _computadores.Find(new BsonDocument()).ToEnumerable();
            var listaFiltrada = new List<Computador>();

            foreach (Computador item in lista)
            {
                if (item.Ocorrencias[item.Ocorrencias.Count - 1].Liberado == false)
                    listaFiltrada.Add(item);
            }

            return listaFiltrada;
        }

        public IEnumerable<Computador> BuscarTodosPorAndar(string andar)
        {
            return _computadores.Find(Builders<Computador>.Filter
                .Eq(x => x.Andar, andar))
                .ToEnumerable();
        }

        public IEnumerable<Computador> BuscarTudo()
        {
            return _computadores.Find(new BsonDocument()).ToEnumerable();
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