using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using Xunit;

namespace DesafioStone.App.Testes.ViewModels
{
    public class ConsultaComputadorViewModelTestes
    {
        // validar viewmodel de consulta para consultar
        [Fact]
        public void ConsultaComputadorViewModel_ValidarEntrada_ResultadoValido()
        {
            // Arrange
            var vm = new ConsultaComputadorViewModel("123");

            // Act
            vm.EhValido();

            // Assert
            Assert.NotNull(vm);
            Assert.True(!string.IsNullOrEmpty(vm.Id));
            Assert.Equal("123", vm.Id);
        }

        // validar viewmodel preenchida pela consulta bem-sucedida
        [Fact]
        public void NomeDoMetodo()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            computador.Id = "123";

            // Act
            var vm = ConsultaComputadorViewModel.Fabrica.Gerar(computador);

            // Assert
            Assert.NotNull(vm);
            Assert.Equal(computador.Id, vm.Id);
            Assert.Equal(computador.Andar, vm.Andar);
            Assert.Equal(computador.Ativo, vm.Ativo);
            Assert.Equal(computador.Ocorrencias, vm.Ocorrencias);
        }

    }
}