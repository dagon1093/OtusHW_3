using System.Collections;
using System.Linq;

namespace OtusHW_3
{
    internal static class Utils
    {

        public static SortedDictionary<string, string> GetUserNumbers()
        {
            
            string userInputA = "";
            string userInputB = "";
            string userInputC = "";

            SortedDictionary<string, string> userInputs = new SortedDictionary<string, string>
            {
                { "a", userInputA },
                { "b", userInputB },
                { "c", userInputC }
            };

            Cursor cursor = new();
            

            while (true)
            {
                
                PrintEquationAndVariables(userInputs, cursor);
                cursor.SetPositionCursor(userInputs.ElementAt(cursor.PositionY - 1).Value.Length + 5, cursor.PositionY);
                

                ConsoleKeyInfo key = Console.ReadKey();
                    
   
                if (key.Key == ConsoleKey.UpArrow)
                {
                    cursor.PositionY -= 1;
                    cursor.SetPositionCursor(userInputs.ElementAt(cursor.PositionY - 1).Value.Length + 5, cursor.PositionY);
                    Console.Clear();
                    continue;
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    cursor.PositionY += 1;
                    cursor.SetPositionCursor(userInputs.ElementAt(cursor.PositionY - 1).Value.Length + 5, cursor.PositionY);
                    Console.Clear();
                    continue;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (userInputs.ElementAt(cursor.PositionY - 1).Value.Length > 0)
                    {
                        switch (cursor.PositionY - 1)
                        {
                            case 0:
                                userInputs["a"] = userInputs["a"].Substring(0, userInputs["a"].Length - 1); break;
                            case 1:
                                userInputs["b"] = userInputs["b"].Substring(0, userInputs["b"].Length - 1); break;
                            case 2:
                                userInputs["c"] = userInputs["c"].Substring(0, userInputs["c"].Length - 1); break;
                        }

                    }
                    Console.Clear();
                    continue;
                }

                if (key.Key == ConsoleKey.Enter) 
                {
                    break;
                }

                UpdateParameters(userInputs, cursor, key);
                Console.Clear();

            }
            return userInputs;
            
        }

        public static void ParseUserInput(IDictionary<string, string> userInputs, out int a, out int b, out int c)
        {

            try
            {

                if (!int.TryParse(userInputs["a"], out a))
                {
                    throw new FormatException("a");
                }
                else if (!CanBeInt(userInputs["a"]))
                {
                    throw new OverflowException("a");
                }
                else
                {
                    a = int.Parse(userInputs["a"]);
                }

                if (!int.TryParse(userInputs["b"], out b))
                {
                    throw new FormatException("b");
                }
                else if (!CanBeInt(userInputs["b"]))
                {
                    throw new OverflowException("b");
                }
                else
                {
                    b = int.Parse(userInputs["b"]);
                }

                if (!int.TryParse(userInputs["c"], out c))
                {
                    throw new FormatException("c");
                }
                else if (!CanBeInt(userInputs["c"]))
                {
                    throw new OverflowException("c");
                }
                else
                {
                    c = int.Parse(userInputs["c"]);
                }

            }
            catch (OverflowException overflow)
            {
                throw new OverflowException(overflow.Message);
            }
            catch (FormatException e) 
            {
                throw new FormatException(e.Message);
                
            }
            
        }

        public static void FormatData(string message, Severity severity, IDictionary<string, string> data)
        {
            if (severity == Severity.Error)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
                Console.WriteLine($"Неверный формат параметра {message}");
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
                Console.WriteLine();
                Console.WriteLine($"a = {data["a"]}");
                Console.WriteLine($"b = {data["b"]}");
                Console.WriteLine($"c = {data["c"]}");
                Console.WriteLine();
            }

            if (severity == Severity.Warning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Вещественных значений не найдено");
            }
            Console.ResetColor();
        }

        public static void SolveExercise(int a, int b, int c)
        {
            try
            {
                double eps = 0.00001;
                Console.WriteLine(FindDiscriminant(a, b, c));
                double discriminant = FindDiscriminant(a, b, c);
                if (discriminant > eps)
                {
                    double root1 = (-(b) + Math.Sqrt(discriminant)) / (2 * a);
                    double root2 = (-(b) - Math.Sqrt(discriminant)) / (2 * a);

                    Console.Clear();   
                    Console.WriteLine($"x1 = {root1}, x2 = {root2}");
                }
                if (Math.Abs(discriminant) < eps)
                {
                    Console.Clear();
                    double root = (-(b) + Math.Sqrt(discriminant)) / 2 * a;
                    Console.WriteLine($"x = {root}");
                }
                if (discriminant < eps)
                {
                    throw new NoRealRootsException();
                }
            }
            catch(NoRealRootsException e)
            {
                throw new NoRealRootsException();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static double FindDiscriminant(int a, int b, int c)
        {
            double discriminant = (b * b) - 4 * a * c;
            return discriminant;
        }

        public static void PrintEquationAndVariables(SortedDictionary<string,string> userInputs, Cursor cursor)
        {
            if (cursor.PositionY == 1) 
            {
                Console.WriteLine("Уравнение: a * x^2 + b * x + c = 0");
                Console.WriteLine("> a: " + userInputs.ElementAt(0).Value);
                Console.WriteLine("  b: " + userInputs.ElementAt(1).Value);
                Console.WriteLine("  c: " + userInputs.ElementAt(2).Value);
            }
            else if (cursor.PositionY == 2)
            {
                
                Console.WriteLine("Уравнение: a * x^2 + b * x + c = 0");
                Console.WriteLine("  a: " + userInputs.ElementAt(0).Value);
                Console.WriteLine("> b: " + userInputs.ElementAt(1).Value);
                Console.WriteLine("  c: " + userInputs.ElementAt(2).Value);
                
            }
            else if (cursor.PositionY == 3)
            {
                Console.WriteLine("Уравнение: a * x^2 + b * x + c = 0");
                Console.WriteLine("  a: " + userInputs.ElementAt(0).Value);
                Console.WriteLine("  b: " + userInputs.ElementAt(1).Value);
                Console.WriteLine("> c: " + userInputs.ElementAt(2).Value);
            }


        }

        public static void UpdateParameters(SortedDictionary<string, string> userInputs ,Cursor cursor, ConsoleKeyInfo key)
        {
            if (!char.IsControl(key.KeyChar)) { 
                switch (cursor.PositionY)
                {
                    case 1: userInputs["a"] = userInputs["a"] + key.KeyChar; break;
                    case 2: userInputs["b"] = userInputs["b"] + key.KeyChar; break;
                    case 3: userInputs["c"] = userInputs["c"] + key.KeyChar; break;
                }
            }
        }

        public static bool ContainNonDigit(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c)) return true;
            }
            return false;
        }

        public static bool CanBeInt(string input)
        {
            long result;
            if(!long.TryParse(input, out result)){
                return false;
            }
            if (result < int.MinValue || result > int.MaxValue)
            {
                return false;
            }
            return true;

        }


        }



    
}
