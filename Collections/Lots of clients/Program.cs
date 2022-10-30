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

            Queue<int> setBills = new Queue<int>();

            for (int i = 0; i < random.Next(minClients, maxClients); i++)
            {
                setBills.Enqueue(random.Next(minSum, maxSum));
            }

            while (setBills.Count > 0)
            {
                sumMoneyShop += setBills.Dequeue();
                Console.WriteLine("Клиент обслужен, наш счет составляет " + sumMoneyShop);
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Все клиенты обслужены, на счету магазина " + sumMoneyShop);
        }
    }
}