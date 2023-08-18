using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    public class ContaPoupança : ContaPF
    {
        public double TaxaDeInteresse { get; set; }

        public ContaPoupança()
        {

        }

        public ContaPoupança(int numero, string holder, double saldo, double taxadeinteresse)
            :base(numero,holder,saldo)
        {
            TaxaDeInteresse = taxadeinteresse;
        }

        public void AtualizarSaldo(double taxadeinteresse)
        {
            Saldo += Saldo * taxadeinteresse;
        }
        //precsa colocar virtual para fazer o override
        public override void Saque(double quantia)
        {
           // base.
            Saldo -= quantia;
        }
    }
}
