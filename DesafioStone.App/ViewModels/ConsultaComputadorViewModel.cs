using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.ObjectosValor;
using System.Collections.Generic;

namespace DesafioStone.App.ViewModels
{
    public class ConsultaComputadorViewModel
    {
        public string ResultadoTransacao { get; set; }
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Andar { get; private set; }
        public bool Ativo { get; private set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }

        public bool ConsultaPorIdEhValida()
        {
            if (string.IsNullOrEmpty(Id.Trim()))
                return false;

            return true;
        }

        public bool ConsultaPorDescricaoEhValida()
        {
            if (string.IsNullOrEmpty(Descricao.Trim()))
                return false;

            return true;
        }

        public class Fabrica
        {
            public static ConsultaComputadorViewModel Gerar(Computador computador)
            {
                return new ConsultaComputadorViewModel()
                {
                    Id = computador.Id,
                    Descricao = computador.Descricao,
                    Andar = computador.Andar,
                    Ativo = computador.Ativo,
                    Ocorrencias = computador.Ocorrencias,
                    ResultadoTransacao = "OK"
                };
            }
        }
    }
}