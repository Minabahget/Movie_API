using System.ComponentModel.DataAnnotations;

namespace Movie_Api.DTO
{
    public class MovieCreateModel
    {

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

        //[Required(ErrorMessage = "Please upload image")]
        public IFormFile? Image { get; set; }

        public List<int> Categories { get; set; } = new List<int>();
    }
}
