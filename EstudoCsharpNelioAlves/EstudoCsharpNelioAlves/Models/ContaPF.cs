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

        public void Saque(double quantia)
        {
            Saldo -= quantia;
            Console.WriteLine("Você sacou: " + quantia + " seu saldo atual é de: " + Saldo);
        }
        public void Deposito(double quantia)
        {
            Saldo += quantia;
            Console.WriteLine("Você depositou: " + quantia + " seu saldo atual é de: " + Saldo);
        }
    }
}
