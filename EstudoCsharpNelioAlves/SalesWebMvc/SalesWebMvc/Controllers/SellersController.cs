using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Data; // Importe o namespace do seu contexto de dados
using Microsoft.EntityFrameworkCore; // Importe este namespace para usar o método ToListAsync
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.Findall();
            return View(list);
        }

        public IActionResult Create()
        {

            var departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };;
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound("Não encontrado");
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
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