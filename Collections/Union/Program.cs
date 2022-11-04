using System;

namespace Union
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] array1 = { "1", "2", "1" };
            string[] array2 = { "4", "2", "3" };
            List<string> numbers = new List<string>();

            TransferNumbers(numbers, array1);
            TransferNumbers(numbers, array2);

            foreach (string number in numbers)
            {
                Console.Write(number + " ");
            }
        }

        static void TransferNumbers (List<string> numbers, string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (numbers.Contains(array[i]) == false)
                {
                    numbers.Add(array[i]);
                }
            }
        }
    }
}