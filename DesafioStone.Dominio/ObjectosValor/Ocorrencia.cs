using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class Ocorrencia
    {
        public string Descricao { get; private set; }
        public string DataOcorrencia { get; set; }
        public bool Liberado { get; private set; }

        private Ocorrencia(string descricao, bool liberado)
        {
            this.Descricao = descricao.Trim().ToUpper();
            this.DataOcorrencia = DateTime.Now.ToString();
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

            public static Ocorrencia ComputadorLiberado()
            {
                return new Ocorrencia("Computador liberado", true);
            }
        }
    }
}