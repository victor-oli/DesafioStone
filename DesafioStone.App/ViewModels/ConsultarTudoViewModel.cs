using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.ObjectosValor;
using System.Collections.Generic;

namespace DesafioStone.App.ViewModels
{
    public class ConsultarTudoViewModel
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Andar { get; private set; }
        public bool Ativo { get; private set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }

        public ConsultarTudoViewModel(Computador computador)
        {
            Id = computador.Id;
            Descricao = computador.Descricao;
            Andar = computador.Andar;
            Ativo = computador.Ativo;
            Ocorrencias = computador.Ocorrencias;
        }

        public bool ConsultaPorAndarEhValida()
        {
            if (string.IsNullOrEmpty(Andar.Trim()))
                return false;

            return true;
        }
    }
}