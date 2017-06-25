using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Servicos;
using MongoDB.Bson;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using DesafioStone.Dominio.ObjectosValor;

namespace DesafioStone.Dominio.Teste.Services
{
    public class ComputadorServiceTestes
    {
        // Testar adição de um novo computador
        [Fact]
        public void ComputadorService_AdicionarComputador_ValidarRetorno()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Adicionar(computador)).Returns("123");
            computador.Id = "123";
            repositorio.Setup(x => x.Buscar(computador.Id)).Returns(computador);
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            computador.Id = servico.Adicionar(computador);

            // Assert
            Assert.True(computador.Id != null);
            Assert.True(servico.Buscar(computador.Id) != null);
        }

        // Validar adição de um computador com a mesma descrição
        [Fact]
        public void ComputadorService_AdicionarComMesmaDescricao_RetornarException()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Adicionar(computador)).Throws(new ComputadorJaExisteException());
            var servico = new ComputadorServico(repositorio.Object);

            // Act & Assert
            var ex = Assert.Throws<ComputadorJaExisteException>(() => servico.Adicionar(computador));
            Assert.Equal("Já existe um computador com este nome!", ex.Message);
        }

        // Testar consultar por descrição
        [Fact]
        public void ComputadorService_ConsultarPorDescricao_RetornarComputador()
        {
            // Arrange
            string descricao = "C001";
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.BuscarPorDescricao(descricao)).Returns(new Computador("C001", "A01"));
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            var computador = servico.BuscarPorDescricao(descricao);

            // Assert
            Assert.NotNull(computador);
            Assert.Equal(descricao, computador.Descricao);
        }

        // Testar update de um computador
        [Fact]
        public void ComputadorService_AtualizarUmComputador_ValidarComunicacaoComORepositorio()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Atualizar(computador));
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            servico.Atualizar(computador);
        }

        // Testar consulta de um unico computador
        [Fact]
        public void ComputadorService_ConsultarUmComputador_ValidarResultado()
        {
            // Arrange
            ObjectId id = new ObjectId();
            var computador = new Computador("C100", "A10");
            computador.Id = id.ToString();
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Buscar(id.ToString())).Returns(computador);
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            var resultado = servico.Buscar(id.ToString());

            // Assert
            Assert.True(resultado != null);
            Assert.Equal(id.ToString(), resultado.Id);
            Assert.Equal("C100", resultado.Descricao);
            Assert.Equal("A10", resultado.Andar);
        }

        // Testar consulta de todos os computadores
        [Fact]
        public void ComputadorService_ConsultarLista_ValidarRetorno()
        {
            // Arrange
            var resultado = new List<Computador>
            {
                new Computador("C001", "A01"),
                new Computador("C002", "A01"),
                new Computador("C003", "A01")
            };
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.BuscarTudo()).Returns(resultado);
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            var resultadoConsultado = servico.BuscarTudo();

            // Assert
            Assert.True(resultadoConsultado != null);
            Assert.Equal(3, resultadoConsultado.ToList().Count);
        }

        // Testar consulta de computadores com status de distintos (liberados/não-liberados)
        [Fact]
        public void ComputadorService_ConsultarPorStatus_ValidarResultados()
        {
            // Arrange
            var listaLiberados = new List<Computador>();
            var listaNaoLiberados = new List<Computador>();
            var c1 = new Computador("C001", "A01");
            listaLiberados.Add(c1);
            var c2 = new Computador("C002", "A01");
            listaLiberados.Add(c2);
            var c3 = new Computador("C003", "A01");
            listaLiberados.Add(c3);

            var c4 = new Computador("C004", "A01");
            c4.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            listaNaoLiberados.Add(c4);
            var c5 = new Computador("C005", "A01");
            c5.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
            listaNaoLiberados.Add(c5);

            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.BuscarTodosLiberados()).Returns(listaLiberados);
            repositorio.Setup(x => x.BuscarTodosNaoLiberados()).Returns(listaNaoLiberados);
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            var resultadoLiberados = servico.BuscarTodosLiberados();
            var resultadoNaoLiberados = servico.BuscarTodosNaoLiberados();

            // Assert
            Assert.True(resultadoLiberados != null);
            Assert.Equal(3, resultadoLiberados.ToList().Count);

            Assert.True(resultadoNaoLiberados != null);
            Assert.Equal(2, resultadoNaoLiberados.ToList().Count);
        }

        // Testar consulta de computadores por andar
        [Fact]
        public void ComputadorService_ConsultarPorAndar_ValidarResultado()
        {
            // Arrange
            var lista = new List<Computador>();
            var c1 = new Computador("C001", "A01");
            lista.Add(c1);
            var c2 = new Computador("C002", "A01");
            lista.Add(c2);
            var c3 = new Computador("C003", "A01");
            lista.Add(c3);
            var c4 = new Computador("C004", "A01");
            lista.Add(c4);

            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.BuscarTodosPorAndar("A01")).Returns(lista);
            repositorio.Setup(x => x.BuscarTodosPorAndar("A02")).Returns(new List<Computador>());
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            var listaA01 = servico.BuscarTodosPorAndar("A01");
            var listaA02 = servico.BuscarTodosPorAndar("A02");

            // Assert
            Assert.True(listaA01 != null);
            Assert.Equal(4, listaA01.ToList().Count);

            Assert.True(listaA02 != null);
            Assert.Equal(0, listaA02.ToList().Count);
        }

        // Testar ocorrencia gerada ao desativar um computador
        [Fact]
        public void ComputadorService_DesativarComputador_VerificarOcorrencia()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Desativar(computador));
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            servico.Desativar(computador);

            // Assert
            Assert.Equal("Computador Desativado".ToUpper(), computador.PegarUltimaOcorrencia().Descricao);
            Assert.Equal(false, computador.PegarUltimaOcorrencia().Liberado);
            Assert.True(!computador.Ativo);
        }
    }
}