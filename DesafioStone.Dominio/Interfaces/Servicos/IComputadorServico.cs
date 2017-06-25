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
        List<Computador> BuscarTudo();
        List<Computador> BuscarTodosLiberados();
        List<Computador> BuscarTodosNaoLiberados();
        List<Computador> BuscarTodosPorAndar(string andar);
    }
}