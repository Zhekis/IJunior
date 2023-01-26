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

    enum Status
    {
        Empty,
        Deport,
        Sell,
        Prepare,
        Wait,
        fail
    }

    class Station
    {
        private Status status = Status.Empty;
        private List<Train> _trains = new List<Train>();
        private string _route;
        private string _startPoint;
        private string _endPoint;
        private int _countPassengers;

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
                Console.WriteLine($"{_route}Статус: {status}");
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
            if (status == Status.Empty || status == Status.Deport)
            {
                Console.WriteLine("Введите пункт отправления:");
                _startPoint = Console.ReadLine();
                Console.WriteLine("Введите пункт прибытия:");
                _endPoint = Console.ReadLine();
                _route = $"Направление {_startPoint} - {_endPoint} ";
                Console.WriteLine($"{_route} создано.");
                status = Status.Sell;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        public void SellTickets()
        {
            if (status == Status.Sell)
            {
                Random random = new Random();
                _countPassengers = random.Next(500, 1000);
                Console.WriteLine($"Продано {_countPassengers} билетов.");
                status = Status.Prepare;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        public void CreateTrain()
        {
            if (status == Status.Prepare)
            {
                Console.WriteLine("Введите вместимость вагона:");
                int countPlacesForCarriage = ReadInt();
                int countCarriages = _countPassengers / countPlacesForCarriage;

                if (_countPassengers % countPlacesForCarriage > 0)
                {
                    countCarriages++;
                }

                Train train = new Train(countCarriages, countPlacesForCarriage, _route);
                Console.WriteLine($"Поезд {_route} создан, количество пассажиров {_countPassengers}, вагоны {countCarriages} шт.");
                status = Status.Wait;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        public void SendTrain()
        {
            if (status == Status.Wait)
            {
                Console.WriteLine("Поезд отправлен.");
                status = Status.Deport;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

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

        private string GetRequiredAction()
        {
            if (status == Status.Empty || status == Status.Deport)
            {
                return "Создайте направление";
            }
            else if (status == Status.Sell)
            {
                return "Продайте билеты";
            }
            else if (status == Status.Prepare)
            {
                return "Создайте поезд";
            }
            else if (status == Status.Wait)
            {
                return "Отправьте поезд";
            }
            else
            {
                return "";
            }
        }
    }

    class Train
    {
        private List<Carriage> _carriages;
        public int CountCarriages { get; private set; }
        public int CountPlacesForCarriage { get; private set; }
        public string Route { get; private set; }

        public Train(int countCorriages, int countPlacesForCarriage, string route)
        {
            CountCarriages = countCorriages;
            CountPlacesForCarriage = countPlacesForCarriage;
            _carriages = new List<Carriage>();
            Route = route;
            CreateCarriage(countCorriages, countPlacesForCarriage);
        }

        private void CreateCarriage(int countCorriages, int countPlacesForCarriage)
        {
            for (int i = 0; i < countCorriages; i++)
            {
                _carriages.Add(new Carriage(countPlacesForCarriage));
            }
        }
    }

    class Carriage
    {
        public int CountPlaces { get; private set; }
        public Carriage(int countPlaces)
        {
            CountPlaces = countPlaces;
        }
    }
}