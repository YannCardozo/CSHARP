using System;
using Bogus;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using EstudoCsharpNelioAlves.Models;

namespace EstudoCsharpNelioAlves
{
    public class Estudo
    {

        /*
            public void Printando()
            {
                var faker = new Faker();
                Random random = new Random();
                int idade = random.Next(20,100);
                string nome = faker.Name.FullName();
                char genero = 'F';
                double saldo = 120.356;
                Console.WriteLine(saldo.ToString("F2"));
                Console.WriteLine(saldo.ToString("F4", CultureInfo.InvariantCulture));
                Console.WriteLine("{0} tem {1} anos , sendo genero:{2} com saldo de:{3}",nome,idade,genero,saldo);

                //Console.WriteLine("testando");
            }
        */

        public void EntradaDeDados()
        {
            //int n1 = int.Parse(Console.ReadLine());
            // char ch = char.Parse(Console.ReadLine());
            //double n2  = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            string[] vet = Console.ReadLine().Split(' ');
            string nome = vet[0];
            char sexo = char.Parse(vet[1]);
            int idade = int.Parse(vet[2]);
            double altura = double.Parse(vet[3], CultureInfo.InvariantCulture);



            Console.WriteLine("Você digitou:");
            //Console.WriteLine(n1);
            //Console.WriteLine(ch);
            //Console.WriteLine(n2);
            Console.WriteLine(nome);
            Console.WriteLine(sexo);
            Console.WriteLine(idade);
            Console.WriteLine(altura.ToString("F2", CultureInfo.InvariantCulture));
        }

        public void InicioClassesObjeto()
        {
            Triangulo x, y;
            x = new Triangulo();
            y = new Triangulo();
            //double xa, xb, xc, ya, yb, yc;

            Console.WriteLine("Informe os lados do triangulo x:");
            x.A = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            x.B = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            x.C = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("Informe os lados do triangulo y:");
            y.A = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            y.B = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            y.C = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);



            double areaX = x.Area();

            double areay = y.Area();

            Console.WriteLine("Área de x é: " + areaX, CultureInfo.InvariantCulture);
            Console.WriteLine("Área de y é: " + areay, CultureInfo.InvariantCulture);

            if (areaX > areay)
            {
                Console.WriteLine("Area X é maior que Area de Y");
            }
            else
            {
                Console.WriteLine("Area Y é maior que area de X");
            }
        }

    }
}
