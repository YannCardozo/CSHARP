using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EstudoCsharpNelioAlves.Models
{
    public class Carro
    {
        private int _id;
        private int _codigo;
        private string? _nome;
        private double _preço;
        private int _quantidade;


        public Carro(int codigo, string nome, double preço, int quantidade)
        {
            
            _codigo = codigo;
            _nome = nome;
            _preço = preço;
            _quantidade = quantidade;
        }

        public int Codigo
        {
            get { return _codigo; }

            set { if (value <= 0)
                {
                    Console.WriteLine("Inserir valor diferente de zero.");
                   //Environment.Exit(1);
                }
                _codigo = value;
            }
        }

        public string Nome
        {
            get { return _nome; }

            set { if(value == "" || value.Length <= 5)
                {
                    Console.Write("Inserir nome com mínimo de 5 caracteres");
                   //Environment.Exit(1);
                }
                _nome = value;
                        
            }
        }
        public double ValorEstoque()
        {
            return _quantidade * _preço;
        }

        public void AdicionarCarro(int quantidade)
        {
            _quantidade += quantidade;
        }

        public void RemoverCarro(int quantidade)
        {
            _quantidade -= quantidade;
        }

        public override string ToString()
        {
            return "O carro " + _nome + " tem valor de: " + _preço.ToString("F3", CultureInfo.InvariantCulture) + " em estoque de: " +_quantidade;
        }
    }
}
