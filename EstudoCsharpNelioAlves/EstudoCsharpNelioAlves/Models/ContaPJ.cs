using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EstudoCsharpNelioAlves.Models
{
    public class ContaPJ : ContaPF
    {
        public double LimiteEmprestimo { get; set; }

        public ContaPJ() 
        {
            
        }

        //herdando o construtor com os dois pontos :  
        // a base remete ao construtor de ContaPF que esta sendo herdada no contexto de ContaPJ

        public ContaPJ(int numero, string holder, double saldo, double emprestimo) : base(numero,holder,saldo)
        {
            LimiteEmprestimo = emprestimo;
        }

        public void Emprestimo(double quantia)
        {
            if(quantia <= LimiteEmprestimo)
            {
                Saldo += quantia;
            }
            else
            {
                Console.WriteLine("Empréstimo limite é de: " + LimiteEmprestimo);
            }
        }
        public override string ToString()
        {
            string retornostring = "Conta: " + this.Holder + " com número: " + this.Numero + " saldo de: " + this.Saldo.ToString("F2", CultureInfo.InvariantCulture) + " de valor de empréstimo: " + LimiteEmprestimo.ToString("F2", CultureInfo.InvariantCulture);

            return retornostring;
        }

    }
}
