﻿using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace DesafioStone.Dominio.Servicos
{
    public class ComputadorServico : IComputadorServico
    {
        private readonly IComputadorRepositorio _repositorio;

        public ComputadorServico(IComputadorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ObjectId Adicionar(Computador computador)
        {
            return _repositorio.Adicionar(computador);
        }

        public void Atualizar(Computador computador)
        {
            _repositorio.Atualizar(computador);
        }

        public Computador Buscar(ObjectId id)
        {
            return _repositorio.Buscar(id);
        }

        public List<Computador> BuscarTodosLiberados()
        {
            return _repositorio.BuscarTodosLiberados().ToList();
        }

        public List<Computador> BuscarTodosNaoLiberados()
        {
            return _repositorio.BuscarTodosNaoLiberados().ToList();
        }

        public List<Computador> BuscarTodosPorAndar(string andar)
        {
            return _repositorio.BuscarTodosPorAndar(andar).ToList();
        }

        public List<Computador> BuscarTudo()
        {
            return _repositorio.BuscarTudo().ToList();
        }

        public void Desativar(Computador computador)
        {
            _repositorio.Desativar(computador);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}