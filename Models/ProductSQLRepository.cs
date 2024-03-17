using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
	public class ProductSQLRepository
	{
		private readonly MarketContext db;

		public ProductSQLRepository(MarketContext db)
		{
			this.db = db;
		}

		public void AddProduct(Product product)
		{
			db.Products.Add(product);
			db.SaveChanges();
		}

		public List<Product> GetProducts(bool loadCategory = false)
		{
			if (loadCategory) { return db.Products.Include(x => x.Category).ToList(); }
			else return db.Products.OrderBy(x => x.Category).ToList();

		}


		public Product? GetProductById(int productId, bool loadCategory = false)
		{
			if(loadCategory)
			{
				return db.Products
					.Include(x=>x.Category)
					.FirstOrDefault(x=>x.ProductId == productId);
			}
			else
			{
				return db.Products.FirstOrDefault(x=>x.ProductId == productId);
			}
		}

		public void UpdateProduct(int productId, Product product)
		{
			if (productId != product.ProductId) return;
			var prod = db.Products.Find(productId);
			if (prod == null) return;
			prod.CategoryId = product.CategoryId;
			prod.Name = product.Name;
			prod.Price = product.Price;
			prod.Quantity = product.Quantity;
			db.SaveChanges();
		}

		public void DeleteProduct(int productId)
		{
			var product = db.Products.Find(productId);
			if (product == null) return;
			db.Products.Remove(product);
			db.SaveChanges();
		}

		public List<Product> GetProductsByCategoryId(int categoryId)
		{
			return db.Products.Where (x=>x.CategoryId == categoryId).ToList();
		}
	}
}

