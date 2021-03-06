﻿using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.ObjectosValor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DesafioStone.Api.Controllers
{
    public class ComputadorController : ApiController
    {
        private readonly IComputadorAppServico _appServico;

        public ComputadorController(IComputadorAppServico appServico)
        {
            _appServico = appServico;
        }

        [HttpPost]
        public HttpResponseMessage CadastrarComputador(HttpRequestMessage request)
        {
            try
            {
                var vm = request.Content.ReadAsAsync<AdicionarViewModel>().Result;

                if (vm.EhValido())
                {
                    string id = _appServico.Adicionar(vm);

                    return new HttpResponseMessage
                    {
                        Content = new StringContent(id)
                    };
                }
                else
                    return new HttpResponseMessage
                    {
                        Content = new StringContent("Não é possível cadastrar um cadastrar um computador com essas informações.")
                    };
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Cadastro inválido! Você deve informar Descrição e Andar.")
                };
            }
            catch (ComputadorJaExisteException ex)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };
            }
            catch(Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage DesativarComputador(HttpRequestMessage request)
        {
            try
            {
                DesativarComputadorViewModel vm = request.Content.ReadAsAsync<DesativarComputadorViewModel>().Result;

                if (vm.EhValido())
                {
                    _appServico.Desativar(vm);

                    return new HttpResponseMessage
                    {
                        Content = new StringContent("O computador foi desativado.")
                    };
                }
                else
                    return new HttpResponseMessage
                    {
                        Content = new StringContent("Erro nos dados enviados ao servidor.")
                    };
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Você deve informar o id de um computador.")
                };
            }
            catch (ComputadorNaoExisteException ex)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };
            }
            catch(ComputadorEmUsoException ex)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage LiberarComputador(HttpRequestMessage request)
        {
            LiberarComputadorViewModel vm = new LiberarComputadorViewModel();
            try
            {
                vm = request.Content.ReadAsAsync<LiberarComputadorViewModel>().Result;

                if (vm.EhValido())
                {
                    vm = _appServico.LiberarComputador(vm);

                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    vm.Resultado = "Os dados informados não são válidos.";

                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro no corpo da requisição.")
                };
            }
            catch (ComputadorNaoExisteException ex)
            {
                vm.Resultado = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch(ComputadorDesativadoException ex)
            {
                vm.Resultado = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<LiberarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage UtilizarComputador(HttpRequestMessage request)
        {
            UtilizarComputadorViewModel vm = new UtilizarComputadorViewModel();

            try
            {
                vm = request.Content.ReadAsAsync<UtilizarComputadorViewModel>().Result;

                if (vm.EhValido())
                {
                    vm = _appServico.UtilizarComputador(vm);

                    return new HttpResponseMessage
                    {
                        Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    vm.Resultado = "Os dados informados não são válidos.";

                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro no corpo da requisição.")
                };
            }
            catch (ComputadorNaoExisteException ex)
            {
                vm.Resultado = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (ComputadorEmUsoException ex)
            {
                vm.Resultado = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch(ComputadorDesativadoException ex)
            {
                vm.Resultado = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<UtilizarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpGet]
        public HttpResponseMessage BuscarTodos(HttpRequestMessage request)
        {
            try
            {
                var vm = _appServico.BuscarTodos();

                return new HttpResponseMessage
                {
                    Content = new ObjectContent<List<ConsultarTudoViewModel>>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage BuscarPorId(HttpRequestMessage request)
        {
            ConsultarComputadorViewModel vm = new ConsultarComputadorViewModel();

            try
            {
                vm = request.Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

                if (vm.ConsultaPorIdEhValida())
                {
                    vm = _appServico.Buscar(vm.Id);

                    return new HttpResponseMessage
                    {
                        Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    vm.ResultadoTransacao = "Os dados informados não são válidos.";

                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro no corpo da requisição.")
                };
            }
            catch (ComputadorNaoExisteException ex)
            {
                vm.ResultadoTransacao = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage BuscarPorDescricao(HttpRequestMessage request)
        {
            ConsultarComputadorViewModel vm = new ConsultarComputadorViewModel();

            try
            {
                vm = request.Content.ReadAsAsync<ConsultarComputadorViewModel>().Result;

                if (vm.ConsultaPorDescricaoEhValida())
                {
                    vm = _appServico.BuscarPorDescricao(vm.Descricao);

                    return new HttpResponseMessage
                    {
                        Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    vm.ResultadoTransacao = "Os dados informados não são válidos.";

                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                    };
                }
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro no corpo da requisição.")
                };
            }
            catch (ComputadorNaoExisteException ex)
            {
                vm.ResultadoTransacao = ex.Message;

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent<ConsultarComputadorViewModel>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpGet]
        public HttpResponseMessage BuscarLiberados(HttpRequestMessage request)
        {
            try
            {
                var vm = _appServico.BuscarTodosLiberados();

                return new HttpResponseMessage
                {
                    Content = new ObjectContent<List<ConsultarTudoViewModel>>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpGet]
        public HttpResponseMessage BuscarNaoLiberados(HttpRequestMessage request)
        {
            try
            {
                var vm = _appServico.BuscarTodosNaoLiberados();

                return new HttpResponseMessage
                {
                    Content = new ObjectContent<List<ConsultarTudoViewModel>>(vm, new JsonMediaTypeFormatter())
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage BuscarPorAndar(HttpRequestMessage request)
        {
            try
            {
                var vm = request.Content.ReadAsAsync<ConsultarTudoViewModel>().Result;

                if (vm.ConsultaPorAndarEhValida())
                {
                    List<ConsultarTudoViewModel> lista = _appServico.BuscarTodosPorAndar(vm.Andar);

                    return new HttpResponseMessage
                    {
                        Content = new ObjectContent<List<ConsultarTudoViewModel>>(lista, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("Os dados informados não são válidos.")
                    };
                }
            }
            catch (NullReferenceException)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro no corpo da requisição.")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno no servidor.")
                };
            }
        }
    }
}