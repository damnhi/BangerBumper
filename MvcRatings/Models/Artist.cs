using System.ComponentModel.DataAnnotations;

namespace MvcRatings.Models
{

    public class Artist
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Name")] public string Name { get; set; }

        [Display(Name = "Bio")] public string Bio { get; set; }

        public ICollection<Album>? Albums { get; set; }

        public ICollection<Song>? Songs { get; set; }
    }
}