using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection.Metadata;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using HtmlAgilityPack;
using Wattpad.DTO;
using Wattpad.Nodes;

namespace Wattpad.logindetails
{
    public class LoginAction
    {
        public static async Task ExecuteLogin()
        {
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                // Defina a URL de login
                string loginUrl = "https://www.wattpad.com/";

                // Crie um dicionário com os dados do formulário (por exemplo, nome de usuário e senha)
                var formValues = new Dictionary<string, string>
            {
                { "username", "UsernameTeste" },
                { "password", "Chaons26196460!@" }
                // Adicione outros campos do formulário, se necessário
            };

                // Envie uma solicitação POST para efetuar login
                var content = new FormUrlEncodedContent(formValues);
                HttpResponseMessage response = await httpClient.PostAsync(loginUrl, content);

                // Verifique se o login foi bem-sucedido
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Login bem-sucedido!");
                    var testandodtoteste = new TesteDto();
                    var resultado = testandodtoteste.GetWattpad();

                }
                else
                {
                    Console.WriteLine("Login falhou.");
                }
            }
        }
    }
}
