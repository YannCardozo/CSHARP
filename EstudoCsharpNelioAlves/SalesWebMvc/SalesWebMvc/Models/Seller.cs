using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário preencher o {0}")]
        [Display(Name = "Nome")]
        [StringLength(50,MinimumLength = 6, ErrorMessage = "{0} O nome deve ter pelo menos {2} caracteres e máximo de {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário preencher o {0}")]
        [EmailAddress(ErrorMessage = "É necessário preencher um email válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário preencher o {0}")]
        [Display(Name = "Data de aniversário")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Precisa preencher o salário base.")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} precisa ser no mínimo de: {1} e até {2}")]
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} é necessário preencher.")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; } // Adicione esta propriedade para armazenar o ID do departamento
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
