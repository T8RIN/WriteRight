namespace WebApplication1.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
} 