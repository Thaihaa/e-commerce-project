using DoAnCoSo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Controllers
{
	public class OrderController : Controller
	{
        private readonly DoAnCoSoContext _context;
        public OrderController(DoAnCoSoContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrator")]
		public async  Task<IActionResult> Index()
		{
			return View(await _context.Order.OrderByDescending(m=>m.Id).ToListAsync());
		}


    }
}
