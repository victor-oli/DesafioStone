using DesafioStone.App.ViewModels;
using Xunit;

namespace DesafioStone.App.Testes.ViewModels
{
    public class AdicionarViewModelTestes
    {
        // Validar consistência da viewmodel
        [Fact]
        public void AdicionarViewModel_ValidarConsistencia_RetornoPositivo()
        {
            // Arrange
            var vm = new AdicionarViewModel();
            vm.Descricao = "C001";
            vm.Andar = "A10";

            // Act
            var retorno = vm.EhValido();

            // Assert
            Assert.True(retorno);
            Assert.True(!string.IsNullOrEmpty(vm.Descricao.Trim()));
            Assert.True(!string.IsNullOrEmpty(vm.Andar.Trim()));
        }

        // Validar metodo que devolve um computador pela viewmodel
        [Fact]
        public void AdicionarViewModel_DevolverComputador_ValidadarRetorno()
        {
            // Arrange
            var vm = new AdicionarViewModel();
            vm.Descricao = "C001";
            vm.Andar = "A10";

            // Act
            var computador = vm.RetornarComputador();

            // Assert
            Assert.NotNull(computador);
            Assert.Equal("C001", computador.Descricao);
            Assert.Equal("A10", computador.Andar);
            Assert.Equal(true, computador.Ativo);
        }
    }
}