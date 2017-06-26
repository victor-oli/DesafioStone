using DesafioStone.App.AppServicos;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using DesafioStone.Dominio.ObjectosValor;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DesafioStone.App.Testes.Servicos
{
    public class ComputadorAppServiceTestes
    {
        // validar cadastro de um computador
        [Fact]
        public void ComputadorAppService_AdicionarComputador_Validar()
        {
            // Arrange
            var computador = new AdicionarViewModel("C001", "A01").GerarComputador();
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.Adicionar(computador)).Returns("123");
            computador.Id = "123";
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Adicionar(computador)).Returns(computador.Id);

            // Act
            computador.Id = new ComputadorAppServico(servico.Object).Adicionar(computador);

            // Assert
            Assert.NotNull(computador.Id);

        }

        // validar exception ao desativar um computador
        [Fact]
        public void ComputadorAppService_DesativarComputadorNaoLiberado_RetornarException()
        {
            // Arrange
            var vm = new DesativarComputadorViewModel("123");
            var computador = new Computador("C001", "A01");
            computador.Id = vm.Id;
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.Desativar(computador));
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Buscar(computador.Id)).Returns(computador);
            servico.Setup(x => x.Desativar(computador))
                .Throws(new ComputadorEmUsoException("Não é possível desativar um computador em uso!"));
            var appServico = new ComputadorAppServico(servico.Object);

            // Act & Assert
            var ex = Assert.Throws<ComputadorEmUsoException>(() => appServico.Desativar(vm));
            Assert.NotNull(ex);
            Assert.Equal("Não é possível desativar um computador em uso!", ex.Message);
        }

        // validar buscar um computador pelo id
        [Fact]
        public void ComputadorAppService_BuscarComputadorPorId_ValidarRetorno()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            computador.Id = "123";
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.Buscar(computador.Id)).Returns(computador);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Buscar(computador.Id)).Returns(computador);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.Buscar(computador.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(computador.Id, resultado.Id);
            Assert.Equal("OK", resultado.ResultadoTransacao);
            Assert.True(!string.IsNullOrEmpty(resultado.Descricao.Trim()));
            Assert.True(!string.IsNullOrEmpty(resultado.Andar.Trim()));
            Assert.True(resultado.Ocorrencias.Count > 0);
        }

        // validar consulta por id em um computador que não existe
        [Fact]
        public void ComputadorAppService_BuscarComputadorPorId_ComputadorNaoExiste()
        {
            // Arrange
            string id = "123";
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.Buscar(id));
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Buscar(id));
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var computadorVm = appServico.Buscar(id);

            // Assert
            Assert.NotNull(computadorVm);
            Assert.Equal("Computador não existe", computadorVm.ResultadoTransacao);
        }

        // validar buscar um computador por descrição
        [Fact]
        public void ComputadorAppService_BuscarComputadorPorDescricao_RetornoValido()
        {
            // Arrange
            var vm = new ConsultarComputadorViewModel();
            vm.Descricao = "C001";
            var computador = new Computador("C001", "A01");
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.BuscarPorDescricao(vm.Descricao)).Returns(computador);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.BuscarPorDescricao(vm.Descricao)).Returns(computador);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.BuscarPorDescricao(vm.Descricao);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(computador.Descricao, resultado.Descricao);
            Assert.Equal(computador.Andar, resultado.Andar);
            Assert.Equal(computador.Ativo, resultado.Ativo);
            Assert.Equal(computador.Ocorrencias, resultado.Ocorrencias);
            Assert.Equal("OK", resultado.ResultadoTransacao);
        }

        // validar consulta por descrição em um computador que não existe
        [Fact]
        public void ComputadorAppService_BuscarComputadorPorDescricao_ComputadorNaoExiste()
        {
            // Arrange
            string descricao = "C011";
            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.Buscar(descricao));
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Buscar(descricao));
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var computadorVm = appServico.BuscarPorDescricao(descricao);

            // Assert
            Assert.NotNull(computadorVm);
            Assert.Equal("Computador não existe", computadorVm.ResultadoTransacao);
        }

        // validar buscar todos os computadores
        [Fact]
        public void ComputadorAppService_BuscarTodos_RetornoValido()
        {
            // Arrange
            var lista = new List<Computador>();
            lista.Add(new Computador("C001", "A01"));
            lista.Add(new Computador("C002", "A01"));
            lista.Add(new Computador("C003", "A01"));

            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.BuscarTudo()).Returns(lista);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.BuscarTudo()).Returns(lista);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.BuscarTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Count >= 3);
        }

        // validar buscar todos os computadores liberados
        [Fact]
        public void ComputadorAppService_BuscarTodosLiberados_RetornoValido()
        {
            // Arrange
            var lista = new List<Computador>();
            lista.Add(new Computador("C001", "A01"));
            lista.Add(new Computador("C002", "A01"));
            lista.Add(new Computador("C003", "A01"));

            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.BuscarTodosLiberados()).Returns(lista);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.BuscarTodosLiberados()).Returns(lista);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.BuscarTodosLiberados();

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Count >= 3);
        }

        // validar buscar todos os computadores não-liberados
        [Fact]
        public void ComputadorAppService_BuscarTodosNaoLiberados_RetornoValido()
        {
            // Arrange
            var lista = new List<Computador>();
            lista.Add(new Computador("C001", "A01"));
            lista.Add(new Computador("C002", "A01"));
            lista.Add(new Computador("C003", "A01"));

            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.BuscarTodosNaoLiberados()).Returns(lista);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.BuscarTodosNaoLiberados()).Returns(lista);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.BuscarTodosNaoLiberados();

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Count >= 3);
        }

        // validar buscar todos os combutadores por andar
        [Fact]
        public void ComputadorAppService_BuscarTodosPorAndar_RetornoValido()
        {
            // Arrange
            var andar = "A01";
            var lista = new List<Computador>();
            lista.Add(new Computador("C001", andar));
            lista.Add(new Computador("C002", andar));

            var repo = new Mock<IComputadorRepositorio>();
            repo.Setup(x => x.BuscarTodosPorAndar(andar)).Returns(lista);
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.BuscarTodosPorAndar(andar)).Returns(lista);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var resultado = appServico.BuscarTodosPorAndar(andar);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Count == 2);
        }

        // validar informar a utilização de um computador
        // validar informar a liberação de um computador
    }
}