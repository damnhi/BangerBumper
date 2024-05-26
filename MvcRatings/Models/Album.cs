using System.ComponentModel.DataAnnotations;

namespace BangerBumper.WebAPP.MVC.Models
{
    public class Album
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Title")] public string Title { get; set; }

        [Display(Name = "Artist")]
        public Artist? Artist {
            get;
            set;
        }

        [Display(Name = "Release Date")] public DateTime ReleaseDate { get; set; }
        public ICollection<Song>? Songs { get; set; }

    }
}