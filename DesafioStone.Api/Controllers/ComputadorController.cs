using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
using DesafioStone.Dominio.ObjectosValor;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Content = new StringContent("Cadastro inválido! Você deve informar Descrição e Andar.")
                };
            }
            catch(ComputadorJaExisteException)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("Este computador já está cadastrado!")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage DesativarComputador(HttpRequestMessage request)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("O computador foi desativado.")
            };
        }
    }
}