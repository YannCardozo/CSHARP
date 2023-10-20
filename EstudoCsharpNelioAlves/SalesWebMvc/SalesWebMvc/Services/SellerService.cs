using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using SalesWebMvc.Services.Exceptions;

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

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
        }
        public void Remove(int id)
        {
            _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller Obj)
        {
            if(!_context.Seller.Any(x => x.Id == Obj.Id))
            {
                throw new NotFoundException("Id não encontrada");
            }
            try
            {
                _context.Update(Obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
