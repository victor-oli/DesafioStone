using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class ComputadorEmUsoException : Exception
    {
        public ComputadorEmUsoException(string message) : base(message)
        {
        }
    }
}