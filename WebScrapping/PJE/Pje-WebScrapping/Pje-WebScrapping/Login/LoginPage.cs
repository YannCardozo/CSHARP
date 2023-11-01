using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Web;

namespace Pje_WebScrapping
{
    public class LoginPage
    {
        private IWebDriver driver;
        public void ExecuteLogin()
        {
            using (var httpClient = new HttpClient())
            {
                driver = new ChromeDriver();
                // Defina a URL de login
                string loginUrl = "https://tjrj.pje.jus.br/1g/login.seam";

                // Crie um dicionário com os dados do formulário (por exemplo, nome de usuário e senha)
                var formValues = new Dictionary<string, string>
            {
                { "username", "15248945755" },
                { "password", "Gabiroba22!@" }
                // Adicione outros campos do formulário, se necessário
            };

                // Codifique os valores do formulário em uma sequência de consulta
                var formData = new FormUrlEncodedContent(formValues).ReadAsStringAsync().Result;

                // Crie a solicitação POST
                var content = new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded");
                httpClient.DefaultRequestHeaders.ExpectContinue = false;

                // Envie a solicitação POST para efetuar login
                var response = httpClient.PostAsync(loginUrl, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Login bem-sucedido!");



                    // Navegue para a página desejada usando o Selenium WebDriver
                    driver.Navigate().GoToUrl("https://tjrj.pje.jus.br/1g/QuadroAviso/listViewQuadroAvisoMensagem.seam?cid=32943");
                    int iframesize = driver.FindElements(By.TagName("iframe")).Count;

                    Console.WriteLine(iframesize);

                    // Aguarde o carregamento da página usando o Selenium WebDriver
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                    // Agora você pode continuar com o web scraping usando o HtmlAgilityPack
                    string pageSource = driver.PageSource;
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(pageSource);

                    // Agora você pode fazer scraping com o HtmlAgilityPack
                    // ...

                    // Lembre-se de gerenciar o driver e fechá-lo quando terminar
                   // driver.Quit();
                }
                else
                {
                    Console.WriteLine("Login falhou.");
                }
            }
        }


    }
}
