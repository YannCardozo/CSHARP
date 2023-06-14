using System;
using Aspose.Cells;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        //SqlConnection connectionString;
        string connectionString = "Data Source=192.168.254.20;Initial Catalog=sempreodonto_com_br;Persist Security Info=True;User ID=sa;Password=a@bBY8tA7D3H;TrustServerCertificate=True";

        int contador = 0;
        List<string> erros = new List<string>();
        List<Cep> lstceps = new List<Cep>();
        bool linhaVazia = true;
        bool todosZeros = true;

        // Carregar arquivo Excel
        Workbook wb = new Workbook("C:\\Users\\Yann S.O\\Desktop\\REPOSITORIO YANN\\Excel-Console-App\\ExtratorPlanilha\\json_credencial\\SEMPRE ODONTO - Planilha geolocalizadora.xlsx");

        // Obter todas as planilhas
        WorksheetCollection collection = wb.Worksheets;

        // Percorra todas as planilhas
        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            // Obter planilha usando seu índice
            Worksheet worksheet = collection[worksheetIndex];

            // Imprimir nome da planilha
            Console.WriteLine("Worksheet: " + worksheet.Name);

            // Obter número de linhas e colunas
            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            // Percorrer as linhas
            for (int i = 0; i <= rows; i++)
            {
                contador++;
                linhaVazia = true;
                todosZeros = true;

                // Criar um novo objeto Cep para cada linha
                Cep cep = new Cep();

                // Percorrer cada coluna na linha selecionada
                for (int j = 0; j < cols + 1; j++)
                {
                    // Recebe o valor da coluna a ser verificada
                    object valor = worksheet.Cells[i, j].Value;

                    // Verifica se o valor é null ou é diferente de vazio
                    if (valor != null && !string.IsNullOrEmpty(valor.ToString()))
                    {
                        linhaVazia = false;

                        if (i > 0 && j == 3)
                        {
                            cep.LATITUDE = double.Parse(worksheet.Cells[i, 3].Value.ToString());
                        }
                        else if (i > 0 && j == 4)
                        {
                            cep.LONGITUDE = double.Parse(worksheet.Cells[i, 4].Value.ToString());
                        }

                        // Verifica se valor é zero
                        if (!valor.Equals(0))
                        {
                            todosZeros = false;
                        }
                    }
                }

                // Adicionar o objeto Cep à lista lstceps
                if (!linhaVazia && !todosZeros)
                {
                    lstceps.Add(cep);
                }

                // Fazemos a busca na lista (array) de erros e adicionamos as repetições
                if (linhaVazia || todosZeros)
                {
                    erros.Add($"A linha {i + 1} está vazia ou contém apenas zeros.");
                }
            }

            // Imprimir quebra de linha
            Console.WriteLine();
        }

        Console.WriteLine("\r\nTerminamos de ler com: " + contador);

        // Caso tenha erros, imprime na tela os erros e em qual linha está o erro
        if (erros.Count > 0)
        {
            foreach (var erro in erros)
            {
                Console.WriteLine(erro);
            }
        }

        // Printa o que foi populado
        foreach (var cep in lstceps)
        {
            Console.WriteLine("LATITUDE: " + cep.LATITUDE);
            Console.WriteLine("LONGITUDE: " + cep.LONGITUDE);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.cep (LATITUDE, LONGITUDE) VALUES (@Latitude, @Longitude)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Latitude", SqlDbType.Float).Value = cep.LATITUDE ?? (object)DBNull.Value;
                command.Parameters.Add("@Longitude", SqlDbType.Float).Value = cep.LONGITUDE ?? (object)DBNull.Value;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("\r\nTerminou ");
    }
}

public class Cep
{
    public int? id_bairro { get; set; }

    public string bairro { get; set; }

    public string cidade { get; set; }

    public string uf { get; set; }

    public string cod_ibge { get; set; }

    public double? LATITUDE { get; set; }

    public double? LONGITUDE { get; set; }

}