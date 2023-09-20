using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;

class Program
{
    static async Task Main()
    {
        try
        {

            //declara variavel para receber a url da api ( normalmente nome é url mesmo )

            //instancia o objeto httpclient para conseguir realizar a chamada http
            //instancia o retorno do objeto a receber a chamada http


            //monta o objeto a receber os mesmos dados da api e instnacia ele recebendo os dados deserializados 
            //printa o objeto


            string cep = "01001000"; // Substitua pelo CEP desejado
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                ViaCepResponse viaCepData = Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCepResponse>(await response.Content.ReadAsStringAsync());
                Console.WriteLine($"CEP: {viaCepData.Cep}\nLogradouro: {viaCepData.Logradouro}\nBairro: {viaCepData.Bairro}\nCidade: {viaCepData.Localidade}\nEstado: {viaCepData.Uf}");
            }
            else
            {
                Console.WriteLine($"Erro na solicitação HTTP: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}

public class ViaCepResponse
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
}
