using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wattpad.DTO;
using HtmlAgilityPack;
using Wattpad.logindetails;

namespace Wattpad.Nodes
{
    public class TesteDto
    {
        public WattpadHomeDTO GetWattpad()
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.wattpad.com/home");
            string taghtml = "h2";
            var titulo = document.DocumentNode.SelectNodes("//*[@id=\"component-home-new-home-landing-%2fhome\"]/div/div[2]/div/div");

            string titulodto = titulo.ToString();

            return new WattpadHomeDTO(titulodto, taghtml);
        }
    }
}
