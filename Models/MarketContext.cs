using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
	public class MarketContext : DbContext
	{
		public MarketContext(DbContextOptions<MarketContext> options):base(options) 
		{ 
		   
		}
		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		//when the inmemeory based modesl are being constructed, if someting needs to happen, will make it happen here
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			//one category has many products while one product has one category and foreign key is CategoryId
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId);

			//seeding data
			modelBuilder.Entity<Category>().HasData(
				   new Category { CategoryId = 1, Name = "Baverage", Description = "Baverage" },
				   new Category { CategoryId = 2, Name = " Bakery", Description = "Bakery" },
				   new Category { CategoryId = 3, Name = " Meat", Description = "Meat" }
				);

			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 50, Price = 1.99 },
				new Product { ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 50, Price = 1.99 },
				new Product { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 50, Price = 4.99 },
				new Product { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 50, Price = 3.99 }
		    );
			
	}

	}
}
