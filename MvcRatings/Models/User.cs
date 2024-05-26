using System.ComponentModel.DataAnnotations;

namespace BangerBumper.WebAPP.MVC.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Display(Name = "First Name")] public string FirstName { get; set; }

        [Display(Name = "Last Name")] public string LastName { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

    }
}