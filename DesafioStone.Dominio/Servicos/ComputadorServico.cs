using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using DesafioStone.Dominio.ObjectosValor;
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

        public string Adicionar(Computador computador)
        {
            if (_repositorio.BuscarPorDescricao(computador.Descricao.Trim().ToUpper()) != null)
                throw new ComputadorJaExisteException();

            computador.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.PrimeiraOcorrencia());

            return _repositorio.Adicionar(computador);
        }

        public void Atualizar(Computador computador)
        {
            _repositorio.Atualizar(computador);
        }

        public Computador Buscar(string id)
        {
            var computador = _repositorio.Buscar(id);

            if (computador == null)
                throw new ComputadorNaoExisteException();

            return computador;
        }

        public Computador BuscarPorDescricao(string descricao)
        {
            var computador = _repositorio.BuscarPorDescricao(descricao);

            if (computador == null)
                throw new ComputadorNaoExisteException();

            return computador;
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
            computador.Desativar();
            _repositorio.Desativar(computador);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}