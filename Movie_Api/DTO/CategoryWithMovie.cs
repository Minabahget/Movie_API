namespace Movie_Api.DTO
{
    public class CategoryWithMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> MovieNames { get; set; } = new List<string>();

    }
}
