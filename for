using System;

class Program
{

    static void Main(string[] args)
    {    
        int startNumber = 5;
        int endNumber = 96;

        for (int i = startNumber; i <= endNumber; i += 7)
        {
            Console.Write(" " + i);
        }
    }
}
