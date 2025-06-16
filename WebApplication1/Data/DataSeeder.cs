using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data {
    public static class DataSeeder {
        public static async Task SeedData(ApplicationDbContext context) {
            await context.Database.EnsureCreatedAsync();

            // Проверяем, есть ли уже курсы
            if (await context.Courses.AnyAsync()) {
                context.TestQuestions.RemoveRange(context.TestQuestions);
                context.TestOptions.RemoveRange(context.TestOptions);
                context.Courses.RemoveRange(context.Courses);
            }

            var courses = new List<Course> {
                new() {
                    Id = 1,
                    Title = "Основы русского языка",
                    Description = "Базовый курс по грамматике и правописанию русского языка.",
                    Content =
                        "<p>В этом курсе вы изучите:</p><ul><li>Основы грамматики</li><li>Правила правописания</li><li>Пунктуацию</li><li>Основные части речи</li></ul>",
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText = "Какое слово является именем существительным?",
                            Options = [
                                new TestOption { OptionText = "Бежать", IsCorrect = false },
                                new TestOption { OptionText = "Красивый", IsCorrect = false },
                                new TestOption { OptionText = "Стол", IsCorrect = true },
                                new TestOption { OptionText = "Быстро", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является глаголом в прошедшем времени?",
                            Options = [
                                new TestOption { OptionText = "Читать", IsCorrect = false },
                                new TestOption { OptionText = "Читает", IsCorrect = false },
                                new TestOption { OptionText = "Читал", IsCorrect = true },
                                new TestOption { OptionText = "Будет читать", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое предложение является простым?",
                            Options = [
                                new TestOption { OptionText = "Солнце светило, и птицы пели.", IsCorrect = false },
                                new TestOption { OptionText = "Девочка читала книгу.", IsCorrect = true },
                                new TestOption { OptionText = "Когда я пришел, он уже спал.", IsCorrect = false },
                                new TestOption
                                    { OptionText = "Мороз крепчал, но мы не сдавались.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "В каком слове ударение падает на первый слог?",
                            Options = [
                                new TestOption { OptionText = "Красивее", IsCorrect = false },
                                new TestOption { OptionText = "Звонит", IsCorrect = false },
                                new TestOption { OptionText = "Торты", IsCorrect = true },
                                new TestOption { OptionText = "Каталог", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Как пишется слово (не)навидеть?",
                            Options = [
                                new TestOption { OptionText = "Ненавидеть", IsCorrect = true },
                                new TestOption { OptionText = "Не навидеть", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая часть речи обозначает признак предмета?",
                            Options = [
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false },
                                new TestOption { OptionText = "Прилагательное", IsCorrect = true },
                                new TestOption { OptionText = "Наречие", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "В каком слове есть приставка ПРИ-?",
                            Options = [
                                new TestOption { OptionText = "Пригород", IsCorrect = true },
                                new TestOption { OptionText = "Преодолеть", IsCorrect = false },
                                new TestOption { OptionText = "Президент", IsCorrect = false },
                                new TestOption { OptionText = "Пример", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово образовано приставочно-суффиксальным способом?",
                            Options = [
                                new TestOption { OptionText = "Подорожник", IsCorrect = true },
                                new TestOption { OptionText = "Лесник", IsCorrect = false },
                                new TestOption { OptionText = "Переход", IsCorrect = false },
                                new TestOption { OptionText = "Домик", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Укажите предложение с однородными членами. ",
                            Options = [
                                new TestOption { OptionText = "Солнце светило и грело.", IsCorrect = true },
                                new TestOption { OptionText = "Шел дождь и дул ветер.", IsCorrect = false },
                                new TestOption
                                    { OptionText = "Мы пошли в лес, чтобы собрать грибы.", IsCorrect = false },
                                new TestOption { OptionText = "Он был высоким и стройным.", IsCorrect = true }
                            ]
                        },
                        new() {
                            QuestionText = "Какое числительное является порядковым?",
                            Options = [
                                new TestOption { OptionText = "Пять", IsCorrect = false },
                                new TestOption { OptionText = "Трое", IsCorrect = false },
                                new TestOption { OptionText = "Первый", IsCorrect = true },
                                new TestOption { OptionText = "Десять", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "В каком слове НЕ пишется слитно?",
                            Options = [
                                new TestOption { OptionText = "Не был", IsCorrect = false },
                                new TestOption { OptionText = "Негодовал", IsCorrect = true },
                                new TestOption { OptionText = "Не читая", IsCorrect = false },
                                new TestOption { OptionText = "Не рад", IsCorrect = false }
                            ]
                        }
                    }
                },
                new() {
                    Id = 2,
                    Title = "Пунктуация в русском языке",
                    Description = "Подробный курс по правилам расстановки знаков препинания.",
                    Content =
                        "<p>В этом курсе вы освоите:</p><ul><li>Знаки препинания в простом и сложном предложении</li><li>Правила оформления прямой речи</li><li>Особенности пунктуации в разных стилях</li></ul>",
                    PrerequisiteCourseId = 1,
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText =
                                "Где должна стоять запятая в предложении: 'Котенок мурлыкал сидел на коленях и смотрел на огонь.'?",
                            Options = [
                                new TestOption {
                                    OptionText = "Котенок мурлыкал, сидел на коленях и смотрел на огонь.",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Котенок, мурлыкал сидел на коленях и смотрел на огонь.",
                                    IsCorrect = false
                                },

                                new TestOption {
                                    OptionText = "Котенок мурлыкал сидел на коленях, и смотрел на огонь.",
                                    IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Нужна ли запятая в предложении: 'Он решил подойти к окну чтобы рассмотреть улицу.'?",
                            Options = [
                                new TestOption { OptionText = "Да, перед 'чтобы'.", IsCorrect = true },
                                new TestOption { OptionText = "Нет, запятая не нужна.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Где нужна запятая: 'День был ясный солнечный и теплый.'?",
                            Options = [
                                new TestOption
                                    { OptionText = "День был ясный, солнечный и теплый.", IsCorrect = true },
                                new TestOption
                                    { OptionText = "День был, ясный солнечный и теплый.", IsCorrect = false },
                                new TestOption
                                    { OptionText = "День был ясный солнечный, и теплый.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "В каком случае ставится тире между подлежащим и сказуемым?",
                            Options = [
                                new TestOption {
                                    OptionText =
                                        "Если оба выражены существительными в именительном падеже и нет связки.",
                                    IsCorrect = true
                                },

                                new TestOption { OptionText = "Всегда, если есть глагол-связка.", IsCorrect = false },
                                new TestOption { OptionText = "Никогда.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите предложение с вводным словом, выделенным запятыми.",
                            Options = [
                                new TestOption { OptionText = "Кажется, дождь собирается.", IsCorrect = true },
                                new TestOption { OptionText = "Он кажется мне умным.", IsCorrect = false },
                                new TestOption { OptionText = "Дом кажется большим.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Нужна ли запятая перед 'как' в предложении: 'Он пел как соловей.'?",
                            Options = [
                                new TestOption {
                                    OptionText =
                                        "Нет, 'как соловей' - это сравнительный оборот в значении образа действия.",
                                    IsCorrect = true
                                },

                                new TestOption { OptionText = "Да, всегда.", IsCorrect = false },
                                new TestOption {
                                    OptionText = "Только если 'как' можно заменить на 'подобно'.", IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText = "Где нужна запятая: 'Мы гуляли по парку наслаждаясь свежим воздухом.'?",
                            Options = [
                                new TestOption {
                                    OptionText = "Мы гуляли по парку, наслаждаясь свежим воздухом.", IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Мы гуляли, по парку наслаждаясь свежим воздухом.", IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText = "В каком случае ставится двоеточие?",
                            Options = [
                                new TestOption
                                    { OptionText = "Перед перечислением после обобщающего слова.", IsCorrect = true },
                                new TestOption { OptionText = "Всегда перед прямой речью.", IsCorrect = false },
                                new TestOption
                                    { OptionText = "Между частями сложносочиненного предложения.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите предложение, где не нужна запятая.",
                            Options = [
                                new TestOption
                                    { OptionText = "Он был добрым и отзывчивым человеком.", IsCorrect = true },
                                new TestOption { OptionText = "Я пришел увидел и победил.", IsCorrect = false },
                                new TestOption { OptionText = "Небо было серым, и шел дождь.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое предложение является бессоюзным сложным?",
                            Options = [
                                new TestOption { OptionText = "Солнце взошло, стало жарко.", IsCorrect = true },
                                new TestOption { OptionText = "Солнце взошло, и стало жарко.", IsCorrect = false },
                                new TestOption { OptionText = "Когда солнце взошло, стало жарко.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Где нужно поставить скобки: 'Мама сказала принеси книгу пожалуйста.'?",
                            Options = [
                                new TestOption
                                    { OptionText = "Мама сказала: 'Принеси книгу, пожалуйста!'.", IsCorrect = true },
                                new TestOption
                                    { OptionText = "Мама сказала принеси книгу, пожалуйста.", IsCorrect = false }
                            ]
                        }
                    }
                },
                new() {
                    Id = 3,
                    Title = "Лексика и фразеология",
                    Description = "Курс посвящен изучению словарного состава русского языка и устойчивых выражений.",
                    Content =
                        "<p>Вы узнаете о:</p><ul><li>Лексическом значении слов</li><li>Синонимах, антонимах, омонимах</li><li>Фразеологизмах и их роли в речи</li><li>Историзмах и архаизмах</li></ul>",
                    PrerequisiteCourseId = 2,
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText = "Выберите пару синонимов.",
                            Options = [
                                new TestOption { OptionText = "Храбрый - трусливый", IsCorrect = false },
                                new TestOption { OptionText = "Быстрый - скорый", IsCorrect = true },
                                new TestOption { OptionText = "Добро - зло", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является архаизмом?",
                            Options = [
                                new TestOption { OptionText = "Очи", IsCorrect = true },
                                new TestOption { OptionText = "Глаза", IsCorrect = false },
                                new TestOption { OptionText = "Рука", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое фразеологизм?",
                            Options = [
                                new TestOption {
                                    OptionText = "Устойчивое сочетание слов с переносным значением.", IsCorrect = true
                                },

                                new TestOption { OptionText = "Слово, вышедшее из употребления.", IsCorrect = false },
                                new TestOption {
                                    OptionText = "Слова, одинаковые по звучанию, но разные по значению.",
                                    IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите антоним к слову 'начало'.",
                            Options = [
                                new TestOption { OptionText = "Конец", IsCorrect = true },
                                new TestOption { OptionText = "Старт", IsCorrect = false },
                                new TestOption { OptionText = "Исток", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое из этих слов является омонимом?",
                            Options = [
                                new TestOption
                                    { OptionText = "Коса (инструмент) - Коса (прическа)", IsCorrect = true },
                                new TestOption { OptionText = "Дом - здание", IsCorrect = false },
                                new TestOption { OptionText = "Веселый - грустный", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что означает фразеологизм 'бить баклуши'?",
                            Options = [
                                new TestOption { OptionText = "Бездельничать", IsCorrect = true },
                                new TestOption { OptionText = "Работать усердно", IsCorrect = false },
                                new TestOption { OptionText = "Шуметь", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является неологизмом?",
                            Options = [
                                new TestOption { OptionText = "Гуглить", IsCorrect = true },
                                new TestOption { OptionText = "Книга", IsCorrect = false },
                                new TestOption { OptionText = "Телефон", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите фразеологизм, синонимичный выражению 'рукой подать'.",
                            Options = [
                                new TestOption { OptionText = "Близко", IsCorrect = true },
                                new TestOption { OptionText = "Далеко", IsCorrect = false },
                                new TestOption { OptionText = "Недоступно", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является историзмом?",
                            Options = [
                                new TestOption { OptionText = "Кафтан", IsCorrect = true },
                                new TestOption { OptionText = "Пальто", IsCorrect = false },
                                new TestOption { OptionText = "Пиджак", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое синонимы?",
                            Options = [
                                new TestOption {
                                    OptionText = "Слова, разные по звучанию, но близкие по значению.", IsCorrect = true
                                },

                                new TestOption
                                    { OptionText = "Слова с противоположным значением.", IsCorrect = false },
                                new TestOption {
                                    OptionText = "Слова, одинаковые по звучанию, но разные по значению.",
                                    IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово, имеющее переносное значение.",
                            Options = [
                                new TestOption { OptionText = "Золотые руки", IsCorrect = true },
                                new TestOption { OptionText = "Золотая монета", IsCorrect = false },
                                new TestOption { OptionText = "Золотое кольцо", IsCorrect = false }
                            ]
                        }
                    }
                },
                new() {
                    Id = 4,
                    Title = "Синтаксис и морфология",
                    Description = "Курс, посвященный изучению строения предложений и словоизменения.",
                    Content =
                        "<p>Вы изучите:</p><ul><li>Части речи и члены предложения</li><li>Виды предложений</li><li>Сложные синтаксические конструкции</li><li>Морфологический разбор</li></ul>",
                    PrerequisiteCourseId = 3,
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText =
                                "Какое слово является подлежащим в предложении: 'Наступила долгожданная весна.'?",
                            Options = [
                                new TestOption { OptionText = "Наступила", IsCorrect = false },
                                new TestOption { OptionText = "Весна", IsCorrect = true },
                                new TestOption { OptionText = "Долгожданная", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой член предложения отвечает на вопросы косвенных падежей?",
                            Options = [
                                new TestOption { OptionText = "Подлежащее", IsCorrect = false },
                                new TestOption { OptionText = "Сказуемое", IsCorrect = false },
                                new TestOption { OptionText = "Дополнение", IsCorrect = true }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите сложноподчиненное предложение.",
                            Options = [
                                new TestOption { OptionText = "Я знаю, что ты придешь.", IsCorrect = true },
                                new TestOption { OptionText = "Шел дождь, и дул ветер.", IsCorrect = false },
                                new TestOption { OptionText = "Наступила зима.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое деепричастие?",
                            Options = [
                                new TestOption {
                                    OptionText = "Особая форма глагола, обозначающая добавочное действие.",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Часть речи, обозначающая признак предмета по действию.",
                                    IsCorrect = false
                                },

                                new TestOption {
                                    OptionText = "Самостоятельная часть речи, обозначающая действие.", IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText = "В каком предложении есть обособленное определение?",
                            Options = [
                                new TestOption {
                                    OptionText = "Над домом, построенным отцом, кружились ласточки.", IsCorrect = true
                                },

                                new TestOption
                                    { OptionText = "Построенный дом выглядел красиво.", IsCorrect = false },
                                new TestOption { OptionText = "Дом был построен давно.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая часть речи изменяется по падежам?",
                            Options = [
                                new TestOption { OptionText = "Существительное", IsCorrect = true },
                                new TestOption { OptionText = "Глагол", IsCorrect = false },
                                new TestOption { OptionText = "Наречие", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое синтаксис?",
                            Options = [
                                new TestOption {
                                    OptionText = "Раздел языкознания, изучающий строение словосочетаний и предложений.",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Раздел языкознания, изучающий словарный состав языка.",
                                    IsCorrect = false
                                },

                                new TestOption
                                    { OptionText = "Раздел языкознания, изучающий звуки речи.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое предложение является безличным?",
                            Options = [
                                new TestOption { OptionText = "На улице темнеет.", IsCorrect = true },
                                new TestOption { OptionText = "Мы гуляем в парке.", IsCorrect = false },
                                new TestOption { OptionText = "Он читает книгу.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой тип связи в словосочетании 'читать книгу'?",
                            Options = [
                                new TestOption { OptionText = "Управление", IsCorrect = true },
                                new TestOption { OptionText = "Согласование", IsCorrect = false },
                                new TestOption { OptionText = "Примыкание", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая часть речи не изменяется?",
                            Options = [
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false }
                            ]
                        }
                    }
                },
                new() {
                    Id = 5,
                    Title = "Стилистика и культура речи",
                    Description =
                        "Курс для повышения уровня владения русским языком и развития коммуникативных навыков.",
                    Content =
                        "<p>Вы научитесь:</p><ul><li>Различать функциональные стили речи</li><li>Использовать языковые средства выразительности</li><li>Избегать речевых ошибок</li><li>Повышать культуру устной и письменной речи</li></ul>",
                    PrerequisiteCourseId = 4,
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText = "Какой стиль речи используется в научных статьях?",
                            Options = [
                                new TestOption { OptionText = "Научный", IsCorrect = true },
                                new TestOption { OptionText = "Разговорный", IsCorrect = false },
                                new TestOption { OptionText = "Публицистический", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое метафора?",
                            Options = [
                                new TestOption {
                                    OptionText =
                                        "Скрытое сравнение, основанное на переносе свойств одного предмета на другой.",
                                    IsCorrect = true
                                },

                                new TestOption { OptionText = "Прямое сравнение.", IsCorrect = false },
                                new TestOption { OptionText = "Преувеличение.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите предложение, написанное в официально-деловом стиле.",
                            Options = [
                                new TestOption {
                                    OptionText = "Просим вас предоставить необходимые документы в срок до 1 декабря.",
                                    IsCorrect = true
                                },

                                new TestOption { OptionText = "Как дела?", IsCorrect = false },
                                new TestOption
                                    { OptionText = "Сегодня на улице прекрасная погода.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Какое выразительное средство используется в предложении: 'Москва златоглавая'?",
                            Options = [
                                new TestOption { OptionText = "Эпитет", IsCorrect = true },
                                new TestOption { OptionText = "Метафора", IsCorrect = false },
                                new TestOption { OptionText = "Сравнение", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что означает понятие 'речевая избыточность'?",
                            Options = [
                                new TestOption {
                                    OptionText = "Использование лишних слов, дублирующих смысл.", IsCorrect = true
                                },
                                new TestOption { OptionText = "Недостаток слов в речи.", IsCorrect = false },
                                new TestOption
                                    { OptionText = "Использование слов в переносном значении.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой функциональный стиль используется в художественной литературе?",
                            Options = [
                                new TestOption { OptionText = "Художественный", IsCorrect = true },
                                new TestOption { OptionText = "Публицистический", IsCorrect = false },
                                new TestOption { OptionText = "Разговорный", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое оксюморон?",
                            Options = [
                                new TestOption {
                                    OptionText = "Сочетание несовместимых по смыслу слов (например, 'живой труп').",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Повторение одного и того же слова или фразы.", IsCorrect = false
                                },
                                new TestOption
                                    { OptionText = "Сравнение двух предметов по сходству.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая из перечисленных ошибок является лексической?",
                            Options = [
                                new TestOption { OptionText = "Плеоназм (масло масляное)", IsCorrect = true },
                                new TestOption { OptionText = "Неправильное окончание слова.", IsCorrect = false },
                                new TestOption { OptionText = "Отсутствие запятой.", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое эвфемизм?",
                            Options = [
                                new TestOption {
                                    OptionText = "Смягченное выражение, заменяющее грубое или неприличное слово.",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Употребление слова в противоположном значении.", IsCorrect = false
                                },

                                new TestOption {
                                    OptionText = "Вопросительное предложение, не требующее ответа.", IsCorrect = false
                                }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Какое выразительное средство используется в предложении: 'Молчит золото, говорит серебро.'?",
                            Options = [
                                new TestOption { OptionText = "Антитеза", IsCorrect = true },
                                new TestOption { OptionText = "Сравнение", IsCorrect = false },
                                new TestOption { OptionText = "Гипербола", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое парцелляция?",
                            Options = [
                                new TestOption {
                                    OptionText =
                                        "Разделение предложения на несколько частей для усиления выразительности.",
                                    IsCorrect = true
                                },

                                new TestOption {
                                    OptionText = "Объединение нескольких простых предложений в одно сложное.",
                                    IsCorrect = false
                                },

                                new TestOption
                                    { OptionText = "Использование только коротких предложений.", IsCorrect = false }
                            ]
                        }
                    }
                },
                new() {
                    Id = 6,
                    Title = "Итоговый курс по русскому языку",
                    Description = "Итоговый курс для проверки всех полученных знаний и получения сертификата.",
                    Content =
                        "<p>Этот курс охватывает все темы, изученные в предыдущих курсах. Успешное прохождение теста открывает доступ к сертификату.</p>",
                    PrerequisiteCourseId = 5,
                    IsFinalCourse = true,
                    TestQuestions = new List<TestQuestion> {
                        new() {
                            QuestionText = "Укажите однокоренные слова",
                            Options = [
                                new TestOption { OptionText = "Дом — домой", IsCorrect = false },
                                new TestOption { OptionText = "Лес — лесной", IsCorrect = true },
                                new TestOption { OptionText = "Кот — котик", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой падеж у слова 'по лесу'?",
                            Options = [
                                new TestOption { OptionText = "Дательный", IsCorrect = false },
                                new TestOption { OptionText = "Предложный", IsCorrect = false },
                                new TestOption { OptionText = "Творительный", IsCorrect = true }
                            ]
                        },
                        new() {
                            QuestionText = "Укажите слово с приставкой 'раз-'",
                            Options = [
                                new TestOption { OptionText = "Развиваться", IsCorrect = false },
                                new TestOption { OptionText = "Разбить", IsCorrect = true },
                                new TestOption { OptionText = "Распись", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что означает фразеологизм 'бить баклуши'?",
                            Options = [
                                new TestOption { OptionText = "Бездельничать", IsCorrect = true },
                                new TestOption { OptionText = "Ссориться", IsCorrect = false },
                                new TestOption { OptionText = "Играть", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что является антонимом к слову 'высокий'?",
                            Options = [
                                new TestOption { OptionText = "Низкий", IsCorrect = true },
                                new TestOption { OptionText = "Длинный", IsCorrect = false },
                                new TestOption { OptionText = "Широкий", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Как пишется слово с НЕ: 'не интересный'?",
                            Options = [
                                new TestOption { OptionText = "Неинтересный", IsCorrect = true },
                                new TestOption { OptionText = "Не интересный", IsCorrect = false },
                                new TestOption { OptionText = "Не интереснный", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что из перечисленного является союзом?",
                            Options = [
                                new TestOption { OptionText = "И", IsCorrect = true },
                                new TestOption { OptionText = "Он", IsCorrect = false },
                                new TestOption { OptionText = "Книга", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Найдите существительное",
                            Options = [
                                new TestOption { OptionText = "Стол", IsCorrect = true },
                                new TestOption { OptionText = "Бежать", IsCorrect = false },
                                new TestOption { OptionText = "Красивый", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая часть речи 'быстро'?",
                            Options = [
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Прилагательное", IsCorrect = false },
                                new TestOption { OptionText = "Существительное", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой предлог употребляется с творительным падежом?",
                            Options = [
                                new TestOption { OptionText = "С", IsCorrect = true },
                                new TestOption { OptionText = "В", IsCorrect = false },
                                new TestOption { OptionText = "На", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Как правильно образовать сравнительную степень прилагательного 'добрый'?",
                            Options = [
                                new TestOption { OptionText = "Добрей", IsCorrect = true },
                                new TestOption { OptionText = "Добрейший", IsCorrect = false },
                                new TestOption { OptionText = "Более добрый", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Укажите верную форму слова в родительном падеже множественного числа: 'стул'.",
                            Options = [
                                new TestOption { OptionText = "Стульев", IsCorrect = true },
                                new TestOption { OptionText = "Стулов", IsCorrect = false },
                                new TestOption { OptionText = "Стуль", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая часть речи изменяется по временам?",
                            Options = [
                                new TestOption { OptionText = "Глагол", IsCorrect = true },
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Прилагательное", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что из нижеперечисленного — деепричастие?",
                            Options = [
                                new TestOption { OptionText = "Сделав", IsCorrect = true },
                                new TestOption { OptionText = "Сделает", IsCorrect = false },
                                new TestOption { OptionText = "Сделанный", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что означает фразеологизм 'как в воду канул'?",
                            Options = [
                                new TestOption { OptionText = "Пропал без вести", IsCorrect = true },
                                new TestOption { OptionText = "Упал в воду", IsCorrect = false },
                                new TestOption { OptionText = "Промок", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'вчера'?",
                            Options = [
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Местоимение", IsCorrect = false },
                                new TestOption { OptionText = "Предлог", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово, в котором пишется Ь.",
                            Options = [
                                new TestOption { OptionText = "Мельник", IsCorrect = true },
                                new TestOption { OptionText = "Певчий", IsCorrect = false },
                                new TestOption { OptionText = "Гусар", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "В каком слове пишется приставка С-?",
                            Options = [
                                new TestOption { OptionText = "Сделать", IsCorrect = true },
                                new TestOption { OptionText = "Списывать", IsCorrect = false },
                                new TestOption { OptionText = "Сходить", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'их'?",
                            Options = [
                                new TestOption { OptionText = "Местоимение", IsCorrect = true },
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Предлог", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой предлог употребляется с винительным падежом?",
                            Options = [
                                new TestOption { OptionText = "На", IsCorrect = true },
                                new TestOption { OptionText = "От", IsCorrect = false },
                                new TestOption { OptionText = "При", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'первый'?",
                            Options = [
                                new TestOption { OptionText = "Числительное", IsCorrect = true },
                                new TestOption { OptionText = "Существительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является вводным?",
                            Options = [
                                new TestOption { OptionText = "Конечно", IsCorrect = true },
                                new TestOption { OptionText = "Быстро", IsCorrect = false },
                                new TestOption { OptionText = "Очень", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите правильное написание слова:",
                            Options = [
                                new TestOption { OptionText = "ПрИгород", IsCorrect = true },
                                new TestOption { OptionText = "ПрЕгород", IsCorrect = false },
                                new TestOption { OptionText = "ПРигород", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое омонимы?",
                            Options = [
                                new TestOption {
                                    OptionText = "Слова, одинаково звучащие, но разные по значению", IsCorrect = true
                                },
                                new TestOption
                                    { OptionText = "Слова с противоположным значением", IsCorrect = false },
                                new TestOption { OptionText = "Слова с одинаковым значением", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой корень в слове 'водитель'?",
                            Options = [
                                new TestOption { OptionText = "вод", IsCorrect = true },
                                new TestOption { OptionText = "води", IsCorrect = false },
                                new TestOption { OptionText = "водител", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово является антонимом к слову 'грусть'?",
                            Options = [
                                new TestOption { OptionText = "Радость", IsCorrect = true },
                                new TestOption { OptionText = "Скорбь", IsCorrect = false },
                                new TestOption { OptionText = "Слеза", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Какой знак препинания нужен в предложении: 'Когда он вошёл ___ все замолчали'?",
                            Options = [
                                new TestOption { OptionText = "Запятая", IsCorrect = true },
                                new TestOption { OptionText = "Точка", IsCorrect = false },
                                new TestOption { OptionText = "Точка с запятой", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово с чередующейся гласной в корне",
                            Options = [
                                new TestOption { OptionText = "Пловец", IsCorrect = true },
                                new TestOption { OptionText = "Плавать", IsCorrect = false },
                                new TestOption { OptionText = "Поплавок", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая форма слова 'лист' во множественном числе?",
                            Options = [
                                new TestOption { OptionText = "Листы", IsCorrect = true },
                                new TestOption { OptionText = "Листья", IsCorrect = false },
                                new TestOption { OptionText = "Листов", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что обозначает приставка 'пре-'?",
                            Options = [
                                new TestOption { OptionText = "Очень", IsCorrect = true },
                                new TestOption { OptionText = "Приближение", IsCorrect = false },
                                new TestOption { OptionText = "Противоположность", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'они'?",
                            Options = [
                                new TestOption { OptionText = "Местоимение", IsCorrect = true },
                                new TestOption { OptionText = "Союз", IsCorrect = false },
                                new TestOption { OptionText = "Предлог", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово с приставкой",
                            Options = [
                                new TestOption { OptionText = "Подъезд", IsCorrect = true },
                                new TestOption { OptionText = "Яблоко", IsCorrect = false },
                                new TestOption { OptionText = "Море", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово с суффиксом -ик?",
                            Options = [
                                new TestOption { OptionText = "Домик", IsCorrect = true },
                                new TestOption { OptionText = "Дом", IsCorrect = false },
                                new TestOption { OptionText = "Домовой", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово с проверяемой безударной гласной",
                            Options = [
                                new TestOption { OptionText = "Гора", IsCorrect = true },
                                new TestOption { OptionText = "Школа", IsCorrect = false },
                                new TestOption { OptionText = "Окно", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'семеро'?",
                            Options = [
                                new TestOption { OptionText = "Числительное", IsCorrect = true },
                                new TestOption { OptionText = "Местоимение", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Укажите правильный вариант написания слова",
                            Options = [
                                new TestOption { OptionText = "Бессердечный", IsCorrect = true },
                                new TestOption { OptionText = "Бессердечный", IsCorrect = false },
                                new TestOption { OptionText = "Безсердечный", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какая форма глагола прошедшего времени женского рода?",
                            Options = [
                                new TestOption { OptionText = "Сделала", IsCorrect = true },
                                new TestOption { OptionText = "Сделал", IsCorrect = false },
                                new TestOption { OptionText = "Сделали", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово с чередующейся гласной в корне",
                            Options = [
                                new TestOption { OptionText = "Косить", IsCorrect = true },
                                new TestOption { OptionText = "Косой", IsCorrect = false },
                                new TestOption { OptionText = "Коса", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'самостоятельно'?",
                            Options = [
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Глагол", IsCorrect = false },
                                new TestOption { OptionText = "Существительное", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой глагол стоит в будущем времени?",
                            Options = [
                                new TestOption { OptionText = "Буду читать", IsCorrect = true },
                                new TestOption { OptionText = "Читал", IsCorrect = false },
                                new TestOption { OptionText = "Читаю", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой глагол возвратный?",
                            Options = [
                                new TestOption { OptionText = "Улыбаться", IsCorrect = true },
                                new TestOption { OptionText = "Улыбить", IsCorrect = false },
                                new TestOption { OptionText = "Улыбка", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Выберите слово с приставкой ПРИ- в значении 'приближение'",
                            Options = [
                                new TestOption { OptionText = "Приехать", IsCorrect = true },
                                new TestOption { OptionText = "Приоткрыть", IsCorrect = false },
                                new TestOption { OptionText = "Приукрасить", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой союз соединительный?",
                            Options = [
                                new TestOption { OptionText = "И", IsCorrect = true },
                                new TestOption { OptionText = "Хотя", IsCorrect = false },
                                new TestOption { OptionText = "Если", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Что такое синонимы?",
                            Options = [
                                new TestOption
                                    { OptionText = "Слова с одинаковым или близким значением", IsCorrect = true },
                                new TestOption
                                    { OptionText = "Слова с противоположным значением", IsCorrect = false },
                                new TestOption { OptionText = "Слова с одним корнем", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой частью речи является слово 'дважды'?",
                            Options = [
                                new TestOption { OptionText = "Наречие", IsCorrect = true },
                                new TestOption { OptionText = "Числительное", IsCorrect = false },
                                new TestOption { OptionText = "Глагол", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какой суффикс используется для образования существительного от глагола?",
                            Options = [
                                new TestOption { OptionText = "-ние", IsCorrect = true },
                                new TestOption { OptionText = "-оват", IsCorrect = false },
                                new TestOption { OptionText = "-еньк", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое из слов пишется через дефис?",
                            Options = [
                                new TestOption { OptionText = "По-русски", IsCorrect = true },
                                new TestOption { OptionText = "Вроде бы", IsCorrect = false },
                                new TestOption { OptionText = "Тоже", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText =
                                "Какой знак препинания ставится перед 'что' в сложноподчинённом предложении?",
                            Options = [
                                new TestOption { OptionText = "Запятая", IsCorrect = true },
                                new TestOption { OptionText = "Точка с запятой", IsCorrect = false },
                                new TestOption { OptionText = "Двоеточие", IsCorrect = false }
                            ]
                        },
                        new() {
                            QuestionText = "Какое слово лишнее по значению?",
                            Options = [
                                new TestOption { OptionText = "Река", IsCorrect = true },
                                new TestOption { OptionText = "Озеро", IsCorrect = false },
                                new TestOption { OptionText = "Море", IsCorrect = false }
                            ]
                        }
                    }
                }
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}