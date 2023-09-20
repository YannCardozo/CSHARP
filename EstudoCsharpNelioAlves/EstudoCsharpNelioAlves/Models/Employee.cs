using System.Globalization;

namespace EstudoCsharpNelioAlves.Models
{
    public class Employee : IComparable
    {
        public string Nome { get; set; }
        public double Salario { get; set; }

        public Employee(string csvemployee)
        {
            string[] vect = csvemployee.Split(',');
            Nome = vect[0];
            Salario = double.Parse(vect[1], CultureInfo.InvariantCulture);
        }
        public Employee(string nome, double salario)
        {
            Salario = salario;
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome + ", " + Salario.ToString("F2", CultureInfo.InvariantCulture);
        }

        public int CompareTo(object? obj)
        {
            // Valida se o objeto é do tipo Employee
            if (!(obj is Employee))
            {
                throw new ArgumentException("Erro de comparação: obj não é do tipo Employee");
            }
            Employee other = (Employee)obj;

            // Compare os objetos com base nos nomes
            return Nome.CompareTo(other.Nome);
        }
    }
}
