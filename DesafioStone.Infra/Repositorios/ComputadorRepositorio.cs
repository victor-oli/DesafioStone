using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Infra.BancoDados;
using DesafioStone.Infra.DataBaseModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DesafioStone.Infra.Repositorios
{
    public class ComputadorRepositorio : IComputadorRepositorio
    {
        private IMongoCollection<ComputadorDBM> _computadores = new MongoDbContext<ComputadorDBM>().Open("Computador");

        public string Adicionar(Computador computador)
        {
            ComputadorDBM computadorDBM = new ComputadorDBM(computador);

            _computadores.InsertOne(computadorDBM);

            return computadorDBM.Id.ToString();
        }

        public void Atualizar(Computador computador)
        {
            _computadores.UpdateOne(x => x.Id == new ObjectId(computador.Id),
                Builders<ComputadorDBM>.Update
                .Set(x => x.Descricao, computador.Descricao)
                .Set(x => x.Andar, computador.Andar)
                .Set(x => x.Ativo, computador.Ativo)
                .Set(x => x.Ocorrencias, computador.Ocorrencias));
        }

        public Computador Buscar(string id)
        {
            return _computadores.Find(x => x.Id == new ObjectId(id)).FirstOrDefault().ConverterParaComputador();
        }

        public Computador BuscarPorDescricao(string descricao)
        {
            return _computadores.Find(x => x.Descricao == descricao).FirstOrDefault().ConverterParaComputador();
        }

        public IEnumerable<Computador> BuscarTodosLiberados()
        {
            var lista = _computadores.Find(new BsonDocument()).ToEnumerable();
            var listaFiltrada = new List<Computador>();

            foreach (ComputadorDBM item in lista)
            {
                if (item.Ocorrencias[item.Ocorrencias.Count - 1].Liberado == true)
                    listaFiltrada.Add(item.ConverterParaComputador());
            }

            return listaFiltrada;
        }

        public IEnumerable<Computador> BuscarTodosNaoLiberados()
        {
            var lista = _computadores.Find(new BsonDocument()).ToEnumerable();
            var listaFiltrada = new List<Computador>();

            foreach (ComputadorDBM item in lista)
            {
                if (item.Ocorrencias[item.Ocorrencias.Count - 1].Liberado == false)
                    listaFiltrada.Add(item.ConverterParaComputador());
            }

            return listaFiltrada;
        }

        public IEnumerable<Computador> BuscarTodosPorAndar(string andar)
        {
            IEnumerable<ComputadorDBM> lista = _computadores.Find(Builders<ComputadorDBM>.Filter
                .Eq(x => x.Andar, andar))
                .ToEnumerable();

            List<Computador> resultado = new List<Computador>();

            lista.ToList().ForEach(x => resultado.Add(x.ConverterParaComputador()));

            return resultado;
        }

        public IEnumerable<Computador> BuscarTudo()
        {
            IEnumerable<ComputadorDBM> lista = _computadores.Find(new BsonDocument()).ToEnumerable();
            List<Computador> resultado = new List<Computador>();

            lista.ToList().ForEach(x => resultado.Add(x.ConverterParaComputador()));

            return resultado;
        }

        public void Desativar(Computador computador)
        {
            UpdateDefinition<ComputadorDBM> update = Builders<ComputadorDBM>.Update.Set("Ativo", false);

            _computadores.UpdateOne(x => x.Id == new ObjectId(computador.Id), update);
        }

        public void Dispose()
        {
            _computadores = null;
        }
    }
}