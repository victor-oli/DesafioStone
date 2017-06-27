using DesafioStone.App.Interfaces;
using DesafioStone.App.ViewModels;
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
            var vm = request.Content.ReadAsAsync<AdicionarViewModel>().Result;

            return new HttpResponseMessage
            {
                Content = new ObjectContent<AdicionarViewModel>(vm, new JsonMediaTypeFormatter())
            };
        }
    }
}