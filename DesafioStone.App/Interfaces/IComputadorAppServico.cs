using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace DesafioStone.App.Interfaces
{
    public interface IComputadorAppServico : IDisposable
    {
        string Adicionar(Computador computador);
        void Desativar(DesativarComputadorViewModel computadorVm);
        ConsultarComputadorViewModel Buscar(string id);
        ConsultarComputadorViewModel BuscarPorDescricao(string descricao);
        List<ConsultarTudoViewModel> BuscarTodos();
        List<ConsultarTudoViewModel> BuscarTodosLiberados();
        List<ConsultarTudoViewModel> BuscarTodosNaoLiberados();
        List<ConsultarTudoViewModel> BuscarTodosPorAndar(string andar);
    }
}