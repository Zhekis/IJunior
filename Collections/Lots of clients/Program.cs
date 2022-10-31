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
            int billClient = 0;

            Random random = new Random();

            Queue<int> Bills = new Queue<int>();

            for (int i = 0; i < random.Next(minClients, maxClients); i++)
            {
                Bills.Enqueue(random.Next(minSum, maxSum));
            }

            while (Bills.Count > 0)
            {
                ServiceClient(Bills, ref billClient, ref sumMoneyShop);
            }

            Console.WriteLine("Все клиенты обслужены, на счету магазина " + sumMoneyShop);
        }

        static void ServiceClient (Queue<int> Bills, ref int billClient, ref int sumMoneyShop)
        {
            billClient = Bills.Dequeue();
            sumMoneyShop += billClient;
            Console.WriteLine("Клиент с суммой покупки " + billClient + " обслужен, наш счет составляет " + sumMoneyShop);
            Console.ReadKey();
            Console.Clear();
        }
    }
}