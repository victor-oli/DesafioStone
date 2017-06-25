using DesafioStone.App.Interfaces;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Servicos;

namespace DesafioStone.App.AppServicos
{
    public class ComputadorAppServico : IComputadorAppServico
    {
        private readonly IComputadorServico _servico;

        public ComputadorAppServico(IComputadorServico servico)
        {
            _servico = servico;
        }

        public string Adicionar(Computador computador)
        {
            return _servico.Adicionar(computador);
        }

        public void Desativar(Computador computador)
        {
            _servico.Desativar(computador);
        }

        public void Dispose()
        {
            _servico.Dispose();
        }
    }
}