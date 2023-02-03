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
        Deported,
        SellingTickets,
        PrepareTrain,
        WaitDeport,
        fail
    }

    class Station
    {
        private Train _train;
        private Status _status = Status.Empty;
        private List<Train> _trains = new List<Train>();
        private Dictionary<Status, string> _needAction = new Dictionary<Status, string>()
        {
            { Status.Empty, ""},
            { Status.Deported, "Создайте направление"},
            { Status.SellingTickets, "Продайте билеты"},
            { Status.PrepareTrain, "Создайте поезд"},
            { Status.WaitDeport, "Отправьте поезд"}
        };

        private string TempRoute { get; set; }
        private int TempCountPassengers { get; set; }

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
                Console.WriteLine("Список поездов:");

                ShowTrains();

                Console.WriteLine();
                Console.WriteLine($"Текущий рейс:\n{TempRoute}Статус: {_status}");
                Console.WriteLine();
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

        private void ShowTrains()
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _trains[i].ShowInfo();
            }
        }

        private void CreateRoute()
        {
            if (_status == Status.Empty || _status == Status.Deported)
            {
                _train = new Train();
                Console.WriteLine("Введите пункт отправления:");
                string startPoint = Console.ReadLine();
                Console.WriteLine("Введите пункт прибытия:");
                string endPoint = Console.ReadLine();
                string route = $"Направление '{startPoint} - {endPoint}'";
                Console.WriteLine($"{route} создано.");
                TempRoute = route + " ";

                _train.TakeRoute(route);

                _status = Status.SellingTickets;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void SellTickets()
        {
            int minNumberPassengers = 500;
            int maxNumberPassengers = 1000;

            if (_status == Status.SellingTickets)
            {
                Random random = new Random();
                TempCountPassengers = random.Next(minNumberPassengers, maxNumberPassengers);
                Console.WriteLine($"Продано {TempCountPassengers} билетов.");
                _status = Status.PrepareTrain;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void CreateTrain()
        {
            if (_status == Status.PrepareTrain)
            {
                Console.WriteLine("Введите вместимость вагона:");
                int countPlacesForCarriage = ReadInt();
                int countCarriages = TempCountPassengers / countPlacesForCarriage;

                if (TempCountPassengers % countPlacesForCarriage > 0)
                {
                    countCarriages++;
                }

                _train.TakeInfoCarriage(countCarriages, countPlacesForCarriage);
                Console.WriteLine($"Поезд {_train.Route} создан, количество пассажиров {TempCountPassengers}, вагоны {countCarriages} шт.");
                _status = Status.WaitDeport;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void SendTrain()
        {
            if (_status == Status.WaitDeport)
            {
                _trains.Add(_train);
                Console.WriteLine("Поезд отправлен.");
                _status = Status.Deported;
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
            if (_status == Status.Empty || _status == Status.Deported)
            {
                return _needAction[Status.Deported];
            }
            else if (_status == Status.SellingTickets)
            {
                return _needAction[Status.SellingTickets];
            }
            else if (_status == Status.PrepareTrain)
            {
                return _needAction[Status.PrepareTrain];
            }
            else if (_status == Status.WaitDeport)
            {
                return _needAction[Status.WaitDeport];
            }
            else
            {
                return "";
            }
        }
    }

    class Train
    {
        public int CountCarriages { get; private set; }
        public int CountPlacesForCarriage { get; private set; }
        public string Route { get; private set; }

        public void TakeRoute(string route)
        {
            Route = route;
        }

        public void TakeInfoCarriage(int countCarriages, int countPlacesForCarriage)
        {
            CountCarriages = countCarriages;
            CountPlacesForCarriage = countPlacesForCarriage;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Route}, вагоны {CountCarriages} шт.");
        }
    }
}