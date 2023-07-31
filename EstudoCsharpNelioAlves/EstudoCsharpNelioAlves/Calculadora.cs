using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves
{
    public class Calculadora
    {
        public  double Pi = 3.14;



        public double Circunferencia(double raio)
        {
            //Console.Write("valor: " + Pi);
            return 2.0 * double.Pi * raio;
        }
        public double Volume(double raio)
        {
            return 4.0 / 3.0 * double.Pi * Math.Pow(raio, 3);
        }
    }
}
