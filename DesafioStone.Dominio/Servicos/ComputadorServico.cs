using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;

namespace DesafioStone.Dominio.Servicos
{
    public class ComputadorServico : IComputadorServico
    {
        private readonly IComputadorRepositorio _repositorio;

        public ComputadorServico(IComputadorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public string Adicionar(Computador computador)
        {
            return _repositorio.Adicionar(computador);
        }

        public bool Desativar(Computador computador)
        {
            return _repositorio.Desativar(computador);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}