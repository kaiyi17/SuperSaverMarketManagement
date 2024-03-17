using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Policy ="Inventory")]
    public class CategoriesController : Controller
    {

		private readonly CategorySQLRepository categoryRepository;

		public CategoriesController(CategorySQLRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		public IActionResult Index()
        {
            //GET THE DATA IN THE INMEMORY DATA SCOURCE
            var categories = categoryRepository.GetCategories();
            //COMBINE THE DATA TO THE VIEW
            return View(categories);
        }

                                 //specify where the data come from
        
        public IActionResult Edit([FromRoute]int? id)
        {
            ViewBag.Action = "edit";
 
            var category = categoryRepository.GetCategoryById(id.HasValue? id.Value:0);
            return View(category);
      

            //var category = new Category { CategoryId = id.HasValue? id.Value : 0 };
            //if(id.HasValue) 
            //return new ContentResult { Content = id.ToString() };
            // else
            //return new ContentResult { Content =  "null content"};
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid) 
            {
				categoryRepository.UpdateCategory(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(category);
        }


        public IActionResult Add() 
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm]Category category) 
        { 
           if (ModelState.IsValid)
            {
				categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            return View(category);
        }

        public IActionResult Delete(int id)
        {
			categoryRepository.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
