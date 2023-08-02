using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class Carro
    {
        private int _id;
        private int _codigo;
        private string? _nome;
        private double _preço;
        private int _quantidade;



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


        public override string ToString()
        {
            return "O carro " + _nome + " tem valor de: " + _preço + " em estoque de: " +_quantidade;
        }
    }
}
