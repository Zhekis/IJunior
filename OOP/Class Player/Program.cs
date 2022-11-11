namespace Class_Player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Zhek", 10, 100);

            player.ShowInfo();
        }
    }

    class Player
    {
        private string _name;
        private int _level;
        private int _health;

        public Player(string name, int level, int health)
        {
            _name = name;
            _level = level;
            _health = health;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Name player : " + _name + "\nLevel : " + _level + "\nHealth : " + _health);
        }
    }
}