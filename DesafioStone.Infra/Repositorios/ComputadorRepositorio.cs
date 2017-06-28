using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.ObjectosValor;
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
            ObjectId oid = new ObjectId();

            if (!ObjectId.TryParse(id, out oid))
                return null;

            var dbm = _computadores.Find(x => x.Id == oid).FirstOrDefault();

            return dbm != null ? dbm.ConverterParaComputador() : null;
        }

        public Computador BuscarPorDescricao(string descricao)
        {
            var dbm = _computadores.Find(x => x.Descricao.ToUpper() == descricao.ToUpper()).FirstOrDefault();

            return dbm != null ? dbm.ConverterParaComputador() : null;
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
            List<ComputadorDBM> lista = _computadores.Find(x => x.Andar.ToUpper() == andar.ToUpper())
                .ToList();

            List<Computador> resultado = new List<Computador>();
            
            lista.ForEach(x => resultado.Add(x.ConverterParaComputador()));

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
            UpdateDefinition<ComputadorDBM> update = Builders<ComputadorDBM>.Update
                .Set("Ativo", false)
                .Set(x => x.Ocorrencias, computador.Ocorrencias);

            _computadores.UpdateOne(x => x.Id == new ObjectId(computador.Id), update);
        }

        public void Dispose()
        {
            _computadores = null;
        }
    }
}