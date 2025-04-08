using DoAnCoSo.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Models
{
	public class Search: ViewComponent
	{
		private readonly DoAnCoSoContext _context;

		public Search(DoAnCoSoContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Category.ToList());
		}
	}
}
