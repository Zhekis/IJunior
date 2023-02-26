namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket(10);
            supermarket.Work();
        }
    }

    class Supermarket
    {
        private int _money = 0;
        private bool _purchaseCompleted = false;
        private List<Good> _goods = new List<Good>();
        private Queue<Client> _clients = new Queue<Client>();

        public Supermarket(int clientCount)
        {
            _goods.Add(new Good("Milk", 50));
            _goods.Add(new Good("Coffee", 100));
            _goods.Add(new Good("Beer", 80));
            _goods.Add(new Good("Sugar", 50));
            _goods.Add(new Good("Cigarette", 150));
            _goods.Add(new Good("Oil", 140));
            _goods.Add(new Good("Water", 30));
            _goods.Add(new Good("Cake", 250));
            _goods.Add(new Good("Alcohol", 500));
            Random random = new Random(245);
            CreateNewClient(clientCount,random);
        }

        public void Work() 
        {
            while (_clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"В очереди {_clients.Count} человек");
                Client client = _clients.Dequeue();
                Console.WriteLine($"Клиент у кассы. Сумма к оплате {client.GetAmountPurchase()}.");

                while (_purchaseCompleted == false)
                {
                    if (client.CheckSolvency())
                    {
                        _money += client.ToPay();
                        Console.WriteLine("Покупка оплачена.");
                        _purchaseCompleted = true;
                    }
                    else
                    {
                        Console.Write("У клиента не хватает денег.");
                        client.RemoveGood();
                        Console.WriteLine("Нажмите для продолжения.");
                        Console.ReadKey();
                    }
                }

                Console.WriteLine("Нажмите для обслуживания следующего клиента.");
                _purchaseCompleted = false;
                Console.ReadKey();
            }
        }

        private void CreateNewClient(int count, Random random)
        {
            int minMoney = 2000;
            int maxMoney = 2600;
            int minCountGoods = 10;
            int maxCountGoods = 20;
            List<Good> goods;

            for (int i = 0; i < count; i++)
            {
                goods = new List<Good>();
                for (int j = 0;j < random.Next(minCountGoods, maxCountGoods); j++)
                {
                    goods.Add(_goods[random.Next(_goods.Count)]);
                }

                _clients.Enqueue(new Client(random.Next(minMoney, maxMoney), goods));
            }
        }
    }

    class Client
    {
        private int _money;
        private int _moneyToPay;
        private List<Good> _basket;

        public Client (int money, List<Good> goods)
        {
            _money = money;
            _basket = goods;
        }

        public int GetAmountPurchase()
        {
            int amount = 0;

            for (int i =0; i < _basket.Count;i++)
            {
                amount += _basket[i].Price;
            }

            return amount;
        }

        public void ShowGoods()
        {
            foreach (var good in _basket)
            {
                good.ShowInfo();
            }
        }

        public bool CheckSolvency()
        {
            _moneyToPay = GetAmountPurchase();

            if (_money >= _moneyToPay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ToPay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }

        public void RemoveGood()
        {
            Random random = new Random();
            Good good = _basket[random.Next(_basket.Count)];
            _basket.Remove(good);
            Console.WriteLine($"Убрали из корзины {good.Name}");
        }
    }

    class Good
    {
        public string Name { get; private set; }

        public int Price { get; private set; }

        public Good(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name} price: {Price}");
        }
    }
}