namespace Data_players
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Data dataPlayers = new Data();
            dataPlayers.Work();
        }
    }

    class Data
    {
        const string ShowDataPlayers = "1";
        const string AddNewPlayer = "2";
        const string RemoveIdPlayer = "3";
        const string BanIdPlayer = "4";
        const string Exit = "5";
        string userInput;
        bool isWork = true;
        int nextIdNumber = 111;
        private List<Player> _players = new List<Player>();

        public Data(/*List<Player> players*/)
        {
            //_players = players;
        }

        public void ShowAllPlayers(int playersCount)
        {
            for(int i = 0; i < playersCount; i++)
            {
                _players[i].ShowInfo();
            }
        }

        public int AddPlayer()
        {
            Console.WriteLine("What's name new player?");
            string nameNewPlayer = Console.ReadLine();
            Console.WriteLine("What's level new player?");
            int levelNewPlayer = ReadInt();
            _players.Add(new Player(nextIdNumber, nameNewPlayer, levelNewPlayer, false));
            return nextIdNumber++;
        }

        public void DeletePlayer()
        {
            Console.WriteLine("What's ID delete?");
            int idPlayer = ReadInt();

            for (int i = 0; i < _players.Count; i++)
            {
                if (idPlayer == _players[i].GetId())
                {
                    Console.Write("Deleted : ");
                    _players[i].ShowInfo();
                    _players.RemoveAt(i);
                }
                else
                {
                    Console.WriteLine("Not find");
                }
            }
        }

        public void BanPlayer()
        {
            Console.WriteLine("What's ID ban?");
            int idPlayer = ReadInt();

            for (int i = 0; i < _players.Count; i++)
            {
                if (idPlayer == _players[i].GetId())
                {
                    Console.Write("Ban : ");
                    _players[i].BanId();
                    _players[i].ShowInfo();
                }
                else
                {
                    Console.WriteLine("Not find");
                }
            }
        }

        public int ReadInt()
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

        public void Work()
        {
            while(isWork)
            {
                Console.Clear();
                Console.WriteLine("Что хотите сделать?");
                Console.WriteLine(ShowDataPlayers + " - Открыть базу данных.\n" + AddNewPlayer + " - Добавить игрока.\n" + RemoveIdPlayer + " - Удалить игрока.\n" + BanIdPlayer + " - Бан по ID.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowDataPlayers:
                        ShowAllPlayers(_players.Count);
                        Console.ReadKey();
                        break;
                    case AddNewPlayer:
                        AddPlayer();
                        Console.ReadKey();
                        break;
                    case RemoveIdPlayer:
                        DeletePlayer();
                        Console.ReadKey();
                        break;
                    case BanIdPlayer:
                        BanPlayer();
                        Console.ReadKey();
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
        private int _id;
        private string _name;
        private int _level;
        private bool _isBan;

        public Player(int id, string name, int level, bool isBan)
        {
            _id = id;
            _name = name;
            _level = level;
            _isBan = isBan;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Name player : " + _name + " Level : " + _level + " ID : " + _id + " Ban : " + _isBan);
        }

        public int GetId()
            { return _id; }

        public void BanId()
        { _isBan = true; }
    }
}