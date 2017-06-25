using DesafioStone.App.AppServicos;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Dominio.Interfaces.Servicos;
using Moq;
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

        // validar buscar um computador pelo id
        // validar buscar um computador por descrição
        // validar buscar todos os computadores
        // validar buscar todos os computadores liberados
        // validar buscar todos os computadores não-liberados
        // validar buscar todos os combutadores por andar
        // validar informar a utilização de um computador
        // validar informar a liberação de um computador
    }
}