using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.ObjectosValor;
using Xunit;

namespace DesafioStone.Dominio.Teste.Entidades
{
    public class ComputadorTestes
    {
        // Validar cadastro de um novo computador
        [Fact]
        public void Computador_ValidarCadastro_SerConsistente()
        {
            // Arrange & Act
            var computador = new Computador("C001", "A01");
            
            // Assert
            Assert.True(!string.IsNullOrEmpty(computador.Descricao));
            Assert.True(!string.IsNullOrEmpty(computador.Andar));
            Assert.True(computador.Ocorrencias != null);
            Assert.True(computador.Ativo);
        }

        // Testar adição da primeira ocorrência
        [Fact]
        public void Computador_AdicionarPrimeiraOcorrencia_Valido()
        {
            // Arrange & Act
            var computador = new Computador("C001", "A01");

            // Assert
            Assert.Equal("Cadastro de computador".ToUpper(), computador.Ocorrencias[0].Descricao);
            Assert.True(computador.Ocorrencias[0].Liberado);
        }

        // Não permitir a utilização de um computador em uso
        [Fact]
        public void Computador_InformarUsoNaoPermitido_RetornarException()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            computador.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());

            // Act & Assert
            var ex = Assert.Throws<ComputadorEmUsoException>(() => computador.InformarUso());
            Assert.Equal("Não é possível utilizar um computador que já está em uso.", ex.Message);
        }

        // Validar pegar a última ocorrência
        [Fact]
        public void Computador_PegarUltimaOcorrencia_RetornarOcorrencia()
        {
            // Arrange
            var computador = new Computador("C001", "A01");

            // Act
            var ocorrencia = computador.PegarUltimaOcorrencia();

            // Assert
            Assert.True(ocorrencia != null);
        }

        // Não permitir desativar um computador em uso
        [Fact]
        public void Computador_DesativarComputadorNaoPermitido_RetornarException()
        {
            // Arrange
            var computador = new Computador("C001", "A01");
            computador.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());

            // Act & Assert
            var ex = Assert.Throws<ComputadorEmUsoException>(() => computador.Desativar());
            Assert.Equal(string.Format("O computador {0} não pode ser desativador pois está em uso.", computador.Descricao), ex.Message);
            Assert.True(computador.Ativo);
            Assert.NotEqual("Computador desativado", computador.PegarUltimaOcorrencia().Descricao);
        }

        // Validar liberação de um computador
        [Fact]
        public void Computador_LiberarComputador_RetornoValido()
        {
            // Arrange
            var computador = new Computador("C001", "A02");

            // Act
            computador.InformarLiberacao();

            // Assert
            Assert.Equal("Computador liberado".ToUpper(), computador.PegarUltimaOcorrencia().Descricao);
            Assert.True(computador.PegarUltimaOcorrencia().Liberado);
        }
    }
}