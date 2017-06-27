using DesafioStone.Dominio.Entidades;

namespace DesafioStone.App.ViewModels
{
    public class AdicionarViewModel
    {
        public string Descricao { get; set; }
        public string Andar { get; set; }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Descricao.Trim()))
                return false;

            if (string.IsNullOrEmpty(Andar.Trim()))
                return false;

            return true;
        }

        public Computador RetornarComputador()
        {
            return new Computador(Descricao, Andar);
        }
    }
}