using static System.Net.Mime.MediaTypeNames;

namespace DinamicArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string UserChoiceExit = "exit";
            const string UserChoiceSum = "sum";
            List<int> array = new List<int>();
            string userChoice = "";
            int sumArray = 0;

            while (userChoice != UserChoiceExit)
            {
                Console.WriteLine("Введите число (для вывода суммы чисел - введите " + UserChoiceSum + ", для выхода - введите " + UserChoiceExit);
                userChoice = Console.ReadLine();

                if (userChoice != UserChoiceSum && userChoice != UserChoiceExit)
                {
                    if (int.TryParse(userChoice, out int number))
                    {
                        Console.WriteLine("Вы ввели число {0}", number);
                        array.Add(number);
                        sumArray += number;
                    }
                    else
                    {
                        Console.WriteLine("Не удалось распознать число, попробуйте еще раз.");
                    }
                }

                if (userChoice == UserChoiceSum)
                {
                    Console.WriteLine(sumArray);
                }
            }
        }
    }
}