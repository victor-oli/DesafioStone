using DesafioStone.App.AppServicos;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using DesafioStone.Dominio.ObjectosValor;
using Moq;
using System;
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
        public void NomeDoMetodo()
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
        // validar buscar todos os computadores
        // validar buscar todos os computadores liberados
        // validar buscar todos os computadores não-liberados
        // validar buscar todos os combutadores por andar
        // validar informar a utilização de um computador
        // validar informar a liberação de um computador
    }
}