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
            //Seed();
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
        public DbSet<Seller> Seller { get; set; }


        public void Seed()
        {
            if (Department.Any() || Seller.Any() || SalesRecord.Any())
            {
                // Verifica se o banco de dados já foi populado ou não.
                //return;
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "Mateus Santos", "mateus@gmail.com", new DateTime(1990, 7, 15), 1500.0, d2);
            Seller s3 = new Seller(3, "Ana Silva", "ana@gmail.com", new DateTime(1985, 2, 8), 2000.0, d3);
            Seller s4 = new Seller(4, "Carla Oliveira", "carla@gmail.com", new DateTime(1992, 11, 30), 1200.0, d4);
            Seller s5 = new Seller(5, "Rafael Pereira", "rafael@gmail.com", new DateTime(1980, 6, 12), 1800.0, d4);
            Seller s6 = new Seller(6, "Lúcia Souza", "lucia@gmail.com", new DateTime(1995, 9, 5), 1400.0, d2);
            Seller s7 = new Seller(7, "Marcos Ferreira", "marcos@gmail.com", new DateTime(1978, 3, 18), 2200.0, d1);
            Seller s8 = new Seller(8, "Juliana Costa", "juliana@gmail.com", new DateTime(1987, 8, 25), 1600.0, d1);
            Seller s9 = new Seller(9, "Pedro Rodrigues", "pedro@gmail.com", new DateTime(1993, 12, 9), 1300.0, d1);
            Seller s10 = new Seller(10, "Fernanda Almeida", "fernanda@gmail.com", new DateTime(1982, 1, 4), 1900.0, d3);



            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 09, 26), 12000.0, SaleStatus.Billed, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2018, 09, 27), 13000.0, SaleStatus.Billed, s1);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 09, 28), 14000.0, SaleStatus.Billed, s1);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 09, 29), 15000.0, SaleStatus.Billed, s1);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2018, 09, 30), 16000.0, SaleStatus.Billed, s1);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2018, 10, 1), 17000.0, SaleStatus.Billed, s1);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2018, 10, 2), 18000.0, SaleStatus.Billed, s1);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2018, 10, 3), 19000.0, SaleStatus.Billed, s1);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2018, 10, 4), 20000.0, SaleStatus.Billed, s1);

            SalesRecord r11 = new SalesRecord(11, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s2);
            SalesRecord r12 = new SalesRecord(12, new DateTime(2018, 09, 26), 12000.0, SaleStatus.Billed, s2);
            SalesRecord r13 = new SalesRecord(13, new DateTime(2018, 09, 27), 13000.0, SaleStatus.Billed, s2);
            SalesRecord r14 = new SalesRecord(14, new DateTime(2018, 09, 28), 14000.0, SaleStatus.Billed, s2);
            SalesRecord r15 = new SalesRecord(15, new DateTime(2018, 09, 29), 15000.0, SaleStatus.Billed, s2);
            SalesRecord r16 = new SalesRecord(16, new DateTime(2018, 09, 30), 16000.0, SaleStatus.Billed, s2);
            SalesRecord r17 = new SalesRecord(17, new DateTime(2018, 10, 1), 17000.0, SaleStatus.Billed, s2);
            SalesRecord r18 = new SalesRecord(18, new DateTime(2018, 10, 2), 18000.0, SaleStatus.Billed, s2);
            SalesRecord r19 = new SalesRecord(19, new DateTime(2018, 10, 3), 19000.0, SaleStatus.Billed, s2);
            SalesRecord r20 = new SalesRecord(20, new DateTime(2018, 10, 4), 20000.0, SaleStatus.Billed, s2);

            SalesRecord r21 = new SalesRecord(21, new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s3);
            SalesRecord r22 = new SalesRecord(22, new DateTime(2018, 09, 26), 12000.0, SaleStatus.Billed, s3);
            SalesRecord r23 = new SalesRecord(23, new DateTime(2018, 09, 27), 13000.0, SaleStatus.Billed, s3);
            SalesRecord r24 = new SalesRecord(24, new DateTime(2018, 09, 28), 14000.0, SaleStatus.Billed, s3);
            SalesRecord r25 = new SalesRecord(25, new DateTime(2018, 09, 29), 15000.0, SaleStatus.Billed, s3);
            SalesRecord r26 = new SalesRecord(26, new DateTime(2018, 09, 30), 16000.0, SaleStatus.Billed, s3);
            SalesRecord r27 = new SalesRecord(27, new DateTime(2018, 10, 1), 17000.0, SaleStatus.Billed, s3);
            SalesRecord r28 = new SalesRecord(28, new DateTime(2018, 10, 2), 18000.0, SaleStatus.Billed, s3);
            SalesRecord r29 = new SalesRecord(29, new DateTime(2018, 10, 3), 19000.0, SaleStatus.Billed, s3);
            SalesRecord r30 = new SalesRecord(30, new DateTime(2018, 10, 4), 20000.0, SaleStatus.Billed, s3);


            Department.AddRange(d1, d2, d3, d4);
            Seller.AddRange(s1, s2, s3, s4, s5, s6);
            SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17);

            SaveChanges();
        }


    }
}
