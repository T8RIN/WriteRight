using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class UserTestResult
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // Инициализируем строкой по умолчанию

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        [Required] // Добавляем required, так как это навигационное свойство, которое должно быть
        public Course Course { get; set; }

        public int Score { get; set; } // Количество правильных ответов

        public int TotalQuestions { get; set; } // Общее количество вопросов в тесте

        public DateTime TestDate { get; set; }
    }
} 