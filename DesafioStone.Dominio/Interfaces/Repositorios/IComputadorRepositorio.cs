using DesafioStone.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace DesafioStone.Dominio.Interfaces.Repositorios
{
    public interface IComputadorRepositorio : IDisposable
    {
        string Adicionar(Computador computador);
        void Desativar(Computador computador);
        Computador Buscar(string id);
        void Atualizar(Computador computador);
        IEnumerable<Computador> BuscarTudo();
        IEnumerable<Computador> BuscarTodosLiberados();
        IEnumerable<Computador> BuscarTodosNaoLiberados();
        IEnumerable<Computador> BuscarTodosPorAndar(string andar);
        Computador BuscarPorDescricao(string descricao);
    }
}