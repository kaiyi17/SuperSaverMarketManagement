
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string? SelectedPosition { get; set; }

        public List<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
    }
}
