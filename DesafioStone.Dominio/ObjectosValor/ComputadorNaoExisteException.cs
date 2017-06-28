using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class ComputadorNaoExisteException : Exception
    {
        public ComputadorNaoExisteException() : base("O computador desejado não existe!")
        {

        }
    }
}