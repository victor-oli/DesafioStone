using DesafioStone.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace DesafioStone.Dominio.Interfaces.Servicos
{
    public interface IComputadorServico : IDisposable
    {
        string Adicionar(Computador computador);
        void Desativar(Computador computador);
        Computador Buscar(string id);
        void Atualizar(Computador computador);
        List<Computador> BuscarTudo();
        List<Computador> BuscarTodosLiberados();
        List<Computador> BuscarTodosNaoLiberados();
        List<Computador> BuscarTodosPorAndar(string andar);
        Computador BuscarPorDescricao(string descricao);
    }
}