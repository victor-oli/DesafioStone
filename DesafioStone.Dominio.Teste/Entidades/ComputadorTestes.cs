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
            var computador = new Computador("C001", "A01");
            
            Assert.True(!string.IsNullOrEmpty(computador.Descricao));
            Assert.True(!string.IsNullOrEmpty(computador.Andar));
            Assert.True(computador.Ocorrencias != null);
        }

        // Testar adição da primeira ocorrência
        [Fact]
        public void Computador_AdicionarPrimeiraOcorrencia_Valido()
        {
            var computador = new Computador("C001", "A01");

            Assert.Equal("Cadastro de computador", computador.Ocorrencias[0].Descricao);
            Assert.True(computador.Ocorrencias[0].Liberado);
        }

        // Validar se um computador está disponível através das ocorrencias
        [Fact]
        public void Computador_ValidarDisponibilidade_Valido()
        {
            var computador = new Computador("C001", "A01");

            bool disponivel = computador.VerificarDisponibilidade();

            Assert.True(disponivel);
        }

        // Não permitir a utilização de um computador em uso
        [Fact]
        public void Computador_InformarUsoNaoPermitido_RetornarException()
        {
            var computador = new Computador("C001", "A01");
            computador.Ocorrencias.Add(new Ocorrencia("Em Uso", false));

            var ex = Assert.Throws<ComputadorEmUsoException>(() => computador.InformarUso());
            Assert.Equal("Não é possível utilizar um computador que já está em uso.", ex.Message);
            Assert.False(computador.VerificarDisponibilidade());
        }

        // Validar pegar a última ocorrência
        [Fact]
        public void Computador_PegarUltimaOcorrencia_RetornarOcorrencia()
        {
            var computador = new Computador("C001", "A01");

            var ocorrencia = computador.PegarUltimaOcorrencia();

            Assert.True(ocorrencia != null);
        }

        // Não permitir desativar um computador em uso
        [Fact]
        public void Computador_DesativarComputadorNaoPermitido_RetornarException()
        {
            var computador = new Computador("C001", "A01");
            computador.Ocorrencias.Add(new Ocorrencia("Em Uso", false));

            var ex = Assert.Throws<ComputadorEmUsoException>(() => computador.Desativar());
            Assert.Equal(string.Format("O computador {0} não pode ser desativador pois está em uso.", computador.Descricao), ex.Message);
            Assert.True(!computador.Ativo);
            Assert.NotEqual("Computador desativado", computador.PegarUltimaOcorrencia().Descricao);
        }
    }
}