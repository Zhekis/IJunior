namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Salesman salesman = new Salesman();
            Player player = new Player(7);

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
                Console.WriteLine(ShowProducts + " - Посмотреть товары.\n" + ShowItems + " - Посмотреть свои вещи.\n" + Buy + " - Купить товар.\n" + Exit + " - Выход");
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
                        salesman.SellProduct(player);
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Salesman
    {
        private List<Product> _goods = new List<Product>();

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

        public void SellProduct(Player player)
        {
            ShowGoods();
            if (TryGetProduct(out Product product) == true)
            {
                if(player.BalanceMoney >= product.Price)
                {
                    _goods.Remove(product);
                    player.BuyProduct(product);
                    Console.WriteLine($"Товар {product.Name} куплен");
                }
                else
                {
                    Console.WriteLine("не хватает средств");
                }
            }

            Console.ReadKey();
        }

        private bool TryGetProduct(out Product product)
        {
            {
                int numberToFind;
                bool isProduct;
                Console.WriteLine("Введите номер товара для покупки.");
                isProduct = int.TryParse(Console.ReadLine(), out numberToFind);
                numberToFind--; // Get correct number good.

                if (isProduct)
                {
                    for (int i = 0; i < _goods.Count; i++)
                    {
                        if (i == numberToFind)
                        {
                            product = _goods[i];
                            return true;
                        }
                    }
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

        public void BuyProduct(Product boughtGood)
        {
            _items.Add(boughtGood);
            BalanceMoney -= boughtGood.Price;
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