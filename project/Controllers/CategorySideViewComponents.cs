using DoAnCoSo.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCoSo.Controllers
{
	[ViewComponent(Name = "CategorySide")]
	public class CategorySideViewComponents : ViewComponent
	{
		private readonly DoAnCoSoContext _context;
		public CategorySideViewComponents(DoAnCoSoContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var category = _context.Category.ToList();
			return View("CategorySide", category);
		}
	}
}
