using System.ComponentModel.DataAnnotations;

namespace tp3dotnet.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Relation One-to-Many avec Movie
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
