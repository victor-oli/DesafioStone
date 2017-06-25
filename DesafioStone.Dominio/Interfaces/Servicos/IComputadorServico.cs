using DesafioStone.Dominio.Entidades;
using MongoDB.Bson;
using System;

namespace DesafioStone.Dominio.Interfaces.Servicos
{
    public interface IComputadorServico : IDisposable
    {
        ObjectId Adicionar(Computador computador);
        void Desativar(Computador computador);
    }
}