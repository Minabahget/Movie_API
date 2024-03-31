namespace Movie_Api.DTO
{
    public class MovieWithCategory
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly CreatedDate { get; set; }

        public TimeOnly Duration { get; set; }

        public float Rate { get; set; }

        public string ImagePath { get; set; }

        public List<string> Categories { get; set; }
    }

}
