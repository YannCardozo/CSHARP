using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using EstudoCsharpNelioAlves.Models.Enums;
using EstudoCsharpNelioAlves.Models.Exceptions;
using System;

class Program : Estudo
{

    static void Main(String[] args)
    {

        //Mexendo com ENUMS

        /*
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



       try
       {

           Console.WriteLine("Preencha o numero do quarto:");
           int numero_do_quarto = int.Parse(Console.ReadLine());
           Console.WriteLine("Informe a entrada e depois saida");
           DateTime checkin = DateTime.Parse(Console.ReadLine());
           DateTime checkout = DateTime.Parse(Console.ReadLine());

           Reservas TestandoReservas = new Reservas(numero_do_quarto, checkin, checkout);
           TestandoReservas.AtualizaData(checkin, checkout);
       }
       catch(Excecoes ex)
       {
           Console.WriteLine("Erro encontrado: " + ex.ToString());

       }



      */

        //TestandoReservas(405, DateTime.Now(),)




        //mexendo com arquivos


        string caminhodoarquivo = @"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivoteste.txt";


        //assim também funcionará : 
        //string destinodoarquivo = @"C:\Users\Yann S.O\Desktop\temp\arquivoteste.txt";

        string destinodoarquivo = @"C:\Users\Yann S.O\Desktop\temp\";

        try
        {
            //instancia o objeto responsável por receber o arquivo associado ao endereço , no caso a variavel com o endereço dele "caminhodoarquivo"
            FileInfo fileinfo = new FileInfo(caminhodoarquivo);
            //usa o método copyto, copiando o arquivo instanciado para a string da variavel que está sendo passada, no caso destinodoarquivo

            //o método copy to, precisa do nome do destino
            fileinfo.CopyTo(destinodoarquivo + "\\" + fileinfo.Name, true);
        }
        catch(IOException e)
        {
            Console.WriteLine(e.ToString());
        }







        /* 
            try
            {
                Console.WriteLine("preencha 2 numeros para prosseguir:");
                int n1 = int.Parse(Console.ReadLine());
                int n2 = int.Parse(Console.ReadLine());
                int result = n1 / n2;
                Console.WriteLine(result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division by zero is not allowed");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Format error! " + e.Message);
            }

        */

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