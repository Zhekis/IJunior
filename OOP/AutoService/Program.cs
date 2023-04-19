namespace AutoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Service service = new Service();
            service.Show();
        }
    }

    class Service
    {
        private int _balanceMoney = 100000;
        private bool _serviceCompleted = false;
        private List<Detail> _details;
        private Dictionary<string, int> _countParts = new Dictionary<string, int>();
        private Queue<Car> _cars = new Queue<Car>();
        //private List<string> _namesDetails = new List<string>() { "Колесный диск", "Шина", "Радиатор", 
        //    "Катализатор", "Масло", "Воздушный фильтр", "Шарнир колеса", "Прокладка ГБЦ", "Руль", "Рычаг подвески", "Сайлентблоки", "Пружины", "Стойка стабилизатора",
        //    "Тормозные колодки", "Лобовое стекло", "Подшипник ступицы"};

        public Service()
        {
            Random random = new Random();
            _details = new List<Detail>() {new Detail("Колесный диск", 5000, 200), new Detail("Шина", 4000, 200), new Detail("Радиатор", 8000, 2000), 
            new Detail("Катализатор", 10000, 5000), new Detail("Масло", 5000, 1000), new Detail("Воздушный фильтр", 1000, 500),
            new Detail("Шарнир колеса", 7000, 3000), new Detail("Прокладка ГБЦ", 1000, 3000), new Detail("Подушка безопасности", 4000, 2000), new Detail("Рычаг подвески", 3000, 1000),
            new Detail("Сайлентблоки", 5000, 2000), new Detail("Пружины", 7000, 3000), new Detail("Стойка стабилизатора", 1000, 1000), new Detail("Тормозные колодки", 3000, 1000),
            new Detail("Лобовое стекло", 15000, 2000), new Detail("Подшипник ступицы", 7000, 2000),};
            CreateStock(random);
        }

        public void Work()
        {
            while (_clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"В очереди {_clients.Count} человек");
                Client client = _clients.Dequeue();
                Console.WriteLine($"Клиент у кассы. Сумма к оплате {client.GetTotalPrice()}.");

                while (_purchaseCompleted == false)
                {
                    if (client.IsEnoughMoney())
                    {
                        _money += client.ToPay();
                        Console.WriteLine("Покупка оплачена.");
                        _purchaseCompleted = true;
                    }
                    else
                    {
                        Console.Write("У клиента не хватает денег.");
                        client.RemoveRandomGood();
                        Console.WriteLine("Нажмите для продолжения.");
                        Console.ReadKey();
                    }
                }

                Console.WriteLine("Нажмите для обслуживания следующего клиента.");
                _purchaseCompleted = false;
                Console.ReadKey();
            }
        }

        public void Show()
        {
            int indexAddition = 1;

            for (int i = 0; i < _details.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                Console.WriteLine(_details[i].GetInfo());
            }

            foreach (var detail in _countParts)
            {
                Console.WriteLine($"Detail: {detail.Key}  value: {detail.Value}");
            }
        }

        private void CreateStock(Random random)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                _countParts.Add(_details[i].Name, random.Next(10, 20));
            }
        }
    }

    class Car
    {

    }

    //class Detail
    //{

    //}

    public struct Detail
    {
        public Detail(string name, int price, int repairPrice)
        {
            Name = name;
            Price = price;
            RepairPrice = repairPrice;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
        public int RepairPrice { get; private set; }

        public string GetInfo()
        {
            return $"Name: {Name}, price: {Price}, {RepairPrice}";
        }
    }
}