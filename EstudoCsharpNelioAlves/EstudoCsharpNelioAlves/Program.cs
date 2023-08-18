using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using EstudoCsharpNelioAlves.Models.Enums;

class Program : Estudo
{

    static void Main(String[] args)
    {

        //Mexendo com ENUMS

        Order PedidoDoCurso = new Order
        {
            Id = 1080,
            Situação = DateTime.Now,
            Status = OrderStatus.PagamentoPendente
        };

        Console.WriteLine(PedidoDoCurso);

        string txt = OrderStatus.PagamentoPendente.ToString();
        Console.WriteLine(txt);

        OrderStatus Os = Enum.Parse<OrderStatus>("Entregue");
        Console.WriteLine(Os);


        ContaPJ NovoCliente = new ContaPJ(01, "Maria joaquina", 250.000, 5500);


        Console.WriteLine(NovoCliente.ToString());
        Console.WriteLine(NovoCliente.Saldo);

        ContaPJ TestandoPJ = new ContaPJ(02, "TestePJ", 2500000, 55000);
        ContaPF TestandoPF = new ContaPJ(03, "TestePF", 3500000, 125000);
        ContaPoupança TestandoPoupança = new ContaPoupança(04, "TestePoupança", 500000, 35000);

        TestandoPJ.Saque(2300);
        TestandoPF.Saque(3000);
        TestandoPoupança.Saque(5000);

        Console.WriteLine(TestandoPJ.Saldo);
        Console.WriteLine(TestandoPF.Saldo);
        Console.WriteLine(TestandoPoupança.Saldo);

        List<ContaPF> listaPF = new List<ContaPF>();

        listaPF.Add(new ContaPoupança(1001,"Alex",3556.55,0.01));
        listaPF.Add(new ContaPJ(1002, "Maria", 500.0, 0.01));
        listaPF.Add(new ContaPoupança(1003, "Bob", 500.0, 0.01));
        listaPF.Add(new ContaPJ(1004, "Ana", 500.0, 0.01));


        double sum = 0.0;

        foreach(ContaPF conta in listaPF)
        {
            sum += conta.Saldo;
        }

        Console.WriteLine(sum);




        /*     Produto p = new Produto("tv", 500.00, 10);

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
           */

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