namespace Class_Player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player hero = new Player("Zhek", 10, 100);

            hero.ShowInfo();
        }
    }

    class Player
    {
        public string Name;
        public int Level;
        public int Health;

        public Player(string name, int level, int health)
        {
            Name = name;
            Level = level;
            Health = health;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Name player : " + Name + "\nLevel : " + Level + "\nHealth : " + Health);
        }
    }
}