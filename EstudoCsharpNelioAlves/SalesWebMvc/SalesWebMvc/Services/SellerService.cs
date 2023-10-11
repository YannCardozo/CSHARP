﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }


        public List<Seller> Findall()
        {
            return _context.Seller.ToList();
        }
        public List<Department> FindallDepartmentName()
        {
            return _context.Department.ToList();
        }

        public void Insert(Seller obj)
        {

            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
