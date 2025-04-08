using DoAnCoSo.Data;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
		private readonly DoAnCoSoContext _context;
		public HomeController(DoAnCoSoContext context)
        {
            _context = context;
        }

		public IActionResult Index()
        {
			var _product = _context.Product.Include(p => p.Brand).Include(p => p.Category);
			return View(_product.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Map()
        {
            return View();
        }
    }
}
