using DesafioStone.Dominio.Entidades;
using DesafioStone.Infra.Repositorios;
using MongoDB.Bson;
using Xunit;
using System.Linq;
using DesafioStone.Dominio.ObjectosValor;

namespace DesafioStone.Infra.Teste.Repositorios
{
    public class ComputadorRepositorioTestes
    {
        // Testar adição de um novo computador
        [Fact]
        public void ComputadorRepositorio_CadastrarComputador_ConsultarParaValidar()
        {
            // Arrange
            var computador = new Computador("C001", "A01");

            // Act
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);

            // Assert
            Assert.Equal(computador.Id, id);
            Assert.Equal(true, new ComputadorRepositorio().Buscar(id).Ativo);
        }

        // Testar desativação de um computador
        [Fact]
        public void ComputadorRepositorio_DesativarRegistro_AtivoDeveEstarFalso()
        {
            // Arrange
            var computador = new Computador("C010", "A02");
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);

            // Act
            new ComputadorRepositorio().Desativar(computador);

            // Assert
            Assert.Equal(false, new ComputadorRepositorio().Buscar(id).Ativo);
        }

        // Testar update de um computador
        [Fact]
        public void ComputadorRepositorio_AtualizarRegistro_ValidarAtualizacao()
        {
            // Arrange
            var computador = new Computador("C012", "A02");
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);
            computador.Descricao = "C013";
            computador.Andar = "A03";
            computador.Ativo = false;

            // Act
            new ComputadorRepositorio().Atualizar(computador);

            // Assert
            var computadorAtualizado = new ComputadorRepositorio().Buscar(computador.Id);
            Assert.Equal("C013", computadorAtualizado.Descricao);
            Assert.Equal("A03", computadorAtualizado.Andar);
            Assert.Equal(false, computadorAtualizado.Ativo);
        }

        // Testar consulta de um unico computador
        [Fact]
        public void ComputadorRepositorio_RealizarConsulta_ValidarRetorno()
        {
            // Arrange
            var computador = new Computador("C012", "A02");
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);

            // Act
            var resultado = new ComputadorRepositorio().Buscar(id);

            // Assert
            Assert.Equal(id, resultado.Id);
        }

        // Testar consulta de todos os computadores
        [Fact]
        public void ComputadorRepositorio_RealizarConsultaDeTudo_ValidarResultado()
        {
            // Arrange
            var computador = new Computador("C012", "A02");
            new ComputadorRepositorio().Adicionar(computador);

            computador = new Computador("C013", "A04");
            new ComputadorRepositorio().Adicionar(computador);

            // Act
            var listaComputadores = new ComputadorRepositorio().BuscarTudo();

            // Assert
            Assert.True(listaComputadores != null);
            Assert.True(listaComputadores.ToList().Count > 0);
        }

        // Testar consulta de todos os computadores em um status específico (liberado/não-liberado)
        [Fact]
        public void ComputadorRepositorio_ConsultarPorStatus_ValidarResultado()
        {
            // Arrange
            var computador1 = new Computador("C001", "A01");
            new ComputadorRepositorio().Adicionar(computador1);

            var computador2 = new Computador("C002", "A01");
            new ComputadorRepositorio().Adicionar(computador2);

            var computador3 = new Computador("C003", "A01");
            computador3.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            new ComputadorRepositorio().Adicionar(computador3);

            var computador4 = new Computador("C004", "A02");
            computador4.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            new ComputadorRepositorio().Adicionar(computador4);

            var computador5 = new Computador("C005", "A02");
            computador5.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            new ComputadorRepositorio().Adicionar(computador5);

            // Act
            var listaComputadoresLiberados = new ComputadorRepositorio().BuscarTodosLiberados();
            var listaComputadoresNaoLiberados = new ComputadorRepositorio().BuscarTodosNaoLiberados();

            // Assert
            Assert.True(listaComputadoresLiberados != null);
            Assert.True(listaComputadoresLiberados.ToList().Count > 2);

            Assert.True(listaComputadoresNaoLiberados != null);
            Assert.True(listaComputadoresNaoLiberados.ToList().Count > 3);
        }

        // Testar consulta de todos os computadores em um andar específico
        [Fact]
        public void ComputadorRepositorio_ConsultarComputadoresEmUmAndar_ValidarResultado()
        {
            // Arrange
            var computador1 = new Computador("C001", "A01");
            new ComputadorRepositorio().Adicionar(computador1);

            var computador2 = new Computador("C002", "A01");
            new ComputadorRepositorio().Adicionar(computador2);

            var computador3 = new Computador("C003", "A01");
            computador3.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            new ComputadorRepositorio().Adicionar(computador3);

            // Act
            var listaComputadores = new ComputadorRepositorio().BuscarTodosPorAndar("A01");

            // Assert
            Assert.True(listaComputadores.ToList().Count > 3);
        }
    }
}