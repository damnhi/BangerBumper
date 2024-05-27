using System.ComponentModel.DataAnnotations;

namespace BangerBumper.WebAPP.MVC.Models
{

    public class Rating
    {
        [Key] public int Id { get; set; }

        // rating value from 1 to 5

        [Display(Name = "Value")] public int Value { get; set; }
        
        [Display(Name = "UserId")] public int UserId { get; set; }
        [Display(Name = "User")] public User? User { get; set; }
        
        [Display(Name = "SongId")] public int SongId { get; set; }
        [Display(Name = "Song")] public Song? Song { get; set; }

        [Display(Name = "Date")] public DateTime Date { get; set; }

        [Display(Name = "Comment")] public string Comment { get; set; }

    }
}