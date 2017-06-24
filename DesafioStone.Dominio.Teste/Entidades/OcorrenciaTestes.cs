using DesafioStone.Dominio.Entidades;
using System;
using Xunit;

namespace DesafioStone.Dominio.Teste.Entidades
{
    public class OcorrenciaTestes
    {
        // Testar cadastro de nova ocorrência
        [Fact]
        public void Ocorrencia_Cadastrar_Consistencia()
        {
            var ocorrencia = new Ocorrencia("teste", false);

            Assert.True(ocorrencia.DataOcorrencia != null);
            Assert.True(!string.IsNullOrEmpty(ocorrencia.Descricao.Trim()));
            Assert.NotEqual(new DateTime(1, 1, 1), ocorrencia.DataOcorrencia.Date);
        }
    }
}