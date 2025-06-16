using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestOption> TestOptions { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasOne(c => c.PrerequisiteCourse)
                .WithMany()
                .HasForeignKey(c => c.PrerequisiteCourseId)
                .IsRequired(false);
            
            modelBuilder.Entity<UserTestResult>()
                .HasOne(utr => utr.User)
                .WithMany()
                .HasForeignKey(utr => utr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}