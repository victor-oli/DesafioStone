using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using MongoDB.Bson;

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