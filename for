using System;

class Program
{

    static void Main(string[] args)
    {    
        int startNumber = 5;
        int endNumber = 96;
        int difference = 7;

        for (int i = startNumber; i <= endNumber; i += difference)
        {
            Console.Write(" " + i);
        }
    }
}
