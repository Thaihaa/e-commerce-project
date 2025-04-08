using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DoAnCoSo.Data
{
    public class DoAnCoSoContext : IdentityDbContext
    {
		public DoAnCoSoContext()
		{
		}

		public DoAnCoSoContext (DbContextOptions<DoAnCoSoContext> options)
            : base(options)
        {
        }

        public DbSet<DoAnCoSo.Models.User> User { get; set; } = default!;
        public DbSet<DoAnCoSo.Models.Brand> Brand { get; set; } = default!;
        public DbSet<DoAnCoSo.Models.Category> Category { get; set; } = default!;
        public DbSet<DoAnCoSo.Models.Product> Product { get; set; } = default!;

        public DbSet<OrderModel> Order { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
