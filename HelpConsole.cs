namespace Checkup;

internal class HelpConsole
{
    public static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            Console.WriteLine("Неккоректное значение. Повторите ввод");
        }
    }

    public static byte ReadByte(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (byte.TryParse(Console.ReadLine(), out byte value))
                return value;
            Console.WriteLine("Ошибка: введите целое число от 0 до 255.");
        }
    }

    public static int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }

    public static double ReadPositiveDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out double value) && value > 0)
                return value;
            Console.WriteLine("Ошибка: введите число больше 0.");
        }
    }

    public static bool ReadBool(string message)
    {
        while (true)
        {
            Console.Write(message + " (1 - Да, 0 - Нет): ");
            string input = Console.ReadLine();
            if (input == "1") return true;
            if (input == "0") return false;
            Console.WriteLine("Ошибка: введите 1 или 0.");
        }
    }
}