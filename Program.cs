using System;
using System.IO;

namespace Matrix_Calculator
{
    // Доп. функционал - решение СЛАУ методом Гаусса и нахождение ранга матрицы.

    class Program
    {
        static readonly string nl = Environment.NewLine;
        static char separator = Path.DirectorySeparatorChar;

        static void Main(string[] args)
        {
            Introduction();
            CommandLine();
        }

        /// <summary>
        /// Реализация повтора решений.
        /// </summary>
        static void CommandLine()
        {
            while (true)
                GetCommand();
        }

        /// <summary>
        /// Получение команды от пользователя.
        /// </summary>
        static void GetCommand()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(nl + "Команда: ");
            Console.ResetColor();
            string command = Console.ReadLine();

            // Переход к выполнению команды.
            bool rightCommand = GoToCommand(command);
            // Проверка корректности ввода.
            while (!rightCommand)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введённой команды не существует!");
                Console.ResetColor();
                Console.Write("Повторите попытку: ");

                command = Console.ReadLine();
                rightCommand = GoToCommand(command);
            }
        }

        /// <summary>
        /// Переход к выполнению указанной команды.
        /// </summary>
        /// <param name="command">введённая команда</param>
        /// <returns></returns>
        static bool GoToCommand(string command)
        {
            // Переход к выполнению введённой операции.
            switch (command)
            {
                case "trace": TraceCommand(); return true;
                case "transpose": TransposeCommand(); return true;
                case "sum": SumCommand(); return true;
                case "dif": DifCommand(); return true;
                case "product": ProductCommand(); return true;
                case "multi": MultiCommand(); return true;
                case "det": DetCommand(); return true;
                case "rank": RankCommand(); return true;
                case "SLAE": SLAECommand(); return true;
                case "help": HelpCommand(); return true;
                case "quit": QuitCommand(); return true;
                default: return false;
            }
        }

        /// <summary>
        /// Нахождение следа матрицы.
        /// </summary>
        static void TraceCommand()
        {
            Console.WriteLine(nl + "Найдём след необходимой матрицы.");

            int rows = 0, columns = 0;
            // Получение порядка матрицы и самой матрицы.
            GetSquareDensity(ref rows, ref columns);
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");

            int sum = 0;
            // Вычисление суммы элементов на главной диагонали.
            for (int row = 0; row < rows; ++row)
                for (int col = 0; col < columns; ++col)
                    if (row == col)
                        sum += matrix[row, col];

            Console.WriteLine(nl + "След матрицы: " + sum);
            Console.ResetColor();

            CommandLine();
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        static void TransposeCommand()
        {
            Console.WriteLine(nl + "Транспонируем необходимую матрицу.");

            int rows = 0, columns = 0;
            // Получение размерности матрицы и самой матрицы.
            GetCommonDensity(ref rows, ref columns);
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");

            Console.WriteLine(nl + "Транспонированная матрица:");
            Console.ResetColor();
            // Вывод массива по столбцам.
            for (int col = 0; col < columns; ++col)
            {
                for (int row = 0; row < rows; ++row)
                    Console.Write(matrix[row, col] + "\t");
                Console.Write(nl);
            }

            CommandLine();
        }

        /// <summary>
        /// Нахождение суммы двух матриц.
        /// </summary>
        static void SumCommand()
        {
            Console.WriteLine(nl + "Сложим две матрицы." + nl +
                "Матрицы должны быть одинаковой размерности.");

            int rows = 0, columns = 0;
            // Получение размерности матриц.
            GetCommonDensity(ref rows, ref columns);

            // Получение матрицы А.
            Console.WriteLine(nl + "Необходимо получить матрицу А.");
            int[,] matrix1 = GetMatrix(rows, columns);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица А получена успешно!");
            Console.ResetColor();

            // Получение матрицы В.
            Console.WriteLine(nl + "Необходимо получить матрицу В.");
            int[,] matrix2 = GetMatrix(rows, columns);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица В получена успешно!");

            Console.WriteLine(nl + "Сумма матриц А и В - матрица С:");
            Console.ResetColor();
            // Вывод сумм соответствующих элементов массивов.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                    Console.Write(matrix1[row, col] + matrix2[row, col] + "\t");
                Console.Write(nl);
            }

            CommandLine();
        }

        /// <summary>
        /// Нахождение разности двух матриц.
        /// </summary>
        static void DifCommand()
        {
            Console.WriteLine(nl + "Найдём разность двух матриц." + nl +
                "Матрицы должны быть одинаковой размерности.");

            int rows = 0, columns = 0;
            // Получение размерности матриц.
            GetCommonDensity(ref rows, ref columns);

            // Получение матрицы А.
            Console.WriteLine(nl + "Необходимо получить матрицу А.");
            int[,] matrix1 = GetMatrix(rows, columns);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица А получена успешно!");
            Console.ResetColor();

            // Получение матрицы В.
            Console.WriteLine(nl + "Необходимо получить матрицу В.");
            int[,] matrix2 = GetMatrix(rows, columns);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица В получена успешно!");

            Console.WriteLine(nl + "Разность матриц А и В - матрица С:");
            Console.ResetColor();
            // Вывод сумм соответствующих элементов массивов.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                    Console.Write(matrix1[row, col] - matrix2[row, col] + "\t");
                Console.Write(nl);
            }

            CommandLine();
        }

        /// <summary>
        /// Нахождение произведения двух матриц.
        /// </summary>
        static void ProductCommand()
        {
            Console.WriteLine(nl + "Найдём произведение двух матриц.");
            int rows1 = 0, columns1 = 0, rows2 = 0, columns2 = 0;

            // Получение размерности первой матрицы.
            GetCommonDensity(ref rows1, ref columns1);

            // Получение матрицы А.
            Console.WriteLine(nl + "Необходимо получить матрицу А.");
            int[,] matrix1 = GetMatrix(rows1, columns1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица А получена успешно!");
            Console.ResetColor();

            Console.WriteLine(nl + "Число строк матрицы В должно равняться числу столбцов А");
            // Получение размерности второй матрицы.
            GetCommonDensity(ref rows2, ref columns2);
            // Обработка некорректного ввода.
            while (rows2 != columns1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нельзя будет умножить такие матрицы!" + nl +
                    "Введите корректную размерность для матрицы В.");
                Console.ResetColor();

                GetCommonDensity(ref rows2, ref columns2);
            }

            // Получение матрицы В.
            Console.WriteLine(nl + "Необходимо получить матрицу В.");
            int[,] matrix2 = GetMatrix(rows2, columns2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица В получена успешно!");

            Console.WriteLine(nl + "Произведение матриц А и В - матрица С:");
            Console.ResetColor();
            // Вывод сумм произведений соответствующих строки А и столбца В.
            int sum;
            for (int row = 0; row < rows1; ++row)
            {
                for (int col = 0; col < columns2; ++col)
                {
                    sum = 0;
                    for (int add = 0; add < columns1; ++add)
                        sum += matrix1[row, add] * matrix2[add, col];
                    Console.Write(sum + "\t");
                }
                Console.Write(nl);
            }

            CommandLine();
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        static void MultiCommand()
        {
            Console.WriteLine(nl + "Умножим необходимую матрицу на число.");

            int rows = 0, columns = 0;
            // Получение размерности матрицы и самой матрицы.
            GetCommonDensity(ref rows, ref columns);
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введите целое число, на которое нужно умножить матрицу: ");
            Console.ResetColor();

            // Получение числа.
            string input = Console.ReadLine();
            int number;
            // Обработка некорректного ввода.
            while (!int.TryParse(input, out number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Введите целое число! ");
                Console.ResetColor();

                input = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Результат умножения:");
            Console.ResetColor();

            // Вывод умножения массива на указанное число.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                    Console.Write((matrix[row, col] * number) + "\t");
                Console.Write(nl);
            }

            CommandLine();
        }

        /// <summary>
        /// Нахождение определителя матрицы.
        /// </summary>
        static void DetCommand()
        {
            Console.WriteLine(nl + "Найдём определитель матрицы.");

            int rows = 0, columns = 0;
            // Получение размерности матрицы и самой матрицы.
            GetSquareDensity(ref rows, ref columns);
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");

            bool sign = true;
            // Приведение матрицы к ступенчатому виду методом Гаусса.
            decimal[,] matrixStep = GetStepMatrix(matrix, rows, columns, ref sign);

            // Получение произведения диагональных элементов.
            decimal determinant = 1;
            for (int row = 0; row < rows; ++row)
                for (int col = 0; col < columns; ++col)
                    if (row == col)
                        determinant *= matrixStep[row, col];
            // Перестановка строк - смена знака определителя на противоположный.
            if (!sign)
                determinant = -determinant;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nl + "Определитель матрицы: " + Math.Round(determinant));
            Console.ResetColor();

            CommandLine();
        }

        /// <summary>
        /// Нахождение ранга матрицы.
        /// </summary>
        static void RankCommand()
        {
            Console.WriteLine(nl + "Найдём ранг матрицы.");

            int rows = 0, columns = 0;
            // Получение размерности матрицы и самой матрицы.
            GetCommonDensity(ref rows, ref columns);
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");

            bool sign = true;
            // Приведение матрицы к ступенчатому виду методом Гаусса.
            decimal[,] matrixStep = GetStepMatrix(matrix, rows, columns, ref sign);

            int rank = 0;
            // Подсчёт кол-ва ненулевых строк - ранг.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                {
                    if (Math.Abs(matrixStep[row, col]) > 0.000000001m)
                    { ++rank; break; }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nl + "Ранг матрицы: " + rank);
            Console.ResetColor();

            CommandLine();
        }

        /// <summary>
        /// Нахождение ранга матрицы (для себя).
        /// </summary>
        static int RankMatrix(decimal[,] matrixStep, int rows, int columns)
        {
            int rank = 0;
            // Подсчёт кол-ва ненулевых строк - ранг.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                {
                    if (Math.Abs(matrixStep[row, col]) > 0.000000001m)
                    { ++rank; break; }
                }
            }

            return rank;
        }

        /// <summary>
        /// Решение СЛАУ методом Гаусса.
        /// </summary>
        static void SLAECommand()
        {
            Console.WriteLine(nl + "Решим СЛАУ.");
            int rows = 0, columns = 0;
            // Получение размерности матрицы.
            GetCommonDensity(ref rows, ref columns);

            // Обработка некорректного ввода.
            while (columns == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Кол-во столбцов должно быть больше одного.");
                GetCommonDensity(ref rows, ref columns);
            }

            Console.WriteLine(nl + "Необходимо ввести систему в матричном виде, " + nl +
                "где столбцы - соответствующие коэффициенты перед неизвестными," + nl +
                "а последний столбец - вектор свободных членов.");
            // Получение матрицы.
            int[,] matrix = GetMatrix(rows, columns);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Матрица получена успешно!");
            Console.ResetColor();

            bool sign = true;
            // Приведение матрицы к ступенчатому виду методом Гаусса.
            decimal[,] matrixStep = GetStepMatrix(matrix, rows, columns, ref sign);

            // Выявление единственности решения СЛАУ и нахождение или выражение решения/й.
            if (OneSolution(matrixStep, rows, columns))
                SLAEAnswerFix(matrixStep, rows, columns);
            else
                SLAEAnswerNotFix(matrixStep, rows, columns);

            CommandLine();
        }

        /// <summary>
        /// Нахождение единственного решения СЛАУ
        /// </summary>
        /// <param name="matrix">матрица, приведённая к ступенчатому виду</param>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        static void SLAEAnswerFix(decimal[,] matrix, int rows, int columns)
        {
            // Строковый массив для записи ответов.
            string[] answers = new string[columns - 1];
            for (int count = 0; count < answers.Length; ++count)
                answers[count] = null;

            decimal equat; string term; decimal coeff;
            // Прохождение по матрице с конца, слева направо.
            for (int row = rows - 1; row >= 0; --row)
            {
                for (int col = 0; col < columns - 1; ++col)
                {
                    // Вычисление переменной ведущего элемента строки.
                    if (Math.Abs(matrix[row, col]) > 0.000000001m)
                    {
                        equat = 0;
                        answers[col] += "x" + (row + 1) + " = ";

                        for (int elem = col + 1; elem < columns - 1; ++elem)
                        {
                            term = null;
                            // Нахождение уже вычисленного значения нужной переменной.
                            for (int symb = 5; symb < answers[elem].Length; ++symb)
                                term += answers[elem][symb];
                            decimal.TryParse(term, out coeff);

                            // Подстановка уже вычисленных переменных в выражение.
                            equat += -matrix[row, elem] * coeff / matrix[row, col];
                        }

                        // Прибавление свободного члена с соответсвтующим коэффициентом.
                        equat += matrix[row, columns - 1] / matrix[row, col];
                        answers[col] += Math.Round(equat, 5);

                        break;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ответ:");
            Console.ResetColor();

            // Вывод массива ответов.
            for (int ans = 0; ans < answers.Length; ++ans)
                Console.WriteLine(answers[ans]);
        }

        /// <summary>
        /// Нахождение и вывод множества решений (если они есть),
        /// когда СЛАУ не имеет единственное решение.
        /// </summary>
        /// <param name="matrix">матрица, приведенная к ступенчатому виду</param>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        static void SLAEAnswerNotFix(decimal[,] matrix, int rows, int columns)
        {
            // Строковый массив для записи выражений переменных.
            string[] output = new string[columns - 1];
            for (int count = 0; count < output.Length; ++count)
                output[count] = null;

            decimal division; bool error = false; bool noSolution;
            // Прохождение по матрице с конца, слева направо.
            for (int row = rows - 1; row >= 0; --row)
            {
                noSolution = true;
                for (int col = 0; col < columns - 1; ++col)
                {
                    // Выражение первого ненулевого элемента - главного - через свободные.
                    if (Math.Abs(matrix[row, col]) > 0.000000001m)
                    {
                        noSolution = false;
                        output[col] = "x" + (col + 1) + " = ";
                        for (int elem = col + 1; elem < columns - 1; ++elem)
                        {
                            if (Math.Abs(matrix[row, elem]) > 0.000000001m)
                            {
                                // Вычисление коэффициента для выражения главного члена.
                                division = -matrix[row, elem] / matrix[row, col];
                                if (division >= 0 && elem != col + 1) output[col] += "+";
                                output[col] += Math.Round(division, 3) + "(x" + (elem + 1) + ")";
                            }
                        }

                        // Приписывание к выражению вычисленного свободного числа.
                        division = matrix[row, columns - 1] / matrix[row, col];
                        if (division >= 0) output[col] += "+";
                        output[col] += Math.Round(division, 3);
                        break;
                    }
                }
                // Если строка имеет вид (0 ... 0 | !0), то решений нет
                if (noSolution && Math.Abs(matrix[row, columns - 1]) > 0.000000001m)
                    error = true;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nl + "Ответ:"); Console.ResetColor();

            if (error) Console.WriteLine("нет решений.");
            else
            {
                bool infinity = true;
                for (int count = 0; count < output.Length; ++count)
                {
                    if (output[count] != null)
                    { Console.WriteLine(output[count]); infinity = false; }
                }

                // Если в матрице одни нули, то переменные могут быть любыми.
                if (infinity)
                    Console.WriteLine("Все переменные могут быть любыми вещественными числами.");
                else
                    Console.WriteLine("Остальные переменные могут быть любыми вещественными числами.");
            }
        }

        /// <summary>
        /// Проверка на единственность решения СЛАУ.
        /// </summary>
        /// <param name="matrix">матрица, приведённая к ступенчатому виду</param>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        /// <returns></returns>
        static bool OneSolution(decimal[,] matrix, int rows, int columns)
        {
            // Создание матрицы системы из исходной.
            decimal[,] matrixSyst = new decimal[rows, columns - 1];
            for (int row = 0; row < rows; ++row)
                for (int col = 0; col < columns - 1; ++col)
                    matrixSyst[row, col] = matrix[row, col];

            // Нахождение ранга матрицы системы и ранга расширенной матрицы.
            int rank1 = RankMatrix(matrixSyst, rows, columns - 1);
            int rank2 = RankMatrix(matrix, rows, columns);

            // Проверка на равенство рангов кол-ву переменных в СЛАУ.
            if (rank1 == rank2 && rank2 == columns - 1)
                return true;
            return false;
        }

        /// <summary>
        /// Вывод сообщения об ошибке.
        /// </summary>
        static void Error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(nl + "Произошла ошибка!");
            Console.ResetColor();

            Console.WriteLine("Данные были введены некорректно" + nl +
                "или" + nl + "Размерность матрицы не соответствовала указанной заранее" + nl +
                "или" + nl + "Какие-то элементы матрицы могли не быть целыми числами" + nl +
                "или" + nl + "Какие-то элементы матрицы не вошли в диапазон [-1000; 1000]."
                + nl);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Введите '0', " + "если хотите вернуться к командной строке;"
                + nl + "введите другую клавишу, " + "если хотите продолжить начатую операцию.");
            Console.ResetColor();

            // Возврат к командной строке.
            if (Console.ReadLine() == "0")
                CommandLine();
        }

        /// <summary>
        /// Получение матрицы.
        /// </summary>
        /// <returns></returns>
        static int[,] GetMatrix(int rows, int columns)
        {
            Console.WriteLine(nl + "Для осуществления операции нужно указать матрицу.");
            // Получение способа задания матрицы.
            string inputChoice = GetInputChoice();

            int[,] matrix = new int[rows, columns];
            // Переход к выбранному способу.
            switch (inputChoice)
            {
                case "1": matrix = GetConsoleMatrix(rows, columns); break;
                case "2": matrix = GetFileMatrix(rows, columns); break;
                case "3": matrix = GetRandMatrix(rows, columns); break;
            }

            return matrix;
        }

        /// <summary>
        /// Получение способа задания матрицы.
        /// </summary>
        /// <returns></returns>
        static string GetInputChoice()
        {
            Console.WriteLine("Вы можете задать матрицу трёмя способами: " + nl +
                "1. Ввести в консольное окно." + nl +
                "2. Прочесть из файла формата .txt." + nl +
                "3. Сгенерировать рандомно.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Выберите, каким способом вы хотите задать матрицу: ");
            Console.ResetColor();
            // Хранение выбранного способа задания.
            string inputChoice = Console.ReadLine();

            // Обработка некорректного ввода.
            while (inputChoice != "1" && inputChoice != "2" && inputChoice != "3")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Введите цифру 1-3! ");
                Console.ResetColor();

                inputChoice = Console.ReadLine();
            }

            return inputChoice;
        }

        /// <summary>
        /// Получение матрицы с консоли.
        /// </summary>
        /// <param name="rows">кол-во строк матрицы</param>
        /// <param name="columns">кол-во столбцов матрицы</param>
        /// <returns></returns>
        static int[,] GetConsoleMatrix(int rows, int columns)
        {
            // Массив для хранения матрицы.
            int[,] matrix = new int[rows, columns];

            // Хранение введённой строки матрицы.
            string[] matrixString = new string[columns];
            // Обработка некорректных введённых строк.
            bool rightMatrix;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(nl + "Введите матрицу размера {0}x{1}, " +
                "разделяя элементы пробелами, а строки - переходами на новые:",
                rows, columns);
                Console.ResetColor();

                rightMatrix = true;
                // Получение матрицы построчно.
                for (int str = 0; str < rows; ++str)
                {
                    // Выделение элементов из строки.
                    matrixString = Console.ReadLine().Split();

                    for (int elem = 0; elem < columns; ++elem)
                    {
                        try
                        {
                            // Заполнение целочисленного массива с проверкой.
                            if (!int.TryParse(matrixString[elem], out matrix[str, elem])
                                || Math.Abs(matrix[str, elem]) > 1000)
                            { Error(); rightMatrix = false; break; }
                        }
                        // Обработка исключения: вводится меньше элементов, чем нужно.
                        catch (Exception) { Error(); rightMatrix = false; break; }
                    }

                    if (!rightMatrix) break;
                }
            } while (!rightMatrix);

            return matrix;
        }

        /// <summary>
        /// Получение матрицы с файла.
        /// </summary>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        /// <returns></returns>
        static int[,] GetFileMatrix(int rows, int columns)
        {
            // Массив для хранения матрицы.
            int[,] matrix = new int[rows, columns];
            Console.WriteLine(nl + "В файле должна лежать только матрица." + nl +
                "Её элементы должны разделяться пробелами.");

            // Обработка некорректных элементов, указанных в файле.
            bool everythingOK;
            do
            {
                everythingOK = true;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(nl + "Введите путь к файлу (.txt) с матрицей: ");
                Console.ResetColor();
                string path = Console.ReadLine();

                string rightPath = null;
                // Замена указанных разделителей системными.
                for (int count = 0; count < path.Length; ++count)
                {
                    if (path[count] == '/' || path[count] == '\\')
                        rightPath += separator;
                    else rightPath += path[count];
                }

                try
                {
                    // Чтение файла в строковый массив.
                    string[] matrixFile = File.ReadAllLines(rightPath);

                    string[] matrixString;
                    // Обработка матрицы построчно.
                    for (int str = 0; str < rows; ++str)
                    {
                        // Выделение элементов из строки.
                        matrixString = matrixFile[str].Split();
                        for (int elem = 0; elem < columns; ++elem)
                        {
                            // Заполнение целочисленного массива с проверкой.
                            if (!int.TryParse(matrixString[elem], out matrix[str, elem])
                                || Math.Abs(matrix[str, elem]) > 1000)
                            { Error(); everythingOK = false; break; }
                        }
                        if (!everythingOK) break;
                    }
                }
                // Обработка исключения при работе с файлом.
                catch (Exception) { Error(); everythingOK = false; }
            } while (!everythingOK);

            return matrix;
        }

        /// <summary>
        /// Генерация рандомной матрицы.
        /// </summary>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        /// <returns></returns>
        static int[,] GetRandMatrix(int rows, int columns)
        {
            Random rand = new Random();
            // Массив для хранения матрицы.
            int[,] matrix = new int[rows, columns];

            string[] input; bool everythingOK; int from = 0, to = 0;
            // Обработка некорректного ввода.
            do
            {
                everythingOK = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(nl + "Введите границы элементов в генерируемой матрице " + nl
                    + "(a b, где диапазон значений элементов - [a, b], |a|<=1000, |b|<=1000):");
                Console.ResetColor();

                try
                {
                    // Разделение входной строки на пограничные значения диапазона.
                    input = Console.ReadLine().Split();
                    // Обработка некорректного ввода.
                    while (!int.TryParse(input[0], out from) || !int.TryParse(input[1], out to)
                        || to < from || Math.Abs(from) > 1000 || Math.Abs(to) > 1000)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введите два целых числа [-1000; 1000]; " +
                            "второе число должно быть больше первого!");
                        Console.ResetColor();
                        input = Console.ReadLine().Split();
                    }
                }
                // Обработка исключения при некорректном вводе.
                catch (Exception) { Error(); everythingOK = false; }
            } while (!everythingOK);

            // Генерация.
            for (int row = 0; row < rows; ++row)
                for (int col = 0; col < columns; ++col)
                    matrix[row, col] = rand.Next(from, to + 1);

            Console.WriteLine(nl + "Сгенерированная матрица:");
            // Вывод массива-матрицы на экран.
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                    Console.Write(matrix[row, col] + "\t");
                Console.Write(nl);
            }
            return matrix;
        }

        /// <summary>
        /// Получение размерности произвольной матрицы.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        static void GetCommonDensity(ref int rows, ref int columns)
        {
            string[] input; bool everythingOK;
            // Обработка некорректного ввода.
            do
            {
                everythingOK = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(nl + "Введите размерность " +
                "(кол-во строк и кол-во столбцов через пробел): ");
                Console.ResetColor();

                try
                {
                    // Разделение на кол-во строк и кол-во столбцов в матрице.
                    input = Console.ReadLine().Split();
                    // Обработка некорректного ввода.
                    while (!int.TryParse(input[0], out rows)
                        || !int.TryParse(input[1], out columns)
                        || rows <= 0 || rows > 10 || columns <= 0 || columns > 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Введите натуральные числа меньше 10! ");
                        Console.ResetColor();
                        input = Console.ReadLine().Split();
                    }
                }
                // Обработка исключения при некорректном вводе.
                catch (Exception) { Error(); everythingOK = false; }
            } while (!everythingOK);
        }

        /// <summary>
        /// Получение порядка квадратной матрицы.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        static void GetSquareDensity(ref int rows, ref int columns)
        {
            Console.WriteLine("Операция возможна только с квадратной матрицей.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(nl + "Введите порядок матрицы " +
                "(число n, где nxn - размер матрицы): ");
            Console.ResetColor();

            string input = Console.ReadLine();
            // Обработка некорректного ввода.
            while (!int.TryParse(input, out rows) || rows <= 0 || rows > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Введите натуральное число меньше 10! ");
                Console.ResetColor();

                input = Console.ReadLine();
            }

            // Кол-во строк и кол-во столбцов равны.
            columns = rows;
        }

        /// <summary>
        /// Приведение матрицы к ступенчатому виду.
        /// </summary>
        /// <param name="matrix">исходная матрица</param>
        /// <param name="rows">кол-во строк в матрице</param>
        /// <param name="columns">кол-во столбцов в матрице</param>
        /// <param name="sign">знак определителя</param>
        /// <returns></returns>
        static decimal[,] GetStepMatrix(int[,] matrix, int rows, int columns, ref bool sign)
        {
            // Запись исходного массива-матрицы в массив типа decimal.
            decimal[,] matrixStep = new decimal[rows, columns];
            for (int row = 0; row < rows; ++row)
                for (int col = 0; col < columns; ++col)
                    matrixStep[row, col] = matrix[row, col];

            decimal prod;
            // Реализация метода Гаусса прохождением поэлементно по строкам.
            for (int row = 0; row < rows - 1; ++row)
            {
                for (int col = 0; col < columns; ++col)
                {
                    // Строка, начинающаяся с нуля, при наличии под нулём ненулевых элементов,
                    // меняется со строкой с конца, в которой обнаруживается ненулевой элемент.
                    // (Во избежание деления на 0.)
                    if (Math.Abs(matrixStep[row, col]) < 0.000000001m)
                    {
                        for (int under = rows - 1; under > row; --under)
                        {
                            if (Math.Abs(matrixStep[under, col]) > 0.000000001m)
                            {
                                // Перестановка строк.
                                for (int elem = 0; elem < columns; ++elem)
                                {
                                    decimal t = matrixStep[row, elem];
                                    matrixStep[row, elem] = matrixStep[under, elem];
                                    matrixStep[under, elem] = t;
                                }

                                // Меняется знак определителя.
                                sign = !sign; --col; break;
                            }
                        }
                    }
                    else
                    {
                        // Получение нулей под ненулевым элементом
                        // с помощью элементарных преобразований:
                        // прибавления к строке другой, умноженной на число (prod).
                        for (int under = row + 1; under < rows; ++under)
                        {
                            prod = -(matrixStep[under, col] / matrixStep[row, col]);
                            for (int elem = 0; elem < columns; ++elem)
                                matrixStep[under, elem] += matrixStep[row, elem] * prod;
                        }
                        break;
                    }
                }
            }
            return matrixStep;
        }

        /// <summary>
        /// Вывод перечня операций.
        /// </summary>
        static void HelpCommand()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(nl + "Перечень доступных операций:");
            Console.ResetColor();

            Console.WriteLine("1. trace\tслед матрицы");
            Console.WriteLine("2. transpose\tтранспонирование матрицы");
            Console.WriteLine("3. sum\t\tсумма двух матриц");
            Console.WriteLine("4. dif\t\tразность двух матриц");
            Console.WriteLine("5. product\tпроизведение двух матриц");
            Console.WriteLine("6. multi\tумножение матрицы на число");
            Console.WriteLine("7. det\t\tопределитель матрицы");
            Console.WriteLine("8. rank\t\tранг матрицы");
            Console.WriteLine("9. SLAE\t\tрешение СЛАУ (метод Гаусса)");

            Console.WriteLine("help\t\tвывод перечня операций");
            Console.WriteLine("quit\t\tвыход из программы");
        }

        /// <summary>
        /// Приветствие и пояснительная бригада.
        /// </summary>
        static void Introduction()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Вы запустили матричный калькулятор!" + nl +
                "Список операций доступен при вводе команды \"help\".");
            Console.ResetColor();

            Console.WriteLine("Элементами матрицы могут быть только целые числа!" + nl +
                "Максимальный порядок матрицы - 10х10." + nl +
                "При чтении матрицы из файла в файле должна лежать только сама матрица!"
                + nl + "Указывать её размерность внутри файла не нужно." + nl +
                "Программа работает с чилами в диапазоне [-1000; 1000].");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Приятного использования!");
            Console.ResetColor();

            Console.WriteLine(nl + "Введите команду для выполнения необходимой операции.");
        }

        /// <summary>
        /// Выход из программы.
        /// </summary>
        static void QuitCommand()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(nl + "Вы уверены, что хотите выйти из программы?");
            Console.ResetColor();

            Console.Write("Для выхода введите '0', иначе - любую клавишу: ");
            if (Console.ReadLine() == "0")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(nl + "Программа завершает работу!");
                Console.ResetColor();

                // Завершение программы.
                Environment.Exit(0);
            }
        }
    }
}
