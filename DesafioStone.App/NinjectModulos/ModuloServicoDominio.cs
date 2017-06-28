using DesafioStone.Dominio.Interfaces.Servicos;
using DesafioStone.Dominio.Servicos;
using Ninject;

namespace DesafioStone.App.NinjectModulos
{
    public class ModuloServicoDominio
    {
        public static void Config(IKernel kernel)
        {
            kernel.Bind<IComputadorServico>().To<ComputadorServico>();
        }
    }
}