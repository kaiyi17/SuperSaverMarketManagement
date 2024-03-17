using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Policy ="Inventory")]
    public class ProductsController : Controller
    {
        private readonly ProductSQLRepository productsRepository;
        private readonly CategorySQLRepository categoryRepository;

        public IActionResult Index()
        {
            var products = productsRepository.GetProducts(loadCategory : true);
            return View(products);
        }

        public IActionResult Edit([FromRoute] int id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                Product = productsRepository.GetProductById(id) ?? new Product(),
                Categories = categoryRepository.GetCategories()
            };
            return View(productViewModel);
        }
           

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            productViewModel.Categories = categoryRepository.GetCategories();
            return View(productViewModel);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ProductViewModel
            {
                Categories = categoryRepository.GetCategories()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add([FromForm] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productsRepository.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            //如果提交表单有问题，直接return View(productViewModel)的话，categories是没有被包含的，
            //因为表单提交的时候是提交的选中的category, 而不是整个list,所以需要重新加入categories
            productViewModel.Categories = categoryRepository.GetCategories();

            return View(productViewModel);
        }

        public IActionResult Delete(int id)
        {
            productsRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

   
    }
}
