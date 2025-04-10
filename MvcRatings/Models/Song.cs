using System.ComponentModel.DataAnnotations;

namespace MvcRatings.Models
{

    public class Song
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Title")] public string Title { get; set; }
        
        [Display(Name = "ArtistId")] public int ArtistId { get; set; }
        [Display(Name = "Artist")]
        public Artist? Artist {
            get;
            set;
        }
        [Display(Name = "AlbumId")] public int? AlbumId { get; set; }
        [Display(Name = "Album")]
        public Album? Album {
            get;
            set;
        }

        [Display(Name = "Release Date")] public DateTime ReleaseDate { get; set; }

        [Display(Name = "Duration")] public TimeSpan Duration { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

    }
}