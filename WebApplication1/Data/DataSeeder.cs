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
                    Content = "<p>В этом курсе вы изучите:</p><ul><li>Основы грамматики</li><li>Правила правописания</li><li>Пунктуацию</li><li>Основные части речи</li></ul>",
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
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое предложение является простым?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Солнце светило, и птицы пели.", IsCorrect = false },
                                new TestOption { OptionText = "Девочка читала книгу.", IsCorrect = true },
                                new TestOption { OptionText = "Когда я пришел, он уже спал.", IsCorrect = false },
                                new TestOption { OptionText = "Мороз крепчал, но мы не сдавались.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком слове ударение падает на первый слог?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Красивее", IsCorrect = false },
                                new TestOption { OptionText = "Звонит", IsCorrect = false },
                                new TestOption { OptionText = "Торты", IsCorrect = true },
                                new TestOption { OptionText = "Каталог", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Как пишется слово (не)навидеть?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Ненавидеть", IsCorrect = true },
                                new TestOption { OptionText = "Не навидеть", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какая часть речи обозначает признак предмета?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false },
                                new TestOption { OptionText = "Прилагательное", IsCorrect = true },
                                new TestOption { OptionText = "Наречие", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком слове есть приставка ПРИ-?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Пригород", IsCorrect = true },
                                new TestOption { OptionText = "Преодолеть", IsCorrect = false },
                                new TestOption { OptionText = "Президент", IsCorrect = false },
                                new TestOption { OptionText = "Пример", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое слово образовано приставочно-суффиксальным способом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Подорожник", IsCorrect = true },
                                new TestOption { OptionText = "Лесник", IsCorrect = false },
                                new TestOption { OptionText = "Переход", IsCorrect = false },
                                new TestOption { OptionText = "Домик", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Укажите предложение с однородными членами. ",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Солнце светило и грело.", IsCorrect = true },
                                new TestOption { OptionText = "Шел дождь и дул ветер.", IsCorrect = false },
                                new TestOption { OptionText = "Мы пошли в лес, чтобы собрать грибы.", IsCorrect = false },
                                new TestOption { OptionText = "Он был высоким и стройным.", IsCorrect = true } 
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое числительное является порядковым?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Пять", IsCorrect = false },
                                new TestOption { OptionText = "Трое", IsCorrect = false },
                                new TestOption { OptionText = "Первый", IsCorrect = true },
                                new TestOption { OptionText = "Десять", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком слове НЕ пишется слитно?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Не был", IsCorrect = false },
                                new TestOption { OptionText = "Негодовал", IsCorrect = true },
                                new TestOption { OptionText = "Не читая", IsCorrect = false },
                                new TestOption { OptionText = "Не рад", IsCorrect = false }
                            }
                        }
                    }
                },
                new Course
                {
                    Title = "Пунктуация в русском языке",
                    Description = "Подробный курс по правилам расстановки знаков препинания.",
                    Content = "<p>В этом курсе вы освоите:</p><ul><li>Знаки препинания в простом и сложном предложении</li><li>Правила оформления прямой речи</li><li>Особенности пунктуации в разных стилях</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "Где должна стоять запятая в предложении: 'Котенок мурлыкал сидел на коленях и смотрел на огонь.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Котенок мурлыкал, сидел на коленях и смотрел на огонь.", IsCorrect = true },
                                new TestOption { OptionText = "Котенок, мурлыкал сидел на коленях и смотрел на огонь.", IsCorrect = false },
                                new TestOption { OptionText = "Котенок мурлыкал сидел на коленях, и смотрел на огонь.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Нужна ли запятая в предложении: 'Он решил подойти к окну чтобы рассмотреть улицу.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Да, перед 'чтобы'.", IsCorrect = true },
                                new TestOption { OptionText = "Нет, запятая не нужна.", IsCorrect = false }
                            }
                        },
                         new TestQuestion
                        {
                            QuestionText = "Где нужна запятая: 'День был ясный солнечный и теплый.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "День был ясный, солнечный и теплый.", IsCorrect = true },
                                new TestOption { OptionText = "День был, ясный солнечный и теплый.", IsCorrect = false },
                                new TestOption { OptionText = "День был ясный солнечный, и теплый.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком случае ставится тире между подлежащим и сказуемым?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Если оба выражены существительными в именительном падеже и нет связки.", IsCorrect = true },
                                new TestOption { OptionText = "Всегда, если есть глагол-связка.", IsCorrect = false },
                                new TestOption { OptionText = "Никогда.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите предложение с вводным словом, выделенным запятыми.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Кажется, дождь собирается.", IsCorrect = true },
                                new TestOption { OptionText = "Он кажется мне умным.", IsCorrect = false },
                                new TestOption { OptionText = "Дом кажется большим.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Нужна ли запятая перед 'как' в предложении: 'Он пел как соловей.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Нет, 'как соловей' - это сравнительный оборот в значении образа действия.", IsCorrect = true },
                                new TestOption { OptionText = "Да, всегда.", IsCorrect = false },
                                new TestOption { OptionText = "Только если 'как' можно заменить на 'подобно'.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Где нужна запятая: 'Мы гуляли по парку наслаждаясь свежим воздухом.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Мы гуляли по парку, наслаждаясь свежим воздухом.", IsCorrect = true },
                                new TestOption { OptionText = "Мы гуляли, по парку наслаждаясь свежим воздухом.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком случае ставится двоеточие?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Перед перечислением после обобщающего слова.", IsCorrect = true },
                                new TestOption { OptionText = "Всегда перед прямой речью.", IsCorrect = false },
                                new TestOption { OptionText = "Между частями сложносочиненного предложения.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите предложение, где не нужна запятая.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Он был добрым и отзывчивым человеком.", IsCorrect = true },
                                new TestOption { OptionText = "Я пришел увидел и победил.", IsCorrect = false },
                                new TestOption { OptionText = "Небо было серым, и шел дождь.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое предложение является бессоюзным сложным?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Солнце взошло, стало жарко.", IsCorrect = true },
                                new TestOption { OptionText = "Солнце взошло, и стало жарко.", IsCorrect = false },
                                new TestOption { OptionText = "Когда солнце взошло, стало жарко.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Где нужно поставить скобки: 'Мама сказала принеси книгу пожалуйста.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Мама сказала: 'Принеси книгу, пожалуйста!'.", IsCorrect = true },
                                new TestOption { OptionText = "Мама сказала принеси книгу, пожалуйста.", IsCorrect = false }
                            }
                        }
                    }
                },
                new Course
                {
                    Title = "Лексика и фразеология",
                    Description = "Курс посвящен изучению словарного состава русского языка и устойчивых выражений.",
                    Content = "<p>Вы узнаете о:</p><ul><li>Лексическом значении слов</li><li>Синонимах, антонимах, омонимах</li><li>Фразеологизмах и их роли в речи</li><li>Историзмах и архаизмах</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "Выберите пару синонимов.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Храбрый - трусливый", IsCorrect = false },
                                new TestOption { OptionText = "Быстрый - скорый", IsCorrect = true },
                                new TestOption { OptionText = "Добро - зло", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является архаизмом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Очи", IsCorrect = true },
                                new TestOption { OptionText = "Глаза", IsCorrect = false },
                                new TestOption { OptionText = "Рука", IsCorrect = false }
                            }
                        },
                         new TestQuestion
                        {
                            QuestionText = "Что такое фразеологизм?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Устойчивое сочетание слов с переносным значением.", IsCorrect = true },
                                new TestOption { OptionText = "Слово, вышедшее из употребления.", IsCorrect = false },
                                new TestOption { OptionText = "Слова, одинаковые по звучанию, но разные по значению.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите антоним к слову 'начало'.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Конец", IsCorrect = true },
                                new TestOption { OptionText = "Старт", IsCorrect = false },
                                new TestOption { OptionText = "Исток", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое из этих слов является омонимом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Коса (инструмент) - Коса (прическа)", IsCorrect = true },
                                new TestOption { OptionText = "Дом - здание", IsCorrect = false },
                                new TestOption { OptionText = "Веселый - грустный", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что означает фразеологизм 'бить баклуши'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Бездельничать", IsCorrect = true },
                                new TestOption { OptionText = "Работать усердно", IsCorrect = false },
                                new TestOption { OptionText = "Шуметь", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является неологизмом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Гуглить", IsCorrect = true },
                                new TestOption { OptionText = "Книга", IsCorrect = false },
                                new TestOption { OptionText = "Телефон", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите фразеологизм, синонимичный выражению 'рукой подать'.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Близко", IsCorrect = true },
                                new TestOption { OptionText = "Далеко", IsCorrect = false },
                                new TestOption { OptionText = "Недоступно", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является историзмом?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Кафтан", IsCorrect = true },
                                new TestOption { OptionText = "Пальто", IsCorrect = false },
                                new TestOption { OptionText = "Пиджак", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое синонимы?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Слова, разные по звучанию, но близкие по значению.", IsCorrect = true },
                                new TestOption { OptionText = "Слова с противоположным значением.", IsCorrect = false },
                                new TestOption { OptionText = "Слова, одинаковые по звучанию, но разные по значению.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите слово, имеющее переносное значение.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Золотые руки", IsCorrect = true },
                                new TestOption { OptionText = "Золотая монета", IsCorrect = false },
                                new TestOption { OptionText = "Золотое кольцо", IsCorrect = false }
                            }
                        }
                    }
                },
                new Course
                {
                    Title = "Синтаксис и морфология",
                    Description = "Курс, посвященный изучению строения предложений и словоизменения.",
                    Content = "<p>Вы изучите:</p><ul><li>Части речи и члены предложения</li><li>Виды предложений</li><li>Сложные синтаксические конструкции</li><li>Морфологический разбор</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "Какое слово является подлежащим в предложении: 'Наступила долгожданная весна.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Наступила", IsCorrect = false },
                                new TestOption { OptionText = "Весна", IsCorrect = true },
                                new TestOption { OptionText = "Долгожданная", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какой член предложения отвечает на вопросы косвенных падежей?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Подлежащее", IsCorrect = false },
                                new TestOption { OptionText = "Сказуемое", IsCorrect = false },
                                new TestOption { OptionText = "Дополнение", IsCorrect = true }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите сложноподчиненное предложение.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Я знаю, что ты придешь.", IsCorrect = true },
                                new TestOption { OptionText = "Шел дождь, и дул ветер.", IsCorrect = false },
                                new TestOption { OptionText = "Наступила зима.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое деепричастие?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Особая форма глагола, обозначающая добавочное действие.", IsCorrect = true },
                                new TestOption { OptionText = "Часть речи, обозначающая признак предмета по действию.", IsCorrect = false },
                                new TestOption { OptionText = "Самостоятельная часть речи, обозначающая действие.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "В каком предложении есть обособленное определение?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Над домом, построенным отцом, кружились ласточки.", IsCorrect = true },
                                new TestOption { OptionText = "Построенный дом выглядел красиво.", IsCorrect = false },
                                new TestOption { OptionText = "Дом был построен давно.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какая часть речи изменяется по падежам?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Существительное", IsCorrect = true },
                                new TestOption { OptionText = "Глагол", IsCorrect = false },
                                new TestOption { OptionText = "Наречие", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое синтаксис?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Раздел языкознания, изучающий строение словосочетаний и предложений.", IsCorrect = true },
                                new TestOption { OptionText = "Раздел языкознания, изучающий словарный состав языка.", IsCorrect = false },
                                new TestOption { OptionText = "Раздел языкознания, изучающий звуки речи.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое предложение является безличным?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "На улице темнеет.", IsCorrect = true },
                                new TestOption { OptionText = "Мы гуляем в парке.", IsCorrect = false },
                                new TestOption { OptionText = "Он читает книгу.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какой тип связи в словосочетании 'читать книгу'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Управление", IsCorrect = true },
                                new TestOption { OptionText = "Согласование", IsCorrect = false },
                                new TestOption { OptionText = "Примыкание", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какая часть речи не изменяется?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false }
                            }
                        }
                    }
                },
                new Course
                {
                    Title = "Стилистика и культура речи",
                    Description = "Курс для повышения уровня владения русским языком и развития коммуникативных навыков.",
                    Content = "<p>Вы научитесь:</p><ul><li>Различать функциональные стили речи</li><li>Использовать языковые средства выразительности</li><li>Избегать речевых ошибок</li><li>Повышать культуру устной и письменной речи</li></ul>",
                    TestQuestions = new List<TestQuestion>
                    {
                        new TestQuestion
                        {
                            QuestionText = "Какой стиль речи используется в научных статьях?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Научный", IsCorrect = true },
                                new TestOption { OptionText = "Разговорный", IsCorrect = false },
                                new TestOption { OptionText = "Публицистический", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое метафора?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Скрытое сравнение, основанное на переносе свойств одного предмета на другой.", IsCorrect = true },
                                new TestOption { OptionText = "Прямое сравнение.", IsCorrect = false },
                                new TestOption { OptionText = "Преувеличение.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Выберите предложение, написанное в официально-деловом стиле.",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Просим вас предоставить необходимые документы в срок до 1 декабря.", IsCorrect = true },
                                new TestOption { OptionText = "Как дела?", IsCorrect = false },
                                new TestOption { OptionText = "Сегодня на улице прекрасная погода.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое выразительное средство используется в предложении: 'Москва златоглавая'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Эпитет", IsCorrect = true },
                                new TestOption { OptionText = "Метафора", IsCorrect = false },
                                new TestOption { OptionText = "Сравнение", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что означает понятие 'речевая избыточность'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Использование лишних слов, дублирующих смысл.", IsCorrect = true },
                                new TestOption { OptionText = "Недостаток слов в речи.", IsCorrect = false },
                                new TestOption { OptionText = "Использование слов в переносном значении.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какой функциональный стиль используется в художественной литературе?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Художественный", IsCorrect = true },
                                new TestOption { OptionText = "Публицистический", IsCorrect = false },
                                new TestOption { OptionText = "Разговорный", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое оксюморон?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Сочетание несовместимых по смыслу слов (например, 'живой труп').", IsCorrect = true },
                                new TestOption { OptionText = "Повторение одного и того же слова или фразы.", IsCorrect = false },
                                new TestOption { OptionText = "Сравнение двух предметов по сходству.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какая из перечисленных ошибок является лексической?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Плеоназм (масло масляное)", IsCorrect = true },
                                new TestOption { OptionText = "Неправильное окончание слова.", IsCorrect = false },
                                new TestOption { OptionText = "Отсутствие запятой.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое эвфемизм?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Смягченное выражение, заменяющее грубое или неприличное слово.", IsCorrect = true },
                                new TestOption { OptionText = "Употребление слова в противоположном значении.", IsCorrect = false },
                                new TestOption { OptionText = "Вопросительное предложение, не требующее ответа.", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Какое выразительное средство используется в предложении: 'Молчит золото, говорит серебро.'?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Антитеза", IsCorrect = true },
                                new TestOption { OptionText = "Сравнение", IsCorrect = false },
                                new TestOption { OptionText = "Гипербола", IsCorrect = false }
                            }
                        },
                        new TestQuestion
                        {
                            QuestionText = "Что такое парцелляция?",
                            Options = new List<TestOption>
                            {
                                new TestOption { OptionText = "Разделение предложения на несколько частей для усиления выразительности.", IsCorrect = true },
                                new TestOption { OptionText = "Объединение нескольких простых предложений в одно сложное.", IsCorrect = false },
                                new TestOption { OptionText = "Использование только коротких предложений.", IsCorrect = false }
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