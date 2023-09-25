using EstudoCsharpNelioAlves.Models;
using EstudoCsharpNelioAlves.Models.DelegateMaisLinq;
using EstudoCsharpNelioAlves;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using EstudoCsharpNelioAlves.Models.Enums;
using EstudoCsharpNelioAlves.Models.Exceptions;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using EstudoCsharpNelioAlves.Models.DelegateMaisLinq;

class Program : Estudo
{

    static void print<T>(string message, IEnumerable<T> collection)
    {
        Console.WriteLine(message);
        foreach(var objeto in collection)
        {
            Console.WriteLine(objeto);
        }
    }

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


        //diretorio origem para o teste 
        //@"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivo_filestream.txt";
        /*
                string pathfilestream = @"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivo_filestream.txt";


                string CaminhoOrigem = @"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivo_filestream.txt";
                string EscreveNoArquivo = @"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\EscreveNoArquivo.txt";

                string CaminhoDestino = @"C:\Users\Yann S.O\Desktop\temp\arquivoteste.txt";

                //tente
                try
                {
                    string[] linha = File.ReadAllLines(CaminhoOrigem);
                    using(StreamReader sr = File.OpenText(CaminhoOrigem))
                    {
                        while(!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            Console.WriteLine(line);
                        }

                    }
                    //assim apaga todas as linhas:
                    //File.WriteAllLines(CaminhoOrigem, new string[] { });
                }
                //tratamento de erros
                catch (IOException e)
                {
                    Console.WriteLine("Erro encontrado: ");
                    Console.WriteLine(e.Message);
                }

                try
                {
                    string[] escrevenoarquivo = File.ReadAllLines(CaminhoOrigem);
                    using (StreamWriter escrevendo = File.AppendText(EscreveNoArquivo))
                    {
                        foreach (string linea in escrevenoarquivo)
                            escrevendo.WriteLine(linea.ToUpper());
                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("erro " + e.Message);
                }

                //ESCREVENDO NO ARQUIVO AGORA



               StreamReader sr = null;
                try
                {
                    //fs = new FileStream(pathfilestream, FileMode.Open);
                    //sr = new StreamReader(fs);


                    sr = File.OpenText(pathfilestream);

                    //fazer toda a varredura do arquivo , Ler ele inteiro.
                    while(!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Console.WriteLine(line);

                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("ocorreu um erro " + e.Message);
                }
                finally
                {
                    if(sr != null)
                    {
                        sr.Close();
                    }

                }
                //assim também funcionará : 
                //string destinodoarquivo = @"C:\Users\Yann S.O\Desktop\temp\arquivoteste.txt";

                string destinodoarquivo = @"C:\Users\Yann S.O\Desktop\temp\";

                try
                {
                    //instancia o objeto responsável por receber o arquivo associado ao endereço , no caso a variavel com o endereço dele "caminhodoarquivo"
                    //FileInfo fileinfo = new FileInfo(caminhodoarquivo);
                    //usa o método copyto, copiando o arquivo instanciado para a string da variavel que está sendo passada, no caso destinodoarquivo

                    //o método copy to, precisa do nome do destino
                    //fileinfo.CopyTo(destinodoarquivo + "\\" + fileinfo.Name, true);
                }
                catch(IOException e)
                {
                    Console.WriteLine(e.ToString());
                }






        //Criando Pastas agora:

        string caminhopasta = @"C:\Users\Yann S.O\Desktop\temp";
        try
        {
            //cria coleção genérica para retornar TODAS AS SUBPASTAS de uma pasta original
            IEnumerable<string> ListaDePastas =  Directory.EnumerateDirectories(caminhopasta, "*.*", SearchOption.AllDirectories);
            Console.WriteLine("Folders: ");
            foreach(string s in ListaDePastas)
            {
                Console.WriteLine(s);
            }

            //cria coleção genérica para retornar TODOS OS ARQUIVOS de uma pasta original
            IEnumerable<string> ListaDeArquivos = Directory.EnumerateFiles(caminhopasta, "*.*", SearchOption.AllDirectories);
            Console.WriteLine("Arquivos: ");
            foreach (string s in ListaDeArquivos)
            {
                Console.WriteLine(s);
            }

            //Cria a pasta no directory
            Directory.CreateDirectory(caminhopasta + "\\nova pasta");

            Console.WriteLine("O diretorio do arquivo é: " + Path.GetDirectoryName(caminhopasta));
            Console.WriteLine("O arquivo é: " + Path.GetFileName(caminhopasta));
            Console.WriteLine("a extensão do arquivo é: " + Path.GetExtension(caminhopasta));
            Console.WriteLine("O caminho completo do arquivo é: " + Path.GetFullPath(caminhopasta));

        }
        catch(Exception e)
        {
            Console.WriteLine("erro " + e.Message);
        }



        //

        //PrintService printservice = new PrintService();

        PrintService<int> printservice = new PrintService<int>();

        Console.WriteLine("Quantos valores?");
        int n = int.Parse(Console.ReadLine());


        //repetição que vai digitar o valor de vezes que vc digitou em n
        for(int i = 0; i < n; i++)
        {
            int x = int.Parse(Console.ReadLine());
            //vai adicionando dentro de _values que e um vetor de inteiro com o _count que parará quando for = 10, pelo incremento dentro do metodo AddValue
            printservice.AddValue(x);
        }

        printservice.Print();
        Console.WriteLine("First: " + printservice.FirtValue());








        /*

         

        //string path3 = @"C:\Users\Yann S.O\Desktop\REPOSITORIO YANN\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivoteste.txt";

        string path3 = @"C:\Users\yann_\REPO\Projetos\CSHARP\EstudoCsharpNelioAlves\EstudoCsharpNelioAlves\arquivos\arquivoteste.txt";

        try
        {
            using (StreamReader arquivo = File.OpenText(path3))
            {
                List<Employee> empregado = new List<Employee>();
                // Enquanto o arquivo não chegar ao fim
                while (!arquivo.EndOfStream)
                {
                    // Adiciona na lista de objetos tipo Employee 
                    empregado.Add(new Employee(arquivo.ReadLine()));
                }
                empregado.Sort(); // Ordena a lista de funcionários pelo nome
                foreach (Employee emp in empregado)
                {
                    Console.WriteLine(emp);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Um erro aconteceu");
            Console.WriteLine(e.Message);
        }
        /*


        HashSet<string> set = new HashSet<string>();

        set.Add("Maria");
        set.Add("Joao");
                               */

        //OPERAÇÕES COM LINQ

        Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
        Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
        Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

        List<Produtosss> products = new List<Produtosss>() {
                new Produtosss() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Produtosss() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Produtosss() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Produtosss() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Produtosss() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Produtosss() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Produtosss() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Produtosss() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Produtosss() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Produtosss() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Produtosss() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

        var r1 = products.Where(p => p.Category.Tier == 1 && p.Price > 900.00 );

        var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);

        print("objeto a imprimir: ", r1);
        print("Nome das TOOLS: ", r2);

        /* 
foreach(var produto in r1)
{
    Console.WriteLine(produto);
}



int[] numeros = { 2, 3, 4, 5 };

IEnumerable<int> result = numeros
    .Where(x => x % 2 == 0)
    .Select(x => x * 10);

foreach(int numero in result)
{
    Console.WriteLine(numero);
}


List<Product> ProductsLista = new List<Product>();



ProductsLista.Add(new Product("TV",900.00));
ProductsLista.Add(new Product("Notebook", 1200.00));
ProductsLista.Add(new Product("Tablet", 450.00));
ProductsLista.Add(new Product("Mouse", 7.00));
ProductsLista.Add(new Product("Teclado", 95.00));
ProductsLista.Add(new Product("Mousepad", 30.00));
ProductsLista.Add(new Product("Headset", 250.00));
ProductsLista.Add(new Product("Dispositivo de Áudio", 25.00));


//Comparison<Product> TesteDelegate = CompareProducts();

//Comparison<Product> TesteDelegate = (p1, p2) => p1.Name.ToUpper().CompareTo(p2.Name.ToUpper()) ;

//ProductsLista.Sort(TesteDelegate);
ProductsLista.Sort((p1,p2) => p1.Name.ToUpper().CompareTo(p2.Name.ToUpper()));  

//ProductsLista.Sort(CompareProducts);

foreach (Product produtos in ProductsLista)
{
Console.WriteLine(produtos);
}




//static int CompareProducts(Product product1, Product product2)
//{
//    return product1.Name.ToUpper().CompareTo(product2.Name.ToUpper());
//}
















//Product ProdutoUm = new Product("Alfa", 5.50);



Dictionary<string, string> cookies = new Dictionary<string, string>();


cookies["user"] = "Maria";

cookies["email"] = "maria@gmail.com";

cookies["password"] = "969553127";

cookies["phone"] = "37018392";

//remove chave tipo email do dictionary
cookies.Remove("email");

//faz verificação para saber se existe chave email no dictionary
if(cookies.ContainsKey("email"))
{
Console.WriteLine(cookies["email"]);
}
else
{
Console.WriteLine("Não existe chave de tipo email.");
}

// printa todas as chaves do dictionary
foreach (var dictionarydado in cookies)
{
Console.WriteLine($"chave: {dictionarydado.Key} valor: {dictionarydado.Value}");
}





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