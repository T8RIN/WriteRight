using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Login"); // Перенаправляем на вход, если пользователь не авторизован или ID недействителен
            }

            var userResults = await _context.UserTestResults
                .Where(r => r.UserId == userId)
                .GroupBy(r => r.CourseId)
                .Select(g => new
                {
                    CourseId = g.Key,
                    AverageScore = g.Average(r => r.Score),
                    Attempts = g.Count(),
                    IsCompleted = g.Any(r => r.Score > 0)
                })
                .ToListAsync();

            var courseProgress = userResults
                .Select(r => new StatisticsCourseProgressViewModel
                {
                    CourseId = r.CourseId,
                    CourseName = _context.Courses.Where(c => c.Id == r.CourseId).Select(c => c.Title).FirstOrDefault(), // Получаем название курса
                    AverageScore = r.AverageScore,
                    Attempts = r.Attempts,
                    IsCompleted = r.AverageScore >= 80
                })
                .ToList();

            return View(courseProgress);
        }
    }
} 