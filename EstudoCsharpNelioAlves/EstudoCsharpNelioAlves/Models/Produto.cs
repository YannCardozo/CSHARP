using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class Produto
    {
        public string? Nome;
        public double Preço;
        public int Quantidade;

        //2ª sobrecarga do construtor padrão
        public Produto() { }


        // construtor original
        public Produto(string nome, double preço, int quantidade) 
        {
            Nome = nome;
            Preço = preço;
            Quantidade = quantidade;
        
        }

        //aqui fazemos uma sobrecarga repetindo o método só que alterando ele ( e eles não conflitaram pois são coisas diferentes )
        public Produto(string nome, double preço) 
        {
            Nome = nome;
            Preço = preço;
            Quantidade = 0;
        }

        public double ValorTotalEmEstoque()
        {
            return Preço * Quantidade;
        }
        public void AdicionarProdutos(int quantidade)
        {
            Quantidade += quantidade;
        }
        public void RemoverProdutos(int quantidade)
        {
            Quantidade -= quantidade;
        }
        public override string ToString()
        {
            //return base.ToString();
            return Nome + ", valor unidade: $ " + Preço.ToString("F2", CultureInfo.InvariantCulture) + ", " + Quantidade + " unidades, total: $ " + ValorTotalEmEstoque().ToString("F2",CultureInfo.InvariantCulture);
        }

    }
}
