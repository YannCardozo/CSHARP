using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void 

    }
}
