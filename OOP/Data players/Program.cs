namespace Data_players
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Data
    {
        private List<Player> _players = new List<Player>();

        public Data(List<Player> players)
        {
            _players = players;
        }

        public void ShowAllPlayers(int playersCount)
        {
            for(int i = 0; i < playersCount; i++)
            {
                Players[i].ShowInfo();
            }
        }

        public void AddPlayer()
        {

        }

        public void DeletePlayer()
        {

        }

        public void BanPlayer()
        {

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
            Console.WriteLine("Name player : " + _name + "\nLevel : " + _level + "\nID : " + _id);
        }
    }
}