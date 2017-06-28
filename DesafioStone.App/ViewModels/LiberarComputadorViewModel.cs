namespace DesafioStone.App.ViewModels
{
    public class LiberarComputadorViewModel
    {
        public string DescricaoComputador { get; set; }
        public string Resultado { get; set; }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(DescricaoComputador.Trim()))
                return false;

            return true;
        }
    }
}