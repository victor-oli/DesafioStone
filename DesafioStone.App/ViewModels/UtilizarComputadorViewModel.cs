namespace DesafioStone.App.ViewModels
{
    public class UtilizarComputadorViewModel
    {
        public string Resultado { get; set; }
        public string Descricao { get; set; }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Descricao.Trim()))
                return false;

            return true;
        }
    }
}