using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

class Program : Estudo
{

    static void Main(String[] args)
    {


        Produto p = new Produto("tv", 500.00, 10);

        Listas ListaParaTestar = new Listas();

        ListaParaTestar.DeclarandoListasComImprimindo();

        //após gerar as PROPRIEDADES ( GET{} AND SET{} ) , podemos melhorar a sintaxe do nosso código.

        p.Nome = "alfa";
        Console.WriteLine(p.ToString());

        Carro golf = new Carro(2,"golf do ano",25.600,3);
        //golf.Codigo = 0;
        //golf.Nome = "Fiat Uno";



        Console.WriteLine(golf.ToString());

        int testando1 = Calculadora.Sum(1, 2);



        List<Produto> ProdutoLista = new List<Produto>();

        Produto ProdutoObjeto = new Produto("Alfa",5.5);


        ProdutoObjeto.Nome = "tetando";

        string original = "abcde FGHIJ ABC abc DEFG";

        string s1 = original.ToUpper();

        Console.WriteLine(s1);


        DateTime HoraDoDia = DateTime.Now;
       // Console.Write(HoraDoDia);

        //Console.Write(HoraDoDia.ToLongDateString());

        DatasEhoras DataTeste = new DatasEhoras();



        DataTeste.MostraDataCompleta();
        DataTeste.AdicionaHoras();
        

        //ProdutoObjeto
       

        //List<Calculadora> Trilobita = new List<Calculadora>();


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