using DesafioStone.Dominio.Entidades;
using System;

namespace DesafioStone.Dominio.Interfaces.Servicos
{
    public interface IComputadorServico : IDisposable
    {
        string Adicionar(Computador computador);
        bool Desativar(Computador computador);
    }
}