using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class Ocorrencia
    {
        public string Descricao { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public bool Liberado { get; private set; }

        public Ocorrencia(string descricao, bool liberado)
        {
            this.Descricao = descricao;
            this.DataOcorrencia = DateTime.Now;
            this.Liberado = liberado;
        }
    }
}