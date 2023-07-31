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
        public double preço;
        public int Quantidade;



        public double ValorTotalEmEstoque()
        {
            return preço * Quantidade;
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
            return Nome + ", valor unidade: $ " + preço.ToString("F2", CultureInfo.InvariantCulture) + ", " + Quantidade + " unidades, total: $ " + ValorTotalEmEstoque().ToString("F2",CultureInfo.InvariantCulture);
        }

    }
}
