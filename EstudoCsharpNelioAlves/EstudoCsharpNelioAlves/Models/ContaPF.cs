using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class ContaPF
    {
        public int Numero { get; private set; }
        public string Holder { get; private set; }
        public double Saldo { get; protected  set; }

        //protected permite que SUBCLASSES ALTEREM a propriedade da classe ( programa não pode alterar ) , agora ContaPJ pode alterar o valor de Saldo


        public ContaPF() 
        { 
        
        }

        public ContaPF(int numero, string holder, double saldo)
        {
            Numero = numero;
            Holder = holder;
            Saldo = saldo;
        }

        public virtual void Saque(double quantia)
        {
            Saldo -= quantia - 5;
            Console.WriteLine("Você sacou ( com a taxa de R$5,00 ): " + quantia + " seu saldo atual é de: " + Saldo);
        }
        public void Deposito(double quantia)
        {
            Saldo += quantia;
            Console.WriteLine("Você depositou: " + quantia + " seu saldo atual é de: " + Saldo);
        }
    }
}
