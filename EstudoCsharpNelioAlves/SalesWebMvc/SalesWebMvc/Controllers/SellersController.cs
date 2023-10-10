using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Data; // Importe o namespace do seu contexto de dados
using Microsoft.EntityFrameworkCore; // Importe este namespace para usar o método ToListAsync
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly SalesWebMvcContext _context;

        public SellersController(SellerService sellerService, SalesWebMvcContext context)
        {
            _sellerService = sellerService;
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _sellerService.Findall();
            return View(list);
        }

        public IActionResult Create()
        {
            //var sellertres = _sellerService.FindallDepartmentName();
            var seller = new Seller();
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Seller seller)
//        {
//            if (ModelState.IsValid)
//            {
//                var department = _context.Department.Find(seller.Department.Id);

//                if (department != null)
//                {
//                    seller.Department = department;
//                    _sellerService.Insert(seller);
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    // O Department não existe (você pode lidar com isso de acordo com sua lógica)
//                    ModelState.AddModelError(string.Empty, "Department not found");
//                }
//            }
//            return View(seller);
//        }
//    }
//}