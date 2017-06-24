using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioStone.Dominio.ObjectosValor
{
    public class ComputadorEmUsoException : Exception
    {
        public ComputadorEmUsoException(string message) : base(message)
        {
        }
    }
}