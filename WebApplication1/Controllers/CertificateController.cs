using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    public class CertificateController : Controller {
        private readonly ApplicationDbContext _context;

        public CertificateController(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId)) {
                return RedirectToAction("Index", "Login"); // Пользователь не авторизован
            }

            var finalCourse = await _context.Courses.FirstOrDefaultAsync(c => c.IsFinalCourse);

            if (finalCourse == null) {
                return View(new CertificateViewModel {
                        HasPassedFinalCourse = false,
                        Message = "Итоговый курс не найден в системе. Пожалуйста, обратитесь к администратору."
                    }
                );
            }

            // Получаем все результаты пользователя для данного курса
            var userResultsForFinalCourse = await _context.UserTestResults
                .Where(r => r.UserId == userId && r.CourseId == finalCourse.Id)
                .ToListAsync();

            var viewModel = new CertificateViewModel();

            if (!userResultsForFinalCourse.Any()) {
                // Если нет пройденных тестов по этому курсу
                viewModel.HasPassedFinalCourse = false;
                viewModel.Message =
                    "Вы еще не прошли итоговый курс. Пожалуйста, пройдите его, чтобы получить сертификат.";
            }
            else {
                // Проверяем, есть ли хотя бы один результат с проходным баллом (80%)
                var hasPassedFinalCourse =
                    userResultsForFinalCourse.Any(r => (double)r.Score / r.TotalQuestions >= 0.8);

                if (hasPassedFinalCourse) {
                    viewModel.HasPassedFinalCourse = true;
                    viewModel.Message = "Поздравляем! Вы успешно прошли итоговый курс и получили сертификат.";
                }
                else {
                    viewModel.HasPassedFinalCourse = false;
                    viewModel.Message =
                        "Вы не набрали достаточного балла для получения сертификата по итоговому курсу. Пожалуйста, попробуйте еще раз.";
                }
            }

            return View(viewModel);
        }
    }
}