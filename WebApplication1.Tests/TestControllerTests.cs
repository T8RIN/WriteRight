using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using Xunit;

namespace WebApplication1.Tests
{
    public class TestControllerTests
    {
        private readonly TestController _controller;
        private readonly Mock<ILogger<TestController>> _loggerMock;

        public TestControllerTests()
        {
            // Настройка мока логгера
            _loggerMock = new Mock<ILogger<TestController>>();
            _loggerMock.Setup(x => x.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

            // Создание контроллера с внедренным логгером
            _controller = new TestController();
        }

        [Theory]
        [InlineData("Index")]
        [InlineData("")]
        [InlineData(null)]
        public void Index_ReturnsViewResult_WithCorrectViewName(string viewName)
        {
            // Arrange
            if (!string.IsNullOrEmpty(viewName))
            {
                _controller.ViewData["ViewName"] = viewName;
            }

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewName ?? "Index", viewResult.ViewName);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithCorrectModel()
        {
            // Arrange
            var expectedModel = new { Message = "Test" };
            _controller.ViewData["Model"] = expectedModel;

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedModel, viewResult.Model);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithCorrectViewData()
        {
            // Arrange
            var expectedViewData = new { Title = "Test Page" };
            _controller.ViewData["Title"] = expectedViewData.Title;

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewData.Title, viewResult.ViewData["Title"]);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithQuestions()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TestQuestion>>(viewResult.Model);
            Assert.Equal(5, model.Count);
        }

        [Theory]
        [InlineData(1, 0, true)]  // Правильный ответ
        [InlineData(1, 1, false)] // Неправильный ответ
        [InlineData(2, 1, true)]  // Правильный ответ
        [InlineData(2, 0, false)] // Неправильный ответ
        public void CheckAnswer_ReturnsCorrectResult(int questionId, int selectedAnswer, bool expectedResult)
        {
            // Act
            var result = _controller.CheckAnswer(questionId, selectedAnswer);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var response = Assert.IsType<dynamic>(jsonResult.Value);
            Assert.Equal(expectedResult, (bool)response.isCorrect);
        }

        [Fact]
        public void CheckAnswer_WithInvalidQuestionId_ReturnsBadRequest()
        {
            // Act
            var result = _controller.CheckAnswer(999, 0);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Questions_ContainCorrectData()
        {
            // Act
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<TestQuestion>>(viewResult.Model);

            // Assert
            var firstQuestion = model.First();
            Assert.Equal("Выберите слово с приставкой ПРИ-", firstQuestion.Question);
            Assert.Equal(4, firstQuestion.Options.Length);
            Assert.Contains("Приехать", firstQuestion.Options);
        }
    }
} 