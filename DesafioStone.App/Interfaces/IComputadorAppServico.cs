using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using System;

namespace DesafioStone.App.Interfaces
{
    public interface IComputadorAppServico : IDisposable
    {
        string Adicionar(Computador computador);
        void Desativar(DesativarComputadorViewModel computadorVm);
        ConsultaComputadorViewModel Buscar(string id);
    }
}