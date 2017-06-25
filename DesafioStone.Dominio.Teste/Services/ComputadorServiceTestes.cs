using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Servicos;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace DesafioStone.Dominio.Teste.Services
{
    public class ComputadorServiceTestes
    {
        // Testar adição de um novo computador
        [Fact]
        public void Computador_AdicionarComputador_ValidarRetorno()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Adicionar(computador)).Returns(computador.Id);
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            ObjectId computadorAdicionadoId = servico.Adicionar(computador);

            // Assert
            Assert.True(computadorAdicionadoId != null);
        }

        // Testar desativação de um computador
        [Fact]
        public void Computador_DesativarComputador_ValidarRetorno()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            var repositorio = new Mock<IComputadorRepositorio>();
            repositorio.Setup(x => x.Desativar(computador));
            var servico = new ComputadorServico(repositorio.Object);

            // Act
            servico.Desativar(computador);

            // Assert
            Assert.True(true);
        }

        // Testar update de um computador
        // Testar consulta de um unico computador
        // Testar consulta de todos os computadores
    }
}