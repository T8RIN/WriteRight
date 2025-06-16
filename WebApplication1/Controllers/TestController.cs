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
                // Если это последний вопрос, подсчитываем общий результат и сохраняем его
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var correctAnswersCount = 0;
                Console.WriteLine($"\n--- Начинаем подсчет результатов теста для курса {courseId} ---");
                foreach (var qa in userAnswers)
                {
                    Console.WriteLine($"Обрабатываем вопрос ID: {qa.Key}, выбранный ответ ID: {qa.Value}");
                    var question = await _context.TestQuestions.Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == qa.Key);
                    if (question != null)
                    {
                        var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                        if (correctOption != null)
                        {
                            Console.WriteLine($"   Правильный ответ для вопроса {qa.Key} имеет ID: {correctOption.Id}");
                            if (qa.Value == correctOption.Id)
                            {
                                correctAnswersCount++;
                                Console.WriteLine($"   Ответ правильный! Текущий счет: {correctAnswersCount}");
                            }
                            else
                            {
                                Console.WriteLine($"   Ответ неправильный. Выбрано {qa.Value}, Правильно {correctOption.Id}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"   Внимание: Для вопроса {qa.Key} не найден правильный вариант ответа.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"   Внимание: Вопрос с ID {qa.Key} не найден в базе данных.");
                    }
                }
                Console.WriteLine($"--- Подсчет завершен. Общий правильный счет: {correctAnswersCount} ---");

                var userTestResult = new UserTestResult
                {
                    UserId = userId,
                    CourseId = courseId,
                    Score = correctAnswersCount,
                    TotalQuestions = questions.Count,
                    TestDate = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    UserAnswersJson = JsonSerializer.Serialize(userAnswers)
                };
                _context.UserTestResults.Add(userTestResult);
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove($"test_{courseId}_answers"); // Очищаем сессию после сохранения результатов

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
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Login");
            }

            var userResult = await _context.UserTestResults
                .Include(r => r.Course)
                .Include(r => r.User)
                .Where(r => r.UserId == userId && r.CourseId == courseId)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            if (userResult == null)
            {
                // Вместо перенаправления, отображаем страницу ошибки с сообщением
                return View("Error", new ErrorViewModel { RequestId = "Результат теста для этого курса не найден. Пожалуйста, убедитесь, что вы прошли тест." });
            }

            var course = userResult.Course;
            var questions = await _context.TestQuestions
                .Include(q => q.Options)
                .Where(q => q.CourseId == courseId)
                .ToListAsync();

            // Десериализуем ответы пользователя из UserTestResult
            Dictionary<int, int> userAnswers = new Dictionary<int, int>();
            if (!string.IsNullOrEmpty(userResult.UserAnswersJson))
            {
                userAnswers = JsonSerializer.Deserialize<Dictionary<int, int>>(userResult.UserAnswersJson) ?? new Dictionary<int, int>();
            }

            var viewModel = new TestResultsViewModel
            {
                Course = course,
                TotalQuestions = userResult.TotalQuestions,
                CorrectAnswers = userResult.Score,
                Questions = questions,
                UserAnswers = userAnswers
            };

            return View(viewModel);
        }
    }

    public class TestViewModel
    {
        public int CourseId { get; set; }
        public TestQuestion CurrentQuestion { get; set; } = new();
        public int QuestionNumber { get; set; }
        public int TotalQuestions { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; } = new();
    }

    public class TestResultsViewModel
    {
        public Course Course { get; set; } = new();
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public List<TestQuestion> Questions { get; set; } = [];
        public Dictionary<int, int> UserAnswers { get; set; } = new();
    }
} 