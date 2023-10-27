using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wattpad.Nodes;

namespace Wattpad.PJE.LoginDetails
{
    public class LoginActionPje
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
                string loginUrl = "https://tjrj.pje.jus.br/1g/login.seam";

                // Crie um dicionário com os dados do formulário (por exemplo, nome de usuário e senha)
                var formValues = new Dictionary<string, string>
            {
                { "username", "15248945755" },
                { "password", "Gabiroba22!@" }
                // Adicione outros campos do formulário, se necessário
            };

                // Envie uma solicitação POST para efetuar login
                var content = new FormUrlEncodedContent(formValues);
                HttpResponseMessage response = await httpClient.PostAsync(loginUrl, content);

                // Verifique se o login foi bem-sucedido
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Login bem-sucedido!");

                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument document = web.Load("https://tjrj.pje.jus.br/1g/QuadroAviso/listViewQuadroAvisoMensagem.seam?cid=32943");
                    string taghtml = "h2";
                    var titulo = document.DocumentNode.SelectNodes("//*[@id=\"quadroAvisoPapelListMensagemId:0:j_id167_body\"]");

                    titulo.ToList().ForEach(i => Console.WriteLine(i.InnerText));

                }
                else
                {
                    Console.WriteLine("Login falhou.");
                }
            }
        }
    }
}
