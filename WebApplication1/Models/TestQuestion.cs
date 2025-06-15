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

        [ForeignKey("CourseId")]
        [Required]
        public Course Course { get; set; }

        public ICollection<TestOption> Options { get; set; } = new List<TestOption>();
    }
} 