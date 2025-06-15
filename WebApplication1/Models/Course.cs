using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
} 