using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            // Убедитесь, что база данных создана
            await context.Database.EnsureCreatedAsync();

            // Проверяем, есть ли уже курсы
            if (await context.Courses.AnyAsync())
            {
                return; // База данных уже содержит данные
            }

            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Основы русского языка",
                    Description = "Базовый курс по грамматике и правописанию русского языка.",
                    Content = "<p>В этом курсе вы изучите:</p><ul><li>Основы грамматики</li><li>Правила правописания</li><li>Пунктуацию</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является именем существительным?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Бежать", IsCorrect = false },
                                new TestOption { OptionText = "Красивый", IsCorrect = false },
                                new TestOption { OptionText = "Стол", IsCorrect = true },
                                new TestOption { OptionText = "Быстро", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является глаголом в прошедшем времени?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Читать", IsCorrect = false },
                                new TestOption { OptionText = "Читает", IsCorrect = false },
                                new TestOption { OptionText = "Читал", IsCorrect = true },
                                new TestOption { OptionText = "Будет читать", IsCorrect = false }
                            }
                        }
                    }
                },
                new Course
                {
                    Title = "Продвинутый русский язык",
                    Description = "Углубленный курс для тех, кто хочет усовершенствовать свои знания.",
                    Content = "<p>В этом курсе вы углубитесь в:</p><ul><li>Сложные синтаксические конструкции</li><li>Стилистику</li><li>Тонкости пунктуации</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "В каком предложении допущена пунктуационная ошибка?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Он долго думал, и наконец, принял решение.", IsCorrect = true },
                                new TestOption { OptionText = "Несмотря на дождь, мы пошли гулять.", IsCorrect = false },
                                new TestOption { OptionText = "Я знаю, что ты придешь.", IsCorrect = false },
                                new TestOption { OptionText = "Солнце светило ярко, согревая землю.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое из этих слов является омографом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Ключ (родник) - Ключ (от замка)", IsCorrect = false },
                                new TestOption { OptionText = "Замок (здание) - Замок (на двери)", IsCorrect = true },
                                new TestOption { OptionText = "Лук (растение) - Лук (оружие)", IsCorrect = false },
                                new TestOption { OptionText = "Коса (прическа) - Коса (инструмент)", IsCorrect = false }
                            }
                        }
                    }
                }
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
} 