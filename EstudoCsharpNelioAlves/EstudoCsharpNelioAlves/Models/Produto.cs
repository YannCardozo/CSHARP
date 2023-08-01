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
        private string? _nome;
        private double _preço;
        private int _quantidade;

        //2ª sobrecarga do construtor padrão
        public Produto() { }


        // construtor original
        public Produto(string nome, double preço, int quantidade) 
        {
            _nome = nome;
            _preço = preço;
            _quantidade = quantidade;
        
        }

        public string GetNome()
        {
            return _nome;
        }

        public void SetNome(string nome)
        {
            if(nome != null && nome.Length > 1)
            {
                _nome = nome;
            }
            else
            {
                Console.WriteLine("Nome inválido, precisa ser preenchiido e com mais de 1 caracter");
            }

        }


        //para restringir o acesso ao preço e quantidade, só através dos métodos que poderá ser possível alterar os valores no programa.
        public double GetPreco(double preço)
        {
            return _preço;

        }

        public int GetQuantidade(int quantidade)
        {
            return _quantidade;
        }

        //aqui fazemos uma sobrecarga repetindo o método só que alterando ele ( e eles não conflitaram pois são coisas diferentes )
        public Produto(string nome, double preço) 
        {
            _nome = nome;
            _preço = preço;
            _quantidade = 0;
        }

        public double ValorTotalEmEstoque()
        {
            return _preço * _quantidade;
        }
        public void AdicionarProdutos(int quantidade)
        {
            _quantidade += quantidade;
        }
        public void RemoverProdutos(int quantidade)
        {
            _quantidade -= quantidade;
        }
        public override string ToString()
        {
            //return base.ToString();
            return _nome + ", valor unidade: $ " + _preço.ToString("F2", CultureInfo.InvariantCulture) + ", " + _quantidade + " unidades, total: $ " + ValorTotalEmEstoque().ToString("F2",CultureInfo.InvariantCulture);
        }

    }
}
