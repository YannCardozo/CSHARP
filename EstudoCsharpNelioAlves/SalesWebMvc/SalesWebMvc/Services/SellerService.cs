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


        public async Task<List<Seller>> FindallAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public List<Department> FindallDepartmentName()
        {
            return _context.Department.ToList();
        }

        public async Task InsertAsync(Seller obj)
        {

            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller Obj)
        {
            bool HasAny = await _context.Seller.AnyAsync(x => x.Id == Obj.Id);
            if (!HasAny)
            {
                throw new NotFoundException("Id não encontrada");
            }
            try
            {
                _context.Update(Obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
