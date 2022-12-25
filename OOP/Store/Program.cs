namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Salesman salesman = new Salesman();
            Player player = new Player();

            const string ShowProducts = "1";
            const string ShowItems = "2";
            const string Exit = "3";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Магазин. Что хотите сделать?");
                Console.WriteLine(ShowProducts + " - Посмотреть товары.\n" + ShowItems + " - Посмотреть свои вещи.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowProducts:
                        salesman.ShowGoods();
                        break;

                    case ShowItems:
                        player.BuyProduct();
                        player.ShowItems();
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
            for (int i = 0; i < _goods.Count; i++)
            {
                Console.WriteLine($"Number {i + 1}");
                _goods[i].ShowInfo();
            }

            Console.ReadKey();
        }
    }

    class Player
    {
        private int _balanceMoney = 50;
        private List<Product> _items = new List<Product>();

        public Player()
        {
            _items.Add(new Product("Keys", 2));
            _items.Add(new Product("Glasses", 3));
        }

        public void BuyProduct()
        {
            _balanceMoney -= _items[0].Price;
            Console.WriteLine(_balanceMoney);
        }

        public void ShowItems()
        {
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