using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;

namespace WebApplication1.ViewComponents
{
    public class CertificateEligibilityViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CertificateEligibilityViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return View(false); // Пользователь не авторизован или ID недействителен
            }

            var finalCourse = await _context.Courses.FirstOrDefaultAsync(c => c.IsFinalCourse);

            if (finalCourse == null)
            {
                // Если итоговый курс не найден, сертификат не может быть выдан
                return View(false);
            }

            // Проверяем, есть ли хотя бы один результат с проходным баллом (80%) для итогового курса
            var hasPassedFinalCourse = await _context.UserTestResults
                .Where(r => r.UserId == userId && r.CourseId == finalCourse.Id)
                .AnyAsync(r => (double)r.Score / r.TotalQuestions >= 0.8);

            return View(hasPassedFinalCourse);
        }
    }
} 