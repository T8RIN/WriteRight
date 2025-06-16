using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TestOption
    {
        public int Id { get; set; }

        [Required]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int TestQuestionId { get; set; }

        [ForeignKey("TestQuestionId")]
        public TestQuestion TestQuestion { get; set; } = null!;
    }
} 