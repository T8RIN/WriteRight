using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class UserTestResult
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Изменено на int

        [ForeignKey("UserId")]
        public User? User { get; set; } // Навигационное свойство к User (сделано nullable)

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int Score { get; set; } // Количество правильных ответов

        public int TotalQuestions { get; set; } // Общее количество вопросов в тесте

        public DateTime TestDate { get; set; }

        public DateTime CompletedAt { get; set; }
    }
} 