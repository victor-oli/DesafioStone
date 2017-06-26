using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
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

        public ConsultaComputadorViewModel Buscar(string id)
        {
            var computador = _servico.Buscar(id);
            ConsultaComputadorViewModel resultado = null;

            if (computador != null)
                resultado = ConsultaComputadorViewModel.Fabrica.Gerar(computador);
            else
            {
                resultado = new ConsultaComputadorViewModel(null);
                resultado.ResultadoTransacao = "Computador não existe";
            }

            return resultado;
        }

        public void Desativar(DesativarComputadorViewModel computadorVm)
        {
            var computador = _servico.Buscar(computadorVm.Id);

            if (computador != null)
                _servico.Desativar(computador);
        }

        public void Dispose()
        {
            _servico.Dispose();
        }
    }
}