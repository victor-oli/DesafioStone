using DesafioStone.Api.Controllers;
using DesafioStone.App.AppServicos;
using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.Interfaces.Servicos;
using DesafioStone.Dominio.ObjectosValor;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
            body.Andar = "A01";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.Adicionar(body)).Returns("123");

            // Act
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.True(body.EhValido());
            Assert.True(!string.IsNullOrEmpty(body.Descricao.Trim()));
            Assert.True(!string.IsNullOrEmpty(body.Andar.Trim()));
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("123", response.Content.ReadAsStringAsync().Result);
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
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.False(body.EhValido());
            Assert.True(string.IsNullOrEmpty(body.Descricao.Trim()));
            Assert.True(string.IsNullOrEmpty(body.Andar.Trim()));
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal("Não é possível cadastrar um cadastrar um computador com essas informações.", response.Content.ReadAsStringAsync().Result);
        }

        // validar exception no cadastro
        [Fact]
        public void ComputadorController_ValidarCadastro_ExceptionTratada()
        {
            // Arrange
            var body = new AdicionarViewModel();
            body.Andar = "A01";
            body.Descricao = null;
            var appServico = new Mock<IComputadorAppServico>();
            var controller = new ComputadorController(appServico.Object);

            // Act
            var response = controller
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal("Cadastro inválido! Você deve informar Descrição e Andar.", response.Content.ReadAsStringAsync().Result);
        }

        // validar cadastro de um computador já existente
        [Fact]
        public void ComputadorController_CadastrarComputador_ComputadorJaExisteExceptionTratada()
        {
            // Arrange
            var body = new AdicionarViewModel();
            body.Andar = "A01";
            body.Descricao = "C001";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.Adicionar(body)).Throws(new ComputadorJaExisteException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal("Este computador já está cadastrado!", response.Content.ReadAsStringAsync().Result);
        }

        // validar cadastro de um novo computador
        [Fact]
        public void ComputadorController_CadastrarComputador_ComputadorCadastrado()
        {
            // Arrange
            var body = new AdicionarViewModel();
            body.Descricao = "C001";
            body.Andar = "A01";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.Adicionar(body)).Returns("123");

            // Act
            var response = new ComputadorController(appServico.Object)
                .CadastrarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<AdicionarViewModel>(body, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.True(body.EhValido());
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Content.ReadAsStringAsync().Result != null);
            Assert.Equal("123", response.Content.ReadAsStringAsync().Result);
        }
        
        // validar desativar computador
        [Fact]
        public void ComputadorController_DesativarComputador_FoiDesativado()
        {
            // Arrange
            var vm = new DesativarComputadorViewModel("123");
            var computador = new Computador("C001", "A01");
            computador.Id = "123";
            var servico = new Mock<IComputadorServico>();
            servico.Setup(x => x.Buscar(vm.Id)).Returns(computador);
            var appServico = new ComputadorAppServico(servico.Object);

            // Act
            var response = new ComputadorController(appServico)
                .DesativarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<DesativarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                });

            // Assert
            Assert.True(vm.EhValido());
            Assert.NotNull(computador);
            Assert.Equal(vm.Id, computador.Id);
            Assert.True(computador.Ativo);
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal("O computador foi desativado.", response.Content.ReadAsStringAsync().Result);
        }

        // validar desativar computador em uso
        [Fact]
        public void ComputadorController_DesativarComputador_ComputadorJaEstaEmUso()
        {
            // Arrange
            var vm = new DesativarComputadorViewModel("123");
            var appService = new Mock<IComputadorAppServico>();
            appService.Setup(x => x.Desativar(vm))
                .Throws(new ComputadorEmUsoException("O computador que você tentou desativar está em uso."));

            // Act
            var response = new ComputadorController(appService.Object)
                .DesativarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<DesativarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsStringAsync().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal("O computador que você tentou desativar está em uso.", response);
        }
        
        // validar informar utilização
        [Fact]
        public void ComputadorController_InformarUtilizacao_UtilizacaoInformada()
        {
            // Arrange
            var vm = new UtilizarComputadorViewModel();
            vm.Descricao = "C002";
            var appService = new Mock<IComputadorAppServico>();
            vm.Resultado = "Agora este computador está em uso.";
            appService.Setup(x => x.UtilizarComputador(vm)).Returns(vm);

            // Act
            var response = new ComputadorController(appService.Object)
                .UtilizarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<UtilizarComputadorViewModel>().Result;

            // Assert
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("Agora este computador está em uso.", response.Resultado);
        }

        // validar informar utilização de um computador em uso
        [Fact]
        public void ComputadorController_InformarUtilizacao_ComputadorJaEstaEmUso()
        {
            // Arrange
            var vm = new UtilizarComputadorViewModel();
            vm.Descricao = "C001";
            var appService = new Mock<IComputadorAppServico>();
            appService.Setup(x => x.UtilizarComputador(vm))
                .Throws(new ComputadorEmUsoException("Não é possível utilizar um computador que já está em uso."));

            // Act
            var response = new ComputadorController(appService.Object)
                .UtilizarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<UtilizarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("Não é possível utilizar um computador que já está em uso.", response.Resultado);
        }

        // validar informar utilização de um computador desativado
        [Fact]
        public void ComputadorController_InformarUtilizacao_ComputadorDesativado()
        {
            // Arrange
            var vm = new UtilizarComputadorViewModel();
            vm.Descricao = "c001";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.UtilizarComputador(vm)).Throws(new ComputadorDesativadoException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .UtilizarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<UtilizarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("Computador desativado. Não é possível utilizar este computador!", response.Resultado);
        }

        // validar informar utilização de um computador inexistente
        [Fact]
        public void ComputadorController_InformarUtilizacao_ComputadorNaoExiste()
        {
            // Arrange
            var vm = new UtilizarComputadorViewModel();
            vm.Descricao = "c001";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.UtilizarComputador(vm)).Throws(new ComputadorNaoExisteException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .UtilizarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<UtilizarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("O computador desejado não existe!", response.Resultado);
        }

        // validar liberação de um computador
        [Fact]
        public void ComputadorController_LiberarComputador_LiberacaoValida()
        {
            // Arrange
            var vm = new LiberarComputadorViewModel();
            vm.DescricaoComputador = "c001";
            vm.Resultado = string.Format("O computador {0} foi liberado.", vm.DescricaoComputador);
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.LiberarComputador(vm)).Returns(vm);

            // Act
            var response = new ComputadorController(appServico.Object)
                .LiberarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<LiberarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.DescricaoComputador, response.DescricaoComputador);
            Assert.Equal(string.Format("O computador {0} foi liberado.", vm.DescricaoComputador), response.Resultado);
        }

        // validar liberação de um computador inexistente
        [Fact]
        public void ComputadorController_LiberarComputador_ComputadorNaoExiste()
        {
            // Arrange
            var vm = new LiberarComputadorViewModel();
            vm.DescricaoComputador = "c001";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.LiberarComputador(vm)).Throws(new ComputadorNaoExisteException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .LiberarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<LiberarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.DescricaoComputador, response.DescricaoComputador);
            Assert.Equal("O computador desejado não existe!", response.Resultado);
        }

        // validar liberação de um computador desativado
        [Fact]
        public void ComputadorController_LiberarComputador_ComputadorDesativado()
        {
            // Arrange
            var vm = new LiberarComputadorViewModel();
            vm.DescricaoComputador = "c001";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.LiberarComputador(vm)).Throws(new ComputadorDesativadoException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .LiberarComputador(new HttpRequestMessage
                {
                    Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<LiberarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.DescricaoComputador, response.DescricaoComputador);
            Assert.Equal("Computador desativado. Não é possível utilizar este computador!", response.Resultado);
        }

        // validar consulta de todos os computadores
        [Fact]
        public void ComputadorController_ConsultarTudo_RetornoValido()
        {
            // Arrange
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarTodos()).Returns(lista);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarTodos(new HttpRequestMessage())
                .Content.ReadAsAsync<List<ConsultarTudoViewModel>>().Result;

            // Assert
            Assert.NotNull(response);
        }

        // validar consulta de todos os computadores livres
        [Fact]
        public void ComputadorController_ConsultarLiberados_RetornoValido()
        {
            // Arrange
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarTodosLiberados()).Returns(lista);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarLiberados(new HttpRequestMessage())
                .Content.ReadAsAsync<List<ConsultarTudoViewModel>>().Result;

            // Assert
            Assert.NotNull(response);
        }

        // validar consulta de todos os computadores ocupados
        [Fact]
        public void ComputadorController_ConsultarNaoLiberados_RetornoValido()
        {
            // Arrange
            List<ConsultarTudoViewModel> lista = new List<ConsultarTudoViewModel>();

            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarTodosNaoLiberados()).Returns(lista);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarNaoLiberados(new HttpRequestMessage())
                .Content.ReadAsAsync<List<ConsultarTudoViewModel>>().Result;

            // Assert
            Assert.NotNull(response);
        }

        // validar consulta por id
        [Fact]
        public void ComputadorController_ConsultarPorId_RetornoValido()
        {
            // Arrange
            var vm = new ConsultarComputadorViewModel();
            vm.Id = "C003";
            vm.ResultadoTransacao = "OK";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.Buscar(vm.Id)).Returns(vm);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarPorId(new HttpRequestMessage
                {
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Id, response.Id);
            Assert.Equal("OK", response.ResultadoTransacao);
        }

        // validar consulta por id de um computador inexistente
        [Fact]
        public void ComputadorController_ConsultarPorId_ComputadorNaoExiste()
        {
            // Arrange
            var vm = new ConsultarComputadorViewModel();
            vm.Id = "123";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.Buscar(vm.Id)).Throws(new ComputadorNaoExisteException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarPorId(new HttpRequestMessage
                {
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Id, response.Id);
            Assert.Equal("O computador desejado não existe!", response.ResultadoTransacao);
        }

        // validar consulta por descrição
        [Fact]
        public void ComputadorController_ConsultarPorDescricao_RetornoValido()
        {
            // Arrange
            var vm = new ConsultarComputadorViewModel();
            vm.Descricao = "C003";
            vm.ResultadoTransacao = "OK";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarPorDescricao(vm.Descricao)).Returns(vm);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarPorDescricao(new HttpRequestMessage
                {
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("OK", response.ResultadoTransacao);
        }

        // validar consulta por descrição de um computador inexistente
        [Fact]
        public void ComputadorController_ConsultarPorDescricao_ComputadorNaoExiste()
        {
            // Arrange
            var vm = new ConsultarComputadorViewModel();
            vm.Descricao = "c003";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarPorDescricao(vm.Descricao)).Throws(new ComputadorNaoExisteException());

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarPorDescricao(new HttpRequestMessage
                {
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vm.Descricao, response.Descricao);
            Assert.Equal("O computador desejado não existe!", response.ResultadoTransacao);
        }

        // validar consulta por andar
        [Fact]
        public void ComputadorController_ConsultarPorAndar_RetornoValido()
        {
            // Arrange
            var vm  = new ConsultarTudoViewModel();
            var lista = new List<ConsultarTudoViewModel>();
            vm.Andar = "a01";
            var appServico = new Mock<IComputadorAppServico>();
            appServico.Setup(x => x.BuscarTodosPorAndar(vm.Andar)).Returns(lista);

            // Act
            var response = new ComputadorController(appServico.Object)
                .BuscarPorAndar(new HttpRequestMessage
                {
                    Content = new ObjectContent<ConsultarTudoViewModel>(vm, new JsonMediaTypeFormatter())
                }).Content.ReadAsAsync<List<ConsultarTudoViewModel>>().Result;

            // Assert
            Assert.NotNull(response);
        }
    }
}