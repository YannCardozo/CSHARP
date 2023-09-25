using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class Product 
    {
        public double Price { get; set; }
        public string Name { get; set; }

        public Product(string Nome, double Preco)
        {
            Name = Nome;
            Price = Preco;
        }

        public override string ToString()
        {
            return "O valor de: " + Name + " é: " + Price;
        }

        public int CompareTo(Product teste)
        {
            return Price.CompareTo(teste.Price);
        }
    }

}
