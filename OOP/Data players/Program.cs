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
        private const string ShowDataPlayers = "1";
        private const string AddNewPlayer = "2";
        private const string RemoveIdPlayer = "3";
        private const string BanIdPlayer = "4";
        private const string UnbanIdPlayer = "5";
        private const string Exit = "6";
        private int nextIdNumber = 111;
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Что хотите сделать?");
                Console.WriteLine(ShowDataPlayers + " - Открыть базу данных.\n" + AddNewPlayer + " - Добавить игрока.\n" + RemoveIdPlayer + " - Удалить игрока.\n" + BanIdPlayer + " - Бан по ID.\n" + UnbanIdPlayer + " - Разбан по ID.\n" + Exit + " - Выход");
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
                    case UnbanIdPlayer:
                        UnBanPlayer();
                        Console.ReadKey();
                        break;
                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }

        private void ShowAllPlayers(int playersCount)
        {
            for(int i = 0; i < playersCount; i++)
            {
                _players[i].ShowInfo();
            }
        }

        private int AddPlayer()
        {
            Console.WriteLine("What's name new player?");
            string nameNewPlayer = Console.ReadLine();
            Console.WriteLine("What's level new player?");
            int levelNewPlayer = ReadInt();
            _players.Add(new Player(nextIdNumber, nameNewPlayer, levelNewPlayer, false));
            return nextIdNumber++;
        }

        private void DeletePlayer()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                _players.Remove(player);
                Console.WriteLine("Игрок успешно удалён!");
            }
        }

        private void BanPlayer()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.BanId();
                Console.WriteLine("Игрок забанен!");
            }
        }

        private void UnBanPlayer()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.UnbanId();
                Console.WriteLine("Игрок разбанен!");
            }
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

        private bool TryGetPlayer(out Player player)
        {
            {
                int idToFind;
                bool isPlayer;
                Console.WriteLine("Введите ID игрока.");
                isPlayer = int.TryParse(Console.ReadLine(), out idToFind);

                if (isPlayer)
                {
                    for (int i = 0; i < _players.Count; i++)
                    {
                        if (_players[i].Id == idToFind)
                        {
                            player = _players[i];
                            return true;
                        }
                    }
                }

                Console.WriteLine("Такого игрока нет.");
                player = null;
                return false;
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

        public int Id
        { 
            get 
            { 
                return _id; 
            } 
        }

        public void BanId()
        { 
            _isBan = true;
        }

        public void UnbanId()
        {
            _isBan = false;
        }
    }
}