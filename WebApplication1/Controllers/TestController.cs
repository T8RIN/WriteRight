using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Claims;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var course = await _context.Courses
                                       .Include(c => c.TestQuestions)
                                           .ThenInclude(q => q.Options)
                                       .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound();
            }

            var firstQuestion = course.TestQuestions.OrderBy(q => q.Id).FirstOrDefault();

            if (firstQuestion == null)
            {
                return RedirectToAction("Details", "Courses", new { id = courseId }); // Если нет вопросов, возвращаемся к деталям курса
            }

            HttpContext.Session.Remove($"test_{courseId}_answers"); // Очищаем предыдущие ответы для нового теста

            var viewModel = new TestViewModel
            {
                CourseId = courseId,
                CurrentQuestion = firstQuestion,
                QuestionNumber = 1,
                TotalQuestions = course.TestQuestions.Count,
                UserAnswers = new Dictionary<int, int>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NextQuestion(int courseId, int currentQuestionId, int? selectedOptionId, int questionNumber)
        {
            var userAnswersJson = HttpContext.Session.GetString($"test_{courseId}_answers");
            var userAnswers = userAnswersJson != null ? JsonSerializer.Deserialize<Dictionary<int, int>>(userAnswersJson) : new Dictionary<int, int>();

            if (selectedOptionId.HasValue)
            {
                userAnswers[currentQuestionId] = selectedOptionId.Value;
                HttpContext.Session.SetString($"test_{courseId}_answers", JsonSerializer.Serialize(userAnswers));
            }

            var course = await _context.Courses
                                       .Include(c => c.TestQuestions)
                                           .ThenInclude(q => q.Options)
                                       .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound();
            }

            var questions = course.TestQuestions.OrderBy(q => q.Id).ToList();
            var currentIndex = questions.FindIndex(q => q.Id == currentQuestionId);

            if (currentIndex == -1)
            {
                return BadRequest("Вопрос не найден.");
            }

            if (currentIndex == questions.Count - 1)
            {
                return RedirectToAction("Results", new { courseId = courseId });
            }

            var nextQuestion = questions[currentIndex + 1];
            var viewModel = new TestViewModel
            {
                CourseId = courseId,
                CurrentQuestion = nextQuestion,
                QuestionNumber = questionNumber + 1,
                TotalQuestions = questions.Count,
                UserAnswers = userAnswers
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Results(int courseId)
        {
            var userAnswersJson = HttpContext.Session.GetString($"test_{courseId}_answers");
            var userAnswers = userAnswersJson != null ? JsonSerializer.Deserialize<Dictionary<int, int>>(userAnswersJson) : new Dictionary<int, int>();

            var course = await _context.Courses
                                       .Include(c => c.TestQuestions)
                                           .ThenInclude(q => q.Options)
                                       .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound();
            }

            var questions = course.TestQuestions.ToList();
            var correctAnswersCount = 0;

            foreach (var question in questions)
            {
                if (userAnswers.TryGetValue(question.Id, out var selectedOptionId))
                {
                    var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                    if (correctOption != null && selectedOptionId == correctOption.Id)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            var username = User.Identity?.Name; // Используем имя пользователя
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Login"); // Перенаправляем на вход, если пользователь не авторизован
            }
            
            var existingResult = await _context.UserTestResults.FirstOrDefaultAsync(r => r.UserId == username && r.CourseId == courseId);

            if (existingResult != null)
            {
                existingResult.Score = correctAnswersCount;
                existingResult.TotalQuestions = questions.Count;
                existingResult.TestDate = DateTime.UtcNow;
            }
            else
            {
                var userTestResult = new UserTestResult
                {
                    UserId = username, // Используем имя пользователя как UserId
                    CourseId = courseId,
                    Score = correctAnswersCount,
                    TotalQuestions = questions.Count,
                    TestDate = DateTime.UtcNow
                };
                _context.UserTestResults.Add(userTestResult);
            }
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove($"test_{courseId}_answers"); // Очищаем сессию после сохранения результатов

            var viewModel = new TestResultsViewModel
            {
                Course = course,
                TotalQuestions = questions.Count,
                CorrectAnswers = correctAnswersCount,
                Questions = questions,
                UserAnswers = userAnswers
            };

            return View(viewModel);
        }
    }

    public class TestViewModel
    {
        public int CourseId { get; set; }
        public TestQuestion CurrentQuestion { get; set; } = new TestQuestion();
        public int QuestionNumber { get; set; }
        public int TotalQuestions { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; } = new Dictionary<int, int>();
    }

    public class TestResultsViewModel
    {
        public Course Course { get; set; } = new Course();
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public List<TestQuestion> Questions { get; set; } = new List<TestQuestion>();
        public Dictionary<int, int> UserAnswers { get; set; } = new Dictionary<int, int>();
    }
} 