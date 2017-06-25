using DesafioStone.Dominio.Entidades;
using System;

namespace DesafioStone.Dominio.Interfaces.Repositorios
{
    public interface IComputadorRepositorio : IDisposable
    {
        string Adicionar(Computador computador);
        bool Desativar(Computador computador);
    }
}