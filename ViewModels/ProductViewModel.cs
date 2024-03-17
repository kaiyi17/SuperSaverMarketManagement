using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        //underplying model
        public Product Product { get; set; } = new Product();
    }
}
