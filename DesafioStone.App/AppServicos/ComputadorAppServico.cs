using System;
using System.Collections.Generic;
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

        public ConsultarComputadorViewModel Buscar(string id)
        {
            var computador = _servico.Buscar(id);
            ConsultarComputadorViewModel resultado = null;

            if (computador != null)
                resultado = ConsultarComputadorViewModel.Fabrica.Gerar(computador);
            else
            {
                resultado = new ConsultarComputadorViewModel();
                resultado.ResultadoTransacao = "Computador não existe";
            }

            return resultado;
        }

        public ConsultarComputadorViewModel BuscarPorDescricao(string descricao)
        {
            var computador = _servico.BuscarPorDescricao(descricao);
            ConsultarComputadorViewModel resultado = null;

            if (computador != null)
                resultado = ConsultarComputadorViewModel.Fabrica.Gerar(computador);
            else
            {
                resultado = new ConsultarComputadorViewModel();
                resultado.ResultadoTransacao = "Computador não existe";
            }

            return resultado;
        }

        public List<ConsultarTudoViewModel> BuscarTodos()
        {
            var computadores = _servico.BuscarTudo();
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            computadores.ForEach(x => lista.Add(new ConsultarTudoViewModel(x)));

            return lista;
        }

        public List<ConsultarTudoViewModel> BuscarTodosLiberados()
        {
            var computadores = _servico.BuscarTodosLiberados();
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            computadores.ForEach(x => lista.Add(new ConsultarTudoViewModel(x)));

            return lista;
        }

        public List<ConsultarTudoViewModel> BuscarTodosNaoLiberados()
        {
            var computadores = _servico.BuscarTodosNaoLiberados();
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            computadores.ForEach(x => lista.Add(new ConsultarTudoViewModel(x)));

            return lista;
        }

        public List<ConsultarTudoViewModel> BuscarTodosPorAndar(string andar)
        {
            var computadores = _servico.BuscarTodosPorAndar(andar);
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            computadores.ForEach(x => lista.Add(new ConsultarTudoViewModel(x)));

            return lista;
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