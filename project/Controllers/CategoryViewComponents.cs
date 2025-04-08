using DoAnCoSo.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
	[ViewComponent(Name = "Category")]
	public class CategoryViewComponents: ViewComponent
	{
		private readonly DoAnCoSoContext _context;

		public CategoryViewComponents(DoAnCoSoContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var category = _context.Category.ToList();
			return View("Category", category);
		}
	}
}
