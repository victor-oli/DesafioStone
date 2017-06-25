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
            var vm = new AdicionarViewModel("C001", "A10");

            // Act
            var retorno = vm.EhValido();

            // Assert
            Assert.True(retorno);
            Assert.True(!string.IsNullOrEmpty(vm.Descricao.Trim()));
            Assert.True(!string.IsNullOrEmpty(vm.Andar.Trim()));
        }
    }
}