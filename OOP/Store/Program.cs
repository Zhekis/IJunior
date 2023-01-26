using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;

namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Salesman salesman = new Salesman(5);
            Player player = new Player(7);
            Market market = new Market();
            market.Work(salesman, player);

        }
    }

    class Market
    {
        public void Work(Salesman salesman, Player player)
        {
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
                Console.WriteLine(ShowProducts + " - Посмотреть товары продавца.\n" + ShowItems + " - Посмотреть свои вещи.");
                Console.WriteLine(Buy + " - Купить товар.\n" + Exit + " - Выход");
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
                        MakeDeal(salesman, player);
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }

        public void MakeDeal(Salesman salesman, Player player)
        {
            salesman.ShowGoods();

            int receivedMoney;

            if (salesman.TryGetProduct(out Product product) == true)
            {
                if (player.Money >= product.Price)
                {
                    receivedMoney = player.BuyProduct(product);
                    salesman.SellProduct(product, receivedMoney);
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
        protected List<Product> Goods = new List<Product>();
        protected int BalanceMoney;
        public Participant(int balanceMoney) => BalanceMoney = balanceMoney;

        public void ShowGoods()
        {
            Console.Clear();

            for (int i = 0; i < Goods.Count; i++)
            {
                Console.WriteLine($"Number {i + 1}");
                Goods[i].ShowInfo();
            }

            Console.WriteLine("Нажмите, чтобы продолжить!");
            Console.ReadKey();
            Console.WriteLine();
        }
    }

    class Salesman : Participant
    {
        public Salesman(int balanceMoney) : base(balanceMoney)
        {
            Goods.Add(new Product("Apple", 1));
            Goods.Add(new Product("Knife", 3));
            Goods.Add(new Product("Phone", 5));
            Goods.Add(new Product("Map", 2));
        }

        public void SellProduct(Product soldProduct, int gettingMoney)
        {
            Goods.Remove(soldProduct);
            BalanceMoney += gettingMoney;
        }

        public bool TryGetProduct(out Product product)
        {
            int numberToFind;
            Console.WriteLine("Введите номер товара для покупки.");
            bool isNumber = int.TryParse(Console.ReadLine(), out numberToFind);

            if (numberToFind > 0 && numberToFind <= Goods.Count)
            {
                numberToFind--;
                product = Goods[numberToFind];
                return true;
            }

            Console.WriteLine("Такого товара нет.");
            product = null;
            return false;
        }
    }

    class Player : Participant
    {
        public Player(int balanceMoney) : base(balanceMoney)
        {
            Goods.Add(new Product("Keys", 2));
            Goods.Add(new Product("Glasses", 3));
        }

        public int Money => BalanceMoney;

        public int BuyProduct(Product boughtGood)
        {
            Goods.Add(boughtGood);
            BalanceMoney -= boughtGood.Price;
            Console.WriteLine($"У вас осталось денег: {BalanceMoney}");
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