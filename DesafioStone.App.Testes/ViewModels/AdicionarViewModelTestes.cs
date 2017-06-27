using DesafioStone.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var computador = vm.GerarComputador();

            // Assert
            Assert.NotNull(computador);
            Assert.Equal("C001", computador.Descricao);
            Assert.Equal("A10", computador.Andar);
            Assert.Equal(true, computador.Ativo);
        }
    }
}