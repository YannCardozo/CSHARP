using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class ContaPF
    {
        public int Numero { get; set; }
        public string Holder { get; set; }
        public double Saldo { get; set; }




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
