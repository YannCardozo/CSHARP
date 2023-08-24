using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models.Exceptions
{
    class Excecoes : ApplicationException
    {
        public Excecoes(string message) : base(message)
        { 
            
        }
    }
}
