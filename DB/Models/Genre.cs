using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    public class Genre : IModel
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}
