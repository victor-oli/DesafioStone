using DesafioStone.Dominio.Entidades;
using DesafioStone.Infra.Repositorios;
using MongoDB.Bson;
using Xunit;

namespace DesafioStone.Infra.Teste.Repositorios
{
    public class ComputadorRepositorioTestes
    {
        // Testar adição de um novo computador
        [Fact]
        public void ComputadorRepositorio_CadastrarComputador_ConsultarParaValidar()
        {
            // Arrange
            var computador = new Computador("C001", "A01");

            // Act
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);

            // Assert
            Assert.Equal(computador.Id, id);
            Assert.Equal(true, new ComputadorRepositorio().Buscar(id).Ativo);
        }

        // Testar desativação de um computador
        [Fact]
        public void ComputadorRepositorio_DesativarRegistro_AtivoDeveEstarFalso()
        {
            // Arrange
            var computador = new Computador("C010", "A02");
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);

            // Act
            new ComputadorRepositorio().Desativar(computador);

            // Assert
            Assert.Equal(false, new ComputadorRepositorio().Buscar(id).Ativo);
        }

        // Testar update de um computador
        [Fact]
        public void ComputadorRepositorio_AtualizarRegistro_ValidarAtualizacao()
        {
            // Arrange
            var computador = new Computador("C012", "A02");
            ObjectId id = new ComputadorRepositorio().Adicionar(computador);
            computador.Descricao = "C013";
            computador.Andar = "A03";
            computador.Ativo = false;

            // Act
            new ComputadorRepositorio().Atualizar(computador);

            // Assert
            var computadorAtualizado = new ComputadorRepositorio().Buscar(computador.Id);
            Assert.Equal("C013", computadorAtualizado.Descricao);
            Assert.Equal("A03", computadorAtualizado.Andar);
            Assert.Equal(false, computadorAtualizado.Ativo);
        }

        // Testar consulta de um unico computador
        // Testar consulta de todos os computadores
    }
}