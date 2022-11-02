namespace Lots_of_clients
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int minSum = 100;
            int maxSum = 1000;
            int minClients = 5;
            int maxClients = 10;
            int sumMoneyShop = 0;

            Random random = new Random();

            Queue<int> bills = new Queue<int>();

            for (int i = 0; i < random.Next(minClients, maxClients); i++)
            {
                bills.Enqueue(random.Next(minSum, maxSum));
            }

            while (bills.Count > 0)
            {
                sumMoneyShop += ServiceClient(bills);
                Console.WriteLine("На счету магазина " + sumMoneyShop);
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Все клиенты обслужены, на счету магазина " + sumMoneyShop);
        }

        static int ServiceClient (Queue<int> bills)
        {
            int billClient = bills.Dequeue();
            Console.WriteLine("Клиент с суммой покупки " + billClient + " обслужен");
            return billClient;
        }
    }
}
