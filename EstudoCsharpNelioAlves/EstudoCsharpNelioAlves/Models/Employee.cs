using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EstudoCsharpNelioAlves.Models
{
    public class Employee : IComparable
    {
        public string Nome { get; set; }
        public double Salario { get; set; }


        public Employee(string csvemployee) {
            string[] vect = csvemployee.Split(',');
            Nome = vect[0];
            Salario = double.Parse(vect[1],CultureInfo.InvariantCulture);        
        }



        public override string ToString()
        {
            return Nome + ", " + Salario.ToString("F2", CultureInfo.InvariantCulture);
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
