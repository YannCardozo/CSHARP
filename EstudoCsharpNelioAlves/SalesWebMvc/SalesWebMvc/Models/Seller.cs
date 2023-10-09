using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public double BaseSalary { get; set; }
        public int DepartmentId { get; set; } // Adicione esta propriedade para armazenar o ID do departamento
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        // Este método não precisa ser alterado

        public string FormattedBaseSalary => BaseSalary.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

        // Construtor sem o departamento

        public Seller()
        {

        }

        // Construtor com o departamento

        public Seller(int id, string name, string email, DateTime birthday, double baseSalary, int departmentId)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthday = birthday;
            BaseSalary = baseSalary;
            DepartmentId = departmentId;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
