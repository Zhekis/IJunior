using System.Collections.Generic;
using System.Security.Cryptography;

namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> goods = new List<Product>();
            List<Product> items = new List<Product>();
            Salesman salesman = new Salesman(5, goods);
            Player player = new Player(7, items);
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
                        break;

                    case ShowItems:
                        player.ShowGoods();
                        break;

                    case Buy:
                        market.MakeDeal(salesman, player);
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
        public void MakeDeal(Salesman salesman, Player player)
        {
            salesman.ShowGoods();

            int receivedMoney;

            if (salesman.TryGetProduct(out Product product) == true)
            {
                if (player.BalanceMoney >= product.Price)
                {
                    salesman.SellProduct(product);
                    receivedMoney = player.BuyProduct(product);
                    salesman.TakeMoney(receivedMoney);
                    Console.WriteLine($"Товар {product.Name} куплен");
                }
                else
                {
                    Console.WriteLine("Не хватает средств.");
                }
            }

            Console.ReadKey();
        }
    }

    class Participant
    {
        protected List<Product> _goods;
        protected int _balanceMoney;
        public Participant(int balanceMoney, List<Product> goods)
        {
            _goods = goods;
            _balanceMoney = balanceMoney;
        }

        public void ShowItems()
        {
            Console.Clear();
            Console.WriteLine($"Money: {_balanceMoney}");

            for (int i = 0; i < _goods.Count; i++)
            {
                Console.WriteLine(_goods[i].Name);
            }

            Console.ReadKey();
        }

        public void ShowGoods()
        {
            Console.Clear();
            Console.WriteLine($"Money: {_balanceMoney}");

            for (int i = 0; i < _goods.Count; i++)
            {
                Console.WriteLine($"Number {i + 1}");
                _goods[i].ShowInfo();
            }

            Console.ReadKey();
        }
    }

    class Salesman : Participant
    {
        public Salesman(int balanceMoney, List<Product> goods) : base(balanceMoney, goods)
        {
            _goods.Add(new Product("Apple", 1));
            _goods.Add(new Product("Knife", 3));
            _goods.Add(new Product("Phone", 5));
            _goods.Add(new Product("Map", 2));
        }

        public void SellProduct(Product soldProduct)
        {
            _goods.Remove(soldProduct);
        }

        public void TakeMoney(int gettingMoney)
        {
            _balanceMoney += gettingMoney;
            Console.WriteLine($"Баланс продавца: {_balanceMoney}");
        }

        public bool TryGetProduct(out Product product)
        {
            {
                int numberToFind;
                Console.WriteLine("Введите номер товара для покупки.");
                bool isNumber = int.TryParse(Console.ReadLine(), out numberToFind);

                if (numberToFind > 0 && numberToFind <= _goods.Count)
                {
                    numberToFind--;
                    product = _goods[numberToFind];
                    return true;
                }

                Console.WriteLine("Такого товара нет.");
                product = null;
                return false;
            }
        }
    }

    class Player : Participant
    {
        public Player(int balanceMoney, List<Product> items) : base(balanceMoney, items)
        {
            _goods.Add(new Product("Keys", 2));
            _goods.Add(new Product("Glasses", 3));
        }

        public int BalanceMoney
        {
            get
            {
                return _balanceMoney;
            }
        }

        public int BuyProduct(Product boughtGood)
        {
            _goods.Add(boughtGood);
            _balanceMoney -= boughtGood.Price;
            Console.WriteLine($"У вас осталось {_balanceMoney}");
            return boughtGood.Price;
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; private set; }
        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, price: {Price}.");
        }
    }
}