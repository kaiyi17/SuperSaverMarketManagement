using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class CreateUserViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public List<SelectListItem> Roles { get; set; } // This will be used to populate the dropdown list

        public CreateUserViewModel()
        {
            Roles = new List<SelectListItem>();
        }

    }

}
