using Microsoft.AspNetCore.Mvc;
using DoAnCoSo.Models;
using DoAnCoSo.Data;

namespace DoAnCoSo.Controllers
{
	public class SearchController : Controller
	{
		private readonly DoAnCoSoContext _context;

		public SearchController(DoAnCoSoContext context)
		{
			_context = context;
		}
        public IActionResult SearchBox(string sTuKhoa)
        {
            var listProduct = _context.Product.Where(p => p.ProductName.Contains(sTuKhoa));
            return View(listProduct.OrderBy(p => p.ProductName));
        }
    }
}
