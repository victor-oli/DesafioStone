using DesafioStone.Dominio.Entidades;

namespace DesafioStone.App.ViewModels
{
    public class AdicionarViewModel
    {
        public string Descricao { get; private set; }
        public string Andar { get; private set; }

        public AdicionarViewModel(string descricao, string andar)
        {
            Descricao = descricao.Trim().ToUpper();
            Andar = andar.Trim().ToUpper();
        }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Descricao.Trim()))
                return false;

            if (string.IsNullOrEmpty(Andar.Trim()))
                return false;

            return true;
        }

        public Computador GerarComputador()
        {
            return new Computador(Descricao, Andar);
        }
    }
}