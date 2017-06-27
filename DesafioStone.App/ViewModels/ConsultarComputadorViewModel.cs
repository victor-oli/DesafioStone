using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.ObjectosValor;
using System.Collections.Generic;

namespace DesafioStone.App.ViewModels
{
    public class ConsultarComputadorViewModel
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

        public ConsultarComputadorViewModel()
        {
            Ocorrencias = new List<Ocorrencia>();
        }

        public class Fabrica
        {
            public static ConsultarComputadorViewModel Gerar(Computador computador)
            {
                return new ConsultarComputadorViewModel()
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

        public Ocorrencia PegarUltimaOcorrencia()
        {
            return this.Ocorrencias[this.Ocorrencias.Count - 1];
        }

        public bool VerificarDisponibilidade()
        {
            return PegarUltimaOcorrencia().Liberado;
        }
    }
}