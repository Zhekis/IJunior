using System.Security.Cryptography;

namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Salesman salesman = new Salesman();
            Player player = new Player(7);
            Market market = new Market();

            const string ShowProducts = "1";
            const string ShowItems = "2";
            const string Buy = "3";
            const string Exit = "4";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Магазин. Что хотите сделать?");
                Console.WriteLine(ShowProducts + " - Посмотреть товары продавца.\n" + ShowItems + " - Посмотреть свои вещи.\n" + Buy + " - Купить товар.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowProducts:
                        salesman.ShowGoods();
                        Console.ReadKey();
                        break;

                    case ShowItems:
                        player.ShowItems();
                        break;

                    case Buy:
                        market.Traid(salesman, player);
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Market
    {
        public void Traid(Salesman salesman, Player player)
        {
            salesman.ShowGoods();

            int receivedMoney;
            bool isProduct = salesman.TryGetProduct(out Product product);
            Product boughtGood = product;

            if (isProduct == true)
            {
                if (player.BalanceMoney >= boughtGood.Price)
                {
                    salesman.SellProduct(boughtGood);
                    receivedMoney = player.BuyProduct(boughtGood);
                    salesman.TakeMoney(receivedMoney);
                    Console.WriteLine($"Товар {boughtGood.Name} куплен");
                }
                else
                {
                    Console.WriteLine("Не хватает средств.");
                }
            }

            Console.ReadKey();
        }
    }

    class Salesman
    {
        private List<Product> _goods = new List<Product>();
        private int _money;

        public Salesman()
        {
            _goods.Add(new Product("Apple", 1));
            _goods.Add(new Product("Knife", 3));
            _goods.Add(new Product("Phone", 5));
            _goods.Add(new Product("Map", 2));
        }

        public void ShowGoods()
        {
            Console.Clear();

            for (int i = 0; i < _goods.Count; i++)
            {
                Console.WriteLine($"Number {i + 1}");
                _goods[i].ShowInfo();
            }
        }

        public void SellProduct(Product soldProduct)
        {
            _goods.Remove(soldProduct);
        }

        public void TakeMoney(int gettingMoney)
        {
            _money += gettingMoney;
            Console.WriteLine($"Баланс продавца: {_money}");
        }

        public bool TryGetProduct(out Product product)
        {
            {
                int numberToFind;
                bool isNumber;
                Console.WriteLine("Введите номер товара для покупки.");
                isNumber = int.TryParse(Console.ReadLine(), out numberToFind);

                if (numberToFind > 0 && numberToFind <= _goods.Count)
                {
                    numberToFind--; // Get correct number good.
                    product = _goods[numberToFind];
                    return true;
                }

                Console.WriteLine("Такого товара нет.");
                product = null;
                return false;
            }
        }
    }

    class Player
    {
        private List<Product> _items = new List<Product>();

        public int BalanceMoney { get; private set; }

        public Player(int balanceMoney)
        {
            _items.Add(new Product("Keys", 2));
            _items.Add(new Product("Glasses", 3));
            BalanceMoney = balanceMoney;
        }

        public int BuyProduct(Product boughtGood)
        {
            _items.Add(boughtGood);
            BalanceMoney -= boughtGood.Price;
            return boughtGood.Price;
            Console.WriteLine($"У вас осталось {BalanceMoney}");
        }

        public void ShowItems()
        {
            Console.Clear();
            Console.WriteLine($"Money: { BalanceMoney}");

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine(_items[i].Name);
            }

            Console.ReadKey();
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, price: {Price}.");
        }
    }
}