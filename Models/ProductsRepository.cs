namespace WebApp.Models
{
    public class ProductsRepository
    {
        private static List<Product> _products = new List<Product>
        {
            new Product {ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 50, Price = 1.99 },
            new Product {ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 50, Price = 1.99 },
            new Product {ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 50, Price = 4.99 },
            new Product {ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 50, Price = 3.99 },
        };

        public static void AddProduct(Product product)
        { 
            if(_products != null && _products.Count > 0)
            {
                var maxId = _products.Max(x => x.ProductId);
                product.ProductId = maxId + 1;
            }
            else
            {
                product.ProductId = 1;
            }
          
          if(_products == null) _products = new List<Product>();
           _products.Add(product);
        }

        public static List<Product> GetProducts(bool loadCategory = false)
        {
            if (!loadCategory) 
            {
                // 如果不需要加载Category信息，则直接返回原始的_products列表
                return _products;
            }
            else
            {
                if(_products != null && _products.Count >0)
                {
                    // 对于列表中的每个产品，如果它有一个有效的CategoryId
                    _products.ForEach(x =>
                    {
                        // 找到这个CategoryId对应的Category，并将其赋值给产品的Category属性
                        if (x.CategoryId.HasValue)
                        x.Category = CategoriesRepository.GetCategoryById(x.CategoryId.Value);
                    });
                }
                // 返回更新后的_products列表（现在每个产品都可能有了Category信息）
                return _products ?? new List<Product>();
            }

        }


        public static Product? GetProductById(int productId, bool loadCategory =false)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                var prod = new Product 
                { 
                    ProductId = product.ProductId, 
                    CategoryId = product.CategoryId, 
                    Name = product.Name, 
                    Quantity = product.Quantity, 
                    Price = product.Price
                };
                if (loadCategory && prod.CategoryId.HasValue)
                {
                    prod.Category = CategoriesRepository.GetCategoryById(prod.CategoryId.Value);
                }
                return prod;
            }
            return null;
        }

        public static void UpdateProduct(int productId, Product product)
        {
            if(productId != product.ProductId) return; 

            var productToUpdate = _products.FirstOrDefault(x=>x.ProductId == productId);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }

        public static void DeleteProduct(int productId)
        {
            var productToDelete = _products.FirstOrDefault(x=>x.ProductId == productId);
            if (productToDelete != null)
            {
                _products.Remove(productToDelete);
            }
        }

        public static List<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _products.Where(x => x.CategoryId == categoryId);
            if (products != null)
            {
                return products.ToList();
            }
            else { return new List<Product>(); }
        }
    }
}
