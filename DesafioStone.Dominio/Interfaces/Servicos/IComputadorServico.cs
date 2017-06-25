using DesafioStone.Dominio.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace DesafioStone.Dominio.Interfaces.Servicos
{
    public interface IComputadorServico : IDisposable
    {
        ObjectId Adicionar(Computador computador);
        void Desativar(Computador computador);
        Computador Buscar(ObjectId id);
        void Atualizar(Computador computador);
        IEnumerable<Computador> BuscarTudo();
        IEnumerable<Computador> BuscarTodosLiberados();
        IEnumerable<Computador> BuscarTodosNaoLiberados();
        IEnumerable<Computador> BuscarTodosPorAndar(string andar);
    }
}