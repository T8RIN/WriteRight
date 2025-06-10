using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private readonly List<TestQuestion> _questions = new()
        {
            new TestQuestion
            {
                Id = 1,
                Question = "Выберите слово с приставкой ПРИ-",
                Options = new[] { "Приехать", "Преграда", "Привет", "Прекрасный" },
                CorrectAnswerIndex = 0
            },
            new TestQuestion
            {
                Id = 2,
                Question = "Выберите слово с приставкой ПРЕ-",
                Options = new[] { "Прийти", "Преграда", "Привет", "Приехать" },
                CorrectAnswerIndex = 1
            },
            new TestQuestion
            {
                Id = 3,
                Question = "Выберите слово с приставкой ПРЕ-",
                Options = new[] { "Прийти", "Привет", "Прекрасный", "Приехать" },
                CorrectAnswerIndex = 2
            },
            new TestQuestion
            {
                Id = 4,
                Question = "Выберите слово с приставкой ПРИ-",
                Options = new[] { "Преграда", "Прекрасный", "Привет", "Преодолеть" },
                CorrectAnswerIndex = 2
            },
            new TestQuestion
            {
                Id = 5,
                Question = "Выберите слово с приставкой ПРЕ-",
                Options = new[] { "Прийти", "Привет", "Приехать", "Преодолеть" },
                CorrectAnswerIndex = 3
            }
        };

        public IActionResult Index()
        {
            // Начинаем с первого вопроса
            return View(new TestViewModel 
            { 
                CurrentQuestion = _questions[0],
                QuestionNumber = 1,
                TotalQuestions = _questions.Count,
                UserAnswers = new Dictionary<int, int>()
            });
        }

        [HttpPost]
        public IActionResult NextQuestion(int currentQuestionId, int? selectedAnswer)
        {
            // Сохраняем ответ пользователя
            if (selectedAnswer.HasValue)
            {
                HttpContext.Session.SetInt32($"answer_{currentQuestionId}", selectedAnswer.Value);
            }

            // Находим индекс текущего вопроса
            var currentIndex = _questions.FindIndex(q => q.Id == currentQuestionId);
            if (currentIndex == -1)
            {
                return BadRequest("Вопрос не найден");
            }

            // Если это был последний вопрос, показываем результаты
            if (currentIndex == _questions.Count - 1)
            {
                return RedirectToAction("Results");
            }

            // Переходим к следующему вопросу
            var nextQuestion = _questions[currentIndex + 1];
            return View("Index", new TestViewModel
            {
                CurrentQuestion = nextQuestion,
                QuestionNumber = currentIndex + 2,
                TotalQuestions = _questions.Count,
                UserAnswers = GetUserAnswers()
            });
        }

        public IActionResult Results()
        {
            var userAnswers = GetUserAnswers();
            var correctAnswers = 0;

            foreach (var question in _questions)
            {
                if (userAnswers.TryGetValue(question.Id, out var userAnswer) && 
                    userAnswer == question.CorrectAnswerIndex)
                {
                    correctAnswers++;
                }
            }

            return View(new TestResultsViewModel
            {
                TotalQuestions = _questions.Count,
                CorrectAnswers = correctAnswers,
                Questions = _questions,
                UserAnswers = userAnswers
            });
        }

        private Dictionary<int, int> GetUserAnswers()
        {
            var answers = new Dictionary<int, int>();
            foreach (var question in _questions)
            {
                var answer = HttpContext.Session.GetInt32($"answer_{question.Id}");
                if (answer.HasValue)
                {
                    answers[question.Id] = answer.Value;
                }
            }
            return answers;
        }
    }

    public class TestViewModel
    {
        public TestQuestion CurrentQuestion { get; set; }
        public int QuestionNumber { get; set; }
        public int TotalQuestions { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; }
    }

    public class TestResultsViewModel
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public List<TestQuestion> Questions { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; }
    }
} 