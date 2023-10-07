using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {

        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
        public DbSet<Seller> Seller { get; set; }


        //apagar migration em casa e testar
        //pegar a formatação abaixo no metodo SEED e colocar nesse padrão e dps testar
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SalesRecord>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Seller>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Computers"
                },
                new Department
                {
                    Id = 2,
                    Name = "Electronics"
                },
                new Department
                {
                    Id = 3,
                    Name = "Fashion"
                },
                new Department
                {
                    Id = 4,
                    Name = "Books"
                }
            );
            modelBuilder.Entity<Seller>().HasData(
                          new Seller
                          {
                              Name = "João da Silva",
                              Email = "joao.silva@example.com",
                              Birthday = new DateTime(1990, 1, 1),
                              BaseSalary = 2000.0,
                              Department = new Department { Id = 1, Name = "Computadores" }
                          },
                           new Seller
                           {
                               Name = "Maria Santos",
                               Email = "maria.santos@example.com",
                               Birthday = new DateTime(1990, 1, 2),
                               BaseSalary = 2200.0,
                               Department = new Department { Id = 2, Name = "Eletrônicos" }
                           },
                           new Seller
                           {
                               Name = "Pedro Oliveira",
                               Email = "pedro.oliveira@example.com",
                               Birthday = new DateTime(1990, 1, 3),
                               BaseSalary = 2300.0,
                               Department = new Department { Id = 3, Name = "Moda" }
                           },
                           new Seller
                           {
                               Name = "Ana Costa",
                               Email = "ana.costa@example.com",
                               Birthday = new DateTime(1990, 1, 4),
                               BaseSalary = 2400.0,
                               Department = new Department { Id = 4, Name = "Livros" }
                           },
                           new Seller
                           {
                               Name = "Luiz Fernandes",
                               Email = "luiz.fernandes@example.com",
                               Birthday = new DateTime(1990, 1, 5),
                               BaseSalary = 2100.0,
                               Department = new Department { Name = "Computadores" }
                           },
                           new Seller
                           {
                               Name = "Carla Almeida",
                               Email = "carla.almeida@example.com",
                               Birthday = new DateTime(1990, 1, 6),
                               BaseSalary = 2300.0,
                               Department = new Department { Name = "Eletrônicos" }
                           },
                           new Seller
                           {
                               Name = "José Santos",
                               Email = "jose.santos@example.com",
                               Birthday = new DateTime(1990, 1, 7),
                               BaseSalary = 2400.0,
                               Department = new Department { Name = "Moda" }
                           },
                           new Seller
                           {
                               Name = "Amanda Pereira",
                               Email = "amanda.pereira@example.com",
                               Birthday = new DateTime(1990, 1, 8),
                               BaseSalary = 2500.0,
                               Department = new Department { Name = "Livros" }
                           },
                           new Seller
                           {
                               Name = "Ricardo Lima",
                               Email = "ricardo.lima@example.com",
                               Birthday = new DateTime(1990, 1, 9),
                               BaseSalary = 2200.0,
                               Department = new Department { Id = 1, Name = "Computadores" }
                           },
                           new Seller
                           {
                               Name = "Fernanda Barbosa",
                               Email = "fernanda.barbosa@example.com",
                               Birthday = new DateTime(1990, 1, 10),
                               BaseSalary = 2100.0,
                               Department = new Department { Name = "Eletrônicos" }
                           }
                       );
            modelBuilder.Entity<SalesRecord>().HasData(
                 new SalesRecord
                 {
                     Id = 1,
                     Date = new DateTime(2018, 09, 25),
                     Amount = 11000.0,
                     Status = SaleStatus.Pending,
                     Seller = new Seller(1, "Nome2", "Email2", new DateTime(1990, 01, 02), 1200.0, new Department(2, "Electronics"))
                 },
                new SalesRecord
                {

                    Id = 2,
                    Date = new DateTime(2018, 09, 26),
                    Amount = 12000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(2, "Nome2", "Email2", new DateTime(1990, 01, 02), 1200.0, new Department(2, "Electronics"))
                },
                new SalesRecord
                {

                    Id = 3,
                    Date = new DateTime(2018, 09, 27),
                    Amount = 13000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(3, "Nome3", "Email3", new DateTime(1990, 01, 03), 1300.0, new Department(3, "Fashion"))
                },
                new SalesRecord
                {
                    Id = 4,
                    Date = new DateTime(2018, 09, 28),
                    Amount = 14000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(4, "Nome4", "Email4", new DateTime(1990, 01, 04), 1400.0, new Department(4, "Books"))
                },
                new SalesRecord
                {
                    Id = 5,
                    Date = new DateTime(2018, 09, 29),
                    Amount = 15000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(5, "Nome5", "Email5", new DateTime(1990, 01, 05), 1500.0, new Department(1, "Computers"))
                },
                new SalesRecord
                {
                    Id = 6,
                    Date = new DateTime(2018, 09, 30),
                    Amount = 16000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(6, "Nome6", "Email6", new DateTime(1990, 01, 06), 1600.0, new Department(2, "Electronics"))
                },
                new SalesRecord
                {
                    Id = 7,
                    Date = new DateTime(2018, 10, 1),
                    Amount = 17000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(7, "Nome7", "Email7", new DateTime(1990, 01, 07), 1700.0, new Department(3, "Fashion"))
                },
                new SalesRecord
                {
                    Id = 8,
                    Date = new DateTime(2018, 10, 2),
                    Amount = 18000.0,
                    Status = SaleStatus.Pending,
                    Seller = new Seller(8, "Nome8", "Email8", new DateTime(1990, 01, 08), 1800.0, new Department(4, "Books"))
                },
                new SalesRecord
                {Id = 9,
                    Date = new DateTime(2018, 10, 3),
                    Amount = 19000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(9, "Nome9", "Email9", new DateTime(1990, 01, 09), 1900.0, new Department(1, "Computers"))
                },
                new SalesRecord
                {Id = 10,
                    Date = new DateTime(2018, 10, 4),
                    Amount = 20000.0,
                    Status = (SaleStatus)new Random().Next(0, 3),
                    Seller = new Seller(10, "Nome10", "Email10", new DateTime(1990, 01, 10), 2000.0, new Department(2, "Electronics"))
                }
            );

           
        }
    }
}
