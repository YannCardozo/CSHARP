using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

class Program : Estudo
{

    static void Main(String[] args)
    {
        /* 
         * 
         *Aula  de instanciação
         *
         *
         //Estudo estudo = new Estudo();
         //Produto produto = new Produto();

         //int qte;

         // estudo.Printando();

         //produto.Nome = "alfa";
         Console.WriteLine("Digite o nome do produto: ");
         produto.Nome = Console.ReadLine();
         Console.WriteLine("Digite o preço do produto: ");
         produto.preço = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
         Console.WriteLine("Digite a quantidade do produto: ");
         produto.Quantidade = int.Parse(Console.ReadLine());
         Console.WriteLine("\n\n\n\n");
         Console.WriteLine("Seu estoque é: ");
         Console.WriteLine(produto);
         Console.WriteLine("\n\n\n\n");
         Console.WriteLine("Quantos " +produto.Nome + " vai adicionar ao estoque?");
         qte = int.Parse(Console.ReadLine());
         produto.AdicionarProdutos(qte);
         Console.WriteLine("Depois de adicionado ficou: ");
         Console.WriteLine(produto);


        //Console.WriteLine("Deseja adicionar mais produtos?");
        //produto.AdicionarProdutos(produto.quantidade);

        //int alfa = -4;
        //Console.Write(alfa);
        //estudo.EntradaDeDados();
        //estudo.InicioClassesObjeto();

        */



        double MinhaVariavel = 10;
        Console.WriteLine(Calculadora.Circunferencia(MinhaVariavel));

        Console.Write("Entre o valor do raio: ");
        double raio = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
        double circ = Calculadora.Circunferencia(raio);
        double volume = Calculadora.Volume(raio);

        Console.WriteLine("Circunferência: " + circ.ToString("F2",CultureInfo.InvariantCulture));
        Console.WriteLine("Volume: " + volume.ToString("F2", CultureInfo.InvariantCulture));



    }


}