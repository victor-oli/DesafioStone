namespace DesafioStone.App.ViewModels
{
    public class DesativarComputadorViewModel
    {
        public string Id { get; private set; }

        public DesativarComputadorViewModel(string id)
        {
            Id = id;
        }

        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Id.Trim()))
                return false;

            return true;
        }
    }
}