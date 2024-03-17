using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Cashier")]
    public class SalesController : Controller
    {
        private readonly CategorySQLRepository categoryRepository;
        private readonly ProductSQLRepository productsRepository;
        private readonly TransactionSQLRepository transactionsRepository;

        public SalesController(
       CategorySQLRepository categoryRepository,
       ProductSQLRepository productsRepository,
       TransactionSQLRepository transactionsRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productsRepository = productsRepository;
            this.transactionsRepository = transactionsRepository;
        }

        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };
            return View(salesViewModel);
        }

		public IActionResult SellProductPartial(int productId)
		{
			var product = productsRepository.GetProductById(productId);
			return PartialView("_SellProduct", product);
		}

        [HttpPost]
        public IActionResult Sell([FromForm] SalesViewModel salesViewModel)
        {
			if (ModelState.IsValid)
            {
                //sell the product

                //get the product first
                var prod = productsRepository.GetProductById(salesViewModel.SelectedProductId);
                if (prod != null)
                {
                    transactionsRepository.Add(
                        "Cashier1",
                        salesViewModel.SelectedProductId,
                        prod.Name,
                        prod.Price.HasValue ? prod.Price.Value : 0,
                        prod.Quantity.HasValue ? prod.Quantity.Value : 0,
                        salesViewModel.QuantityToSell);
                    
                    //decrese the quantity
                    prod.Quantity -= salesViewModel.QuantityToSell;
                    productsRepository.UpdateProduct(salesViewModel.SelectedProductId, prod);
                }

              
            }
			var product = productsRepository.GetProductById(salesViewModel.SelectedProductId);
			salesViewModel.SelectedCategoryId = (product?.CategoryId == null)? 0 : product.CategoryId.Value;
			salesViewModel.Categories = categoryRepository.GetCategories();

			return View("Index", salesViewModel);

		}

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = productsRepository.GetProductsByCategoryId(categoryId);
            //using partilview helper method, no need to refresh the page, just loading the partial view
            return PartialView("_Products", products);
        }
    }
}
