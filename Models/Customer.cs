using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp3dotnet.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Le nom du client est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères")]
        public string Name { get; set; }

        // Relation Many-to-One avec MembershipType
        [ForeignKey("MembershipType")]
        public int? MembershiptypeID { get; set; }
        public MemberShipType? MembershipType { get; set; }

        // Relation Many-to-Many avec Movies
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
