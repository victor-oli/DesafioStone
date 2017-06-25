using DesafioStone.Dominio.Entidades;
using MongoDB.Bson;
using System;

namespace DesafioStone.Dominio.Interfaces.Repositorios
{
    public interface IComputadorRepositorio : IDisposable
    {
        ObjectId Adicionar(Computador computador);
        void Desativar(Computador computador);
        Computador Buscar(ObjectId id);
    }
}