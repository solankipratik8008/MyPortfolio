namespace MyPortfolio.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
