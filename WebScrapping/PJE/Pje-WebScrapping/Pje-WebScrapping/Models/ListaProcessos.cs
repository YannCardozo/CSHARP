using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class ListaProcessos
    {
        //Link: https://tjrj.pje.jus.br/1g/Painel/painel_usuario/advogado.seam <- Menu de navegação/Painel/Painel do representante processual
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public IWebElement ListaDosProcessos { get; set; }
    
    }
}