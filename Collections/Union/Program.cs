using System;

namespace Union
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] array1 = { "1", "2", "1" };
            string[] array2 = { "4", "2", "3" };
            string tempNumber;
            List<string> numbers = new List<string>();

            numbers.AddRange(array1);
            numbers.AddRange(array2);
            numbers.Sort();

            for (int i = 0; i < numbers.Count; i++)
            {
                tempNumber = numbers[i];
                if (tempNumber == )
            }

            foreach (string number in numbers)
            {
                tempNumber = number;

                if (tempNumber == number)
                {
                    numbers.Remove(number);
                }

            }


            foreach (string number in numbers)
            {
                Console.Write(number + " ");
            }

        }
    }
}