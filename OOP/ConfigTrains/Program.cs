namespace ConfigTrains
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Station station = new Station();
            station.Work();
        }
    }

    class Station
    {
        private string _route;
        private string _startPoint;
        private string _endPoint;
        private string _statusRoute = "";
        private int _countPassengers;
        private Train train;

        public void Work()
        {
            const string CreateWay = "1";
            const string Sell = "2";
            const string AddTrain = "3";
            const string Departure = "4";
            const string Exit = "5";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"{_route}Статус: {_statusRoute}");
                Console.WriteLine(CreateWay + " - Создать направление.\n" + Sell + " - продать билеты.");
                Console.WriteLine(AddTrain + " - создать поезд.\n" + Departure + " - Отправить поезд.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CreateWay:
                        CreateRoute();
                        break;

                    case Sell:
                        SellTickets();
                        break;

                    case AddTrain:
                        CreateTrain();
                        break;

                    case Departure:
                        SendTrain();
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }
        public void CreateRoute()
        {
            if (_statusRoute == "")
            {
                Console.WriteLine("Введите пункт отправления:");
                _startPoint = Console.ReadLine();
                Console.WriteLine("Введите пункт прибытия:");
                _endPoint = Console.ReadLine();
                _route = $"Направление {_startPoint} - {_endPoint} ";
                Console.WriteLine($"{_route} создано.");
                _statusRoute = "Продажа билетов";
            }
            else
            {
                Console.WriteLine("Завершите отправку поезда");
            }

            Console.ReadKey();
        }

        public void SellTickets()
        {
            Random random = new Random();
            _countPassengers = random.Next(500, 1000);
            Console.WriteLine($"Продано {_countPassengers} билетов.");
            _statusRoute = "Подготовка поезда";
            Console.ReadKey();
        }

        public void CreateTrain()
        {
            Console.WriteLine("Введите вместимость вагона:");
            int countPlacesForCarriage = ReadInt();
            int countCarriages = Math.Ceiling(_countPassengers / countPlacesForCarriage);
            Train train = new Train(countCarriages, countPlacesForCarriage, _startPoint, _endPoint);
            Console.WriteLine($"Поезд создан, количество пассажиров {_countPassengers}, вагоны {countCarriages} шт.");
            _statusRoute = "Ожидается отправка";
            Console.ReadKey();
        }

        public void SendTrain()
        {
            Console.WriteLine("Поезд отправлен.");
            _statusRoute = "";
            Console.ReadKey();
        }

        private int ReadInt()
        {
            bool isNumber = false;
            int result = 0;

            while (isNumber == false)
            {
                string userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out int number);

                if (isNumber == true)
                    result = number;
                else
                    Console.WriteLine("Введите число.");
            }

            return result;
        }
    }

    class Train
    {
        private List<Carriage> _carriages;
        public int CountCarriages { get; private set; }
        public int CountPlacesForCarriage { get; private set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        public Train(int countCorriages, int countPlacesForCarriage, string startPoint, string endtPoint)
        {
            CountCarriages = countCorriages;
            CountPlacesForCarriage = countPlacesForCarriage;
            _carriages = new List<Carriage>();
            StartPoint = startPoint;
            EndPoint = endtPoint;
            CreateCarriage(countCorriages, countPlacesForCarriage);

        }

        public void CreateCarriage(int countCorriages, int countPlacesForCarriage)
        {
            for (int i = 0; i < countCorriages; i++)
            {
                _carriages.Add(new Carriage(countPlacesForCarriage));
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Поезд с количеством вагонов {CountCarriages}. Вагоны вместимостью {CountPlacesForCarriage} мест.");
        }
    }

    class Carriage
    {
        public int CountPlaces { get; private set; }
        public Carriage(int countPlaces)
        {
            CountPlaces = countPlaces;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Вагон вместимостью {CountPlaces} мест.");
        }
    }
}