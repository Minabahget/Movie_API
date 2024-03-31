using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Api.Models
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter title"), MaxLength(30)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter created date"), DataType(DataType.Date)]
        public DateOnly CreatedDate { get; set; }


        [Required(ErrorMessage = "Please enter Duration"), DataType(DataType.Time)]
        public TimeOnly Duration { get; set; }


        [Required(ErrorMessage = "Please enter rate"), RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Please enter a valid rate.")]
        public float Rate { get; set; }

        [Required(ErrorMessage = "Please insert image path")]
        public string ImagePath { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();
    }
}
