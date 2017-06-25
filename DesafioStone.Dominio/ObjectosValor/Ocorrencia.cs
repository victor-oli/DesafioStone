using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class Ocorrencia
    {
        public string Descricao { get; private set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataOcorrencia { get; private set; }
        public bool Liberado { get; private set; }

        private Ocorrencia(string descricao, bool liberado)
        {
            this.Descricao = descricao.Trim().ToUpper();
            this.DataOcorrencia = DateTime.Now;
            this.Liberado = liberado;
        }

        public class OcorrenciaFabrica
        {
            public static Ocorrencia ComputadorDesativado()
            {
                return new Ocorrencia("Computador desativado", false);
            }

            public static Ocorrencia PrimeiraOcorrencia()
            {
                return new Ocorrencia("Cadastro de computador", true);
            }

            public static Ocorrencia ComputadorEmUso()
            {
                return new Ocorrencia("Computador em uso", false);
            }
        }
    }
}