using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public List<TestOption> Options { get; set; } = new();
    }
} 