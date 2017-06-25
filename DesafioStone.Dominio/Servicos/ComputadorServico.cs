using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DesafioStone.Dominio.Servicos
{
    public class ComputadorServico : IComputadorServico
    {
        private readonly IComputadorRepositorio _repositorio;

        public ComputadorServico(IComputadorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ObjectId Adicionar(Computador computador)
        {
            return _repositorio.Adicionar(computador);
        }

        public void Atualizar(Computador computador)
        {
            _repositorio.Atualizar(computador);
        }

        public Computador Buscar(ObjectId id)
        {
            return _repositorio.Buscar(id);
        }

        public IEnumerable<Computador> BuscarTodosLiberados()
        {
            return _repositorio.BuscarTodosLiberados();
        }

        public IEnumerable<Computador> BuscarTodosNaoLiberados()
        {
            return _repositorio.BuscarTodosNaoLiberados();
        }

        public IEnumerable<Computador> BuscarTodosPorAndar(string andar)
        {
            return _repositorio.BuscarTodosPorAndar(andar);
        }

        public IEnumerable<Computador> BuscarTudo()
        {
            return _repositorio.BuscarTudo();
        }

        public void Desativar(Computador computador)
        {
            _repositorio.Desativar(computador);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}