// See https://aka.ms/new-console-template for more information
using OtusHW_3;


while (true) {

    int a, b, c;
    IDictionary<string, string> userInputs = Utils.GetUserNumbers();

    try
    {
        Utils.ParseUserInput(userInputs, out a, out b, out c);
        Utils.SolveExercise(a, b, c);
        break;
    }
    catch (OverflowException overflow)
    {
        Console.Clear();
        Console.WriteLine(overflow.Message);
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine($"Переполнение {overflow.Message}, нужно вводить значения в диапазоне от 2 147 483 648 до 2 147 483 647");
        Console.ResetColor();
        Console.ReadKey();
        continue;

    }
    catch (NoRealRootsException e)
    {
        Utils.FormatData(e.Message, Severity.Warning, userInputs);
        continue;
    }
    catch (FormatException e)
    {
        Utils.FormatData(e.Message, Severity.Error, userInputs);
        continue;
    }
}





