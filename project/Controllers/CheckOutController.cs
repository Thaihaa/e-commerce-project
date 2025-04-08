using DoAnCoSo.Data;
using DoAnCoSo.ExtensionSession;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoAnCoSo.Controllers
{
	public class CheckOutController: Controller
	{
		private readonly DoAnCoSoContext _context;
		public CheckOutController(DoAnCoSoContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> CheckOut()
		{
			var userName = User.FindFirstValue(ClaimTypes.Name);
			//if (userEmail == null) 
			//{
			//	return RedirectToAction("Login","Account");
			//}
			//else
			//{
			var ordercode = Guid.NewGuid().ToString();
			var orderItem = new OrderModel();
			orderItem.OrderCode = ordercode;
			orderItem.UserName = userName;
			orderItem.Status = 1;
			orderItem.CreatedDate = DateTime.Now;
			_context.Add(orderItem);
			_context.SaveChanges();
			List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			foreach(var cartItem in cartItems)
			{
				var orderdetails = new OrderDetails();
				orderdetails.UserName = userName;
				orderdetails.OrderCode = ordercode;
				orderdetails.ProductId = cartItem.ProductId;
				orderdetails.Price = cartItem.ProductPrice;
				orderdetails.Quantity = cartItem.ProductQuantity;
				_context.Add(orderdetails);
				_context.SaveChanges();
			}
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Đơn hàng đã được tạo!";
			return RedirectToAction("Index","Payment");

			//}
			return View();
		}

	}
}
