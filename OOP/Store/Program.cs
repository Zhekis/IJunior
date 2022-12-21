namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    class Salesman
    {
        private List<Product> _goods = new List<Product>();

        public Salesman()
        {
            _goods.Add(new Product("Apple", "Natural product"));
            _goods.Add(new Product("Knife", "Hand waipon"));
            _goods.Add(new Product("Phone", "Device for call"));
            _goods.Add(new Product("Map", "Instructions"));
        }

        public void Work(Player player)
        {
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
                        Show();
                        break;

                    case ShowItems:
                        
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Player
    {
        private List<Product> _items = new List<Product>();

        public Player()
        {
            _items.Add(new Product("Keys", "Things for access"));
            _items.Add(new Product("Glasses", "Protect vision"));
        }
    }

    class Product
    {
        private string _name;
        private string _description;

        public Product(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Name: {_name}, description: {_description}.");
        }
    }
}