using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Api.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please insert name"), MaxLength(30),]
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Please insert description ")]
        public string Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

    }
}
