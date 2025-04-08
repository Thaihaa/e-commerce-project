using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");
			return RedirectToAction("Index");
		}
	}
}
