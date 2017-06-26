using DesafioStone.App.ViewModels;
using Xunit;

namespace DesafioStone.App.Testes.ViewModels
{
    public class DesativarComputadorViewModelTestes
    {

        // Validar consistência da viewmodel
        [Fact]
        public void DesativarComputadorViewModel_ValidarDados_RetornoValido()
        {
            // Arrange
            var vm = new DesativarComputadorViewModel("123");

            // Act
            vm.EhValido();

            // Assert
            Assert.True(!string.IsNullOrEmpty(vm.Id.Trim()));
        }
    }
}