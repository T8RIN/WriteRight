namespace WebApplication1.Models
{
    public class CourseProgressViewModel
    {
        public Course Course { get; set; } = new Course();
        public int Attempts { get; set; }
        public double MaxScorePercentage { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPrerequisiteMet { get; set; }
    }
} 