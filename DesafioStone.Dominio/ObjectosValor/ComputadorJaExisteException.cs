using System;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class ComputadorJaExisteException : Exception
    {
        public ComputadorJaExisteException() : base("Já existe um computador com este nome!")
        {
        }
    }
}