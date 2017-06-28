using DesafioStone.Dominio.Interfaces.Repositorios;
using DesafioStone.Infra.Repositorios;
using Ninject;

namespace DesafioStone.App.NinjectModulos
{
    public class ModuloRepositorio
    {
        public static void Config(IKernel kernel)
        {
            kernel.Bind<IComputadorRepositorio>().To<ComputadorRepositorio>();
        }
    }
}