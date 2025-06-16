namespace WebApplication1.Models
{
    public class StatisticsCourseProgressViewModel
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public double AverageScore { get; set; }
        public int Attempts { get; set; }
        public bool IsCompleted { get; set; }
    }
} 