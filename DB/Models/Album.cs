using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    public class Album : IModel
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        public string AlbumName { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        
        
        [Required]
        public int ArtistId { get; set; } // External key
        [Required]
        public Artist Artist { get; set; } // Navigation property


        [Required]
        public int GenreId { get; set; }
        [Required]
        public Genre Genre { get; set; }
    }
}
