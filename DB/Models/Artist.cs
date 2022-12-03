using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    public class Artist : IModel
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        public string ArtistName { get; set; }
    }
}
