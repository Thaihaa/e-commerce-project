using DoAnCoSo.Data;
using DoAnCoSo.ExtensionSession;
using DoAnCoSo.Models;
using DoAnCoSo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
	public class CartController : Controller
	{
		private readonly DoAnCoSoContext _context;
		public CartController(DoAnCoSoContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(m => m.ProductQuantity * m.ProductPrice)
			};
			return View(cartVM);
		}

		public ActionResult Checkout()
		{
			return View();
		}

		public async Task<IActionResult> Add(int Id)
		{
			Product product = await _context.Product.FindAsync(Id);
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			CartItem cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItems == null)
			{
				cart.Add(new CartItem(product));
			}
			else
			{
				cartItems.ProductQuantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Thêm sản phẩm thành công";
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

			CartItem cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.ProductQuantity > 1)
			{
				--cartItem.ProductQuantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Đã giảm 1 sản phẩm";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

			CartItem cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.ProductQuantity >= 1)
			{
				++cartItem.ProductQuantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Đã tăng 1 sản phẩm";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
			cart.RemoveAll(p => p.ProductId == Id);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Đã xóa giỏ hàng";
			return RedirectToAction("Index");
		}
	}
}
