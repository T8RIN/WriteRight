using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                // Если пользователь не авторизован, можно показать только доступные курсы без статистики
                // Или перенаправить на страницу входа/регистрации
                return RedirectToAction("Index", "Login");
            }

            var allCourses = await _context.Courses
                .Include(c => c.PrerequisiteCourse)
                .ToListAsync();

            var courseProgressList = new List<CourseProgressViewModel>();

            foreach (var course in allCourses)
            {
                var userResults = await _context.UserTestResults
                    .Where(r => r.UserId == userId && r.CourseId == course.Id)
                    .ToListAsync();

                var attempts = userResults.Count();
                var maxScorePercentage = userResults.Any() 
                    ? userResults.Max(r => (double)r.Score / r.TotalQuestions * 100) 
                    : 0;
                var isCompleted = userResults.Any(r => (double)r.Score / r.TotalQuestions >= 0.8); // 80% для завершения

                bool isPrerequisiteMet = true; // По умолчанию считаем, что предварительный курс пройден
                if (course.PrerequisiteCourseId.HasValue)
                {
                    var prerequisiteCourseId = course.PrerequisiteCourseId.Value;
                    isPrerequisiteMet = await _context.UserTestResults
                        .Where(r => r.UserId == userId && r.CourseId == prerequisiteCourseId)
                        .AnyAsync(r => (double)r.Score / r.TotalQuestions >= 0.8);
                }

                courseProgressList.Add(new CourseProgressViewModel
                {
                    Course = course,
                    Attempts = attempts,
                    MaxScorePercentage = maxScorePercentage,
                    IsCompleted = isCompleted,
                    IsPrerequisiteMet = isPrerequisiteMet // Добавляем статус прохождения предварительного курса
                });
            }

            return View(courseProgressList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Login"); // Пользователь не авторизован
            }

            var course = await _context.Courses
                .Include(c => c.TestQuestions)
                    .ThenInclude(q => q.Options)
                .Include(c => c.PrerequisiteCourse)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Проверка на прохождение предыдущего курса
            if (course.PrerequisiteCourseId.HasValue)
            {
                var prerequisiteCourseId = course.PrerequisiteCourseId.Value;
                var hasPassedPrerequisite = await _context.UserTestResults
                    .Where(r => r.UserId == userId && r.CourseId == prerequisiteCourseId)
                    .AnyAsync(r => (double)r.Score / r.TotalQuestions >= 0.8);

                if (!hasPassedPrerequisite)
                {
                    TempData["ErrorMessage"] = $"Для доступа к этому курсу необходимо пройти курс \"{course.PrerequisiteCourse?.Title}\" (ID: {prerequisiteCourseId}) с результатом не менее 80%.";
                    return RedirectToAction("Index", "Courses");
                }
            }

            return View(course);
        }
    }
} 