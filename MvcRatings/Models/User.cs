using System.ComponentModel.DataAnnotations;

namespace MvcRatings.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Display(Name = "First Name")] public string FirstName { get; set; }

        [Display(Name = "Last Name")] public string LastName { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

    }
}