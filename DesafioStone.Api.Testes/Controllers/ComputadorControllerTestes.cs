using DesafioStone.Api.Controllers;
using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioStone.Api.Testes.Controllers
{
    public class ComputadorControllerTestes
    {
        // validar entrada de dados válidos no cadastro
        [Fact]
        public void ComputadorController_ValidarCadastro_EntradaValida()
        {
            // Arrange
            var body = new AdicionarViewModel();
            body.Descricao = "C001";
            body.Andar = "A10";
            var appServico = new Mock<IComputadorAppServico>();

            // Act
            var resultado = body.EhValido();
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.True(resultado);
            Assert.True(!string.IsNullOrEmpty(response.Content.ReadAsAsync<AdicionarViewModel>().Result.Descricao.Trim()));
            Assert.True(!string.IsNullOrEmpty(response.Content.ReadAsAsync<AdicionarViewModel>().Result.Andar.Trim()));
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(body, response.Content.ReadAsAsync<AdicionarViewModel>().Result);
        }

        // validar entrada de dados inválidos no cadastro
        [Fact]
        public void ComputadorController_ValidarCadastro_EntradaInvalida()
        {
            // Arrange
            var body = new AdicionarViewModel();
            body.Descricao = "";
            body.Andar = " ";
            var appServico = new Mock<IComputadorAppServico>();

            // Act
            var resultado = body.EhValido();
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.False(resultado);
            Assert.True(string.IsNullOrEmpty(body.Descricao.Trim()));
            Assert.True(string.IsNullOrEmpty(body.Andar.Trim()));
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }

        // validar entrada com formato inválido no cadastro
        // validar exception no cadastro

        // validar cadastro de um novo computador
        //[Fact]
        //public void ComputadorController_CadastrarComputador_RetornoValido()
        //{
        //    // Arrange
        //    var vm = new AdicionarViewModel("C010", "A10");

        //    // Act

        //    // Assert
        //}

        // validar cadastro de um computador já existente

        // validar entrada de dados válidos ao desativar
        // validar entrada de dados inválidos ao desativar
        // validar desativar computador
        // validar desativar computador em uso

        // validar entrada de dados válidos ao informar utilização
        // validar entrada de dados inválidos ao informar utilização
        // validar informar utilização
        // validar informar utilização de um computador em uso
        // validar informar utilização de um computador desativado
        // validar informar utilização de um computador inexistente

        // validar entrada de dados válidos ao liberar um computador
        // validar entrada de dados inválidos ao liberar um computador
        // validar liberação de um computador
        // validar liberação de um computador inexistente
        // validar liberação de um computador desativado

        // validar consulta de todos os computadores
        // validar consulta de todos os computadores sem resultado

        // validar consulta de todos os computadores livres
        // validar consulta de todos os computadores livres sem resultado

        // validar consulta de todos os computadores ocupados
        // validar consulta de todos os computadores ocupados sem resultado

        // validar entrada de dados válidos em consultar por id
        // validar entrada de dados inválidos em consultar por id
        // validar consulta por id
        // validar consulta por id de um computador inexistente

        // validar entrada de dados válidos em consultar por descrição
        // validar entrada de dados inválidos em consultar por descrição
        // validar consulta por descrição
        // validar consulta por descrição de um computador inexistente

        // validar entrada de dados válidos em consultar por andar
        // validar entrada de dados inválidos em consultar por andar
        // validar consulta por andar
        // validar consulta por andar inexistente
    }
}