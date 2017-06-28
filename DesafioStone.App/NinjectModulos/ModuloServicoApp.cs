using DesafioStone.App.AppServicos;
using DesafioStone.App.Interfaces;
using Ninject;

namespace DesafioStone.App.NinjectModulos
{
    public class ModuloServicoApp
    {
        public static void Config(IKernel kernel)
        {
            kernel.Bind<IComputadorAppServico>().To<ComputadorAppServico>();
        }
    }
}