using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MvcRatings.Models
{
    public class User : IdentityUser<Guid>
    {
        [Display(Name = "First Name")] public string? FirstName { get; set; }

        [Display(Name = "Last Name")] public string? LastName { get; set; }

        public ICollection<Rating>? Ratings { get; set; }
    }
}