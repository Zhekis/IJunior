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
        private int nextIdNumber = 111;
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            const string ShowPlayers = "1";
            const string AddNewPlayer = "2";
            const string RemovePlayer = "3";
            const string BanPlayer = "4";
            const string UnbanPlayer = "5";
            const string Exit = "6";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Что хотите сделать?");
                Console.WriteLine(ShowPlayers + " - Открыть базу данных.\n" + AddNewPlayer + " - Добавить игрока.");
                Console.WriteLine(RemovePlayer + " - Удалить игрока.\n" + BanPlayer + " - Бан по ID.\n" + UnbanPlayer + " - Разбан по ID.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowPlayers:
                        ShowAllPlayers();
                        break;

                    case AddNewPlayer:
                        Add();
                        break;

                    case RemovePlayer:
                        Delete();
                        break;

                    case BanPlayer:
                        Ban();
                        break;

                    case UnbanPlayer:
                        Unban();
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }
            }
        }

        private void ShowAllPlayers()
        {
            for(int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }

            Console.ReadKey();
        }

        private int Add()
        {
            Console.WriteLine("What's name new player?");
            string nameNewPlayer = Console.ReadLine();
            Console.WriteLine("What's level new player?");
            int levelNewPlayer = ReadInt();
            _players.Add(new Player(nextIdNumber, nameNewPlayer, levelNewPlayer, false));
            return nextIdNumber++;
            Console.ReadKey();
        }

        private void Delete()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                _players.Remove(player);
                Console.WriteLine("Игрок успешно удалён!");
            }

            Console.ReadKey();
        }

        private void Ban()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.Ban();
                Console.WriteLine("Игрок забанен!");
            }

            Console.ReadKey();
        }

        private void Unban()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.Unban();
                Console.WriteLine("Игрок разбанен!");
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
        private bool _isBanned;

        public Player(int id, string name, int level, bool isBan)
        {
            _id = id;
            _name = name;
            _level = level;
            _isBanned = isBan;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Name player : " + _name + " Level : " + _level + " ID : " + _id + " Ban : " + _isBanned);
        }

        public int Id
        { 
            get 
            { 
                return _id; 
            } 
        }

        public void Ban()
        { 
            _isBanned = true;
        }

        public void Unban()
        {
            _isBanned = false;
        }
    }
}