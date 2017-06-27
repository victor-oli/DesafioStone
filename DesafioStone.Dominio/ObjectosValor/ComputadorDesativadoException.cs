using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class ComputadorDesativadoException : Exception
    {
        public ComputadorDesativadoException() : 
            base("Computador desativado. Não é possível utilizar este computador!")
        {

        }
    }
}