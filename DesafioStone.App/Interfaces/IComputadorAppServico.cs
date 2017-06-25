using DesafioStone.Dominio.Entidades;
using System;

namespace DesafioStone.App.Interfaces
{
    public interface IComputadorAppServico : IDisposable
    {
        string Adicionar(Computador computador);
    }
}