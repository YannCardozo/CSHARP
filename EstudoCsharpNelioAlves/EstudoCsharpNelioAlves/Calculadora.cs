using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves
{
    public class Calculadora
    {
        public static double Pi = 3.14;



        public static double Circunferencia(double raio)
        {
            //Console.Write("valor: " + Pi);
            return 2.0 * double.Pi * raio;
        }
        public static double Volume(double raio)
        {
            return 4.0 / 3.0 * double.Pi * Math.Pow(raio, 3);
        }
        public static int Sum(params int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];

            }
            return sum;
        }

        //ref faz com que referencie a variavel que esta sendo chamada na main e altere diretamente ela
        //não é BOA PRATICA, esquece.
        public static void Triple(ref int x)
        {
            x = x * 3;
        }
    }
}
