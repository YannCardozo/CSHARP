﻿using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

class Program : Estudo
{

    static void Main(String[] args)
    {
        //testando o encapsulamento

        Produto p = new Produto("tv", 500.00, 10);


        //após gerar as PROPRIEDADES ( GET{} AND SET{} ) , podemos melhorar a sintaxe do nosso código.

        p.Nome = "alfa";
        Console.WriteLine(p.ToString());

        Carro golf = new Carro();
        golf.Codigo = 0;
        golf.Nome = "Fiat Uno";

        Console.WriteLine(golf.ToString());




        //Console.WriteLine(p.GetNome());

        //p.SetNome("Tv");
        //Console.WriteLine(p.GetNome());

        //Gerando CONSTRUTOR DE PRODUTO
        //Produto tv = new Produto("tv",500.0,10);

        //Console.WriteLine(tv.ToString());


        //Aula  de instanciação

        //Estudo estudo = new Estudo();




        /*
        int qte;
        string nome;
        double preço;

        // estudo.Printando();

        //produto.Nome = "alfa";
        Console.WriteLine("Digite o nome do produto: ");
        nome = Console.ReadLine();
        Console.WriteLine("Digite o preço do produto: ");
        preço = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
        Console.WriteLine("Digite a quantidade do produto: ");
        qte = int.Parse(Console.ReadLine());
        Console.WriteLine("\n\n\n\n");
        Console.WriteLine("Seu estoque é: ");

       //Produto produto = new Produto(nome,preço,qte);
       Produto produto = new Produto(nome,preço);
       Console.WriteLine(produto);

       Produto teste = new Produto();

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

        //Calculadora
        /*
        double MinhaVariavel = 10;
        Console.WriteLine(Calculadora.Circunferencia(MinhaVariavel));

        Console.Write("Entre o valor do raio: ");
        double raio = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
        double circ = Calculadora.Circunferencia(raio);
        double volume = Calculadora.Volume(raio);

        Console.WriteLine("Circunferência: " + circ.ToString("F2",CultureInfo.InvariantCulture));
        Console.WriteLine("Volume: " + volume.ToString("F2", CultureInfo.InvariantCulture));
        */




    }


}