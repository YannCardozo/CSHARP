using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Actions
{
    public class ActionsPJE
    {
        private static IWebDriver driver;  // Declara uma variável de classe para o driver

        public static void Initialize(string url)
        {
            
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }

        public static IWebElement GetWebElement(string tagdoelemento)
        {
            // Certifique-se de que o driver tenha sido inicializado antes de usá-lo
            if (driver == null)
            {
                throw new InvalidOperationException("O driver não foi inicializado. Chame Initialize antes de usar GetWebElement.");
            }

            // Use o driver para encontrar o elemento desejado
            IWebElement elemento = driver.FindElement(By.Name(tagdoelemento));

            return elemento;
        }

        public static void CleanUp()
        {
            driver.Close();
        }

    }
}
