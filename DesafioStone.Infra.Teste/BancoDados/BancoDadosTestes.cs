using DesafioStone.Dominio.Entidades;
using DesafioStone.Infra.BancoDados;
using Xunit;

namespace DesafioStone.Infra.Teste.BancoDados
{
    public class BancoDadosTestes
    {
        // Testar conexão com o bando bongodb
        [Fact]
        public void BancoDados_TestarConexao_Validar()
        {
            // Arrange & Act
            var _db = new MongoDbContext<Computador>().Open("Computador");

            // Assert
            Assert.True(_db != null);
        }
    }
}