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
        Train train;
        private Status status = Status.Empty;
        private List<Train> _trains = new List<Train>();
        private string tempRoute = "";
        private int tempCountPassengers;

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

                for (int i = 0; i < _trains.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    _trains[i].ShowInfo();
                }

                Console.WriteLine();
                Console.WriteLine($"Текущий рейс:\n{tempRoute}Статус: {status}");
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

        private void CreateRoute()
        {
            if (status == Status.Empty || status == Status.Deported)
            {
                train = new Train();
                Console.WriteLine("Введите пункт отправления:");
                string startPoint = Console.ReadLine();
                Console.WriteLine("Введите пункт прибытия:");
                string endPoint = Console.ReadLine();
                string route = $"Направление {startPoint} - {endPoint}";
                Console.WriteLine($"{route} создано.");
                tempRoute = route;

                train.GetRoute(route);

                status = Status.SellingTickets;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void SellTickets()
        {
            if (status == Status.SellingTickets)
            {
                Random random = new Random();
                tempCountPassengers = random.Next(500, 1000);
                Console.WriteLine($"Продано {tempCountPassengers} билетов.");
                status = Status.PrepareTrain;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void CreateTrain()
        {
            if (status == Status.PrepareTrain)
            {
                Console.WriteLine("Введите вместимость вагона:");
                int countPlacesForCarriage = ReadInt();
                int countCarriages = tempCountPassengers / countPlacesForCarriage;

                if (tempCountPassengers % countPlacesForCarriage > 0)
                {
                    countCarriages++;
                }

                train.GetInfoCarriage(countCarriages, countPlacesForCarriage);
                Console.WriteLine($"Поезд {train.Route} создан, количество пассажиров {tempCountPassengers}, вагоны {countCarriages} шт.");
                status = Status.WaitDeport;
            }
            else
            {
                Console.WriteLine(GetRequiredAction());
            }

            Console.ReadKey();
        }

        private void SendTrain()
        {
            if (status == Status.WaitDeport)
            {
                _trains.Add(train);
                Console.WriteLine("Поезд отправлен.");
                status = Status.Deported;
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
            if (status == Status.Empty || status == Status.Deported)
            {
                return "Создайте направление";
            }
            else if (status == Status.SellingTickets)
            {
                return "Продайте билеты";
            }
            else if (status == Status.PrepareTrain)
            {
                return "Создайте поезд";
            }
            else if (status == Status.WaitDeport)
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
        public int CountCarriages { get; private set; }
        public int CountPlacesForCarriage { get; private set; }
        public string Route { get; private set; }

        public void GetRoute(string route)
        {
            Route = route;
        }

        public void GetInfoCarriage(int countCarriages, int countPlacesForCarriage)
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