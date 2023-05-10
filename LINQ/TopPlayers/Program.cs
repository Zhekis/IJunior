using System.Numerics;

namespace TopPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player> {
            new Player("RobertPupkin", 1, 100),
            new Player("LarisaDolina", 2, 150),
            new Player("BorisBritva", 1, 120),
            new Player("AndreyMorj", 3, 180),
            new Player("SamPopov", 3, 190),
            new Player("GorgWashington", 3, 200),
            new Player("PetrSidorov", 4, 300),
            new Player("VasiliyPervii", 4, 350),
            new Player("IvanGraga", 6, 400),
            new Player("MelisaKirova", 5, 450)};
            int numberTopPlayers = 3;
            Terminal terminal = new Terminal();
            var filteredPlayers = players.OrderByDescending(player => player.Level).Take(numberTopPlayers);
            Console.WriteLine("TOP3 LEVEL:");
            terminal.ShowPlayers(filteredPlayers);
            Console.WriteLine();
            Console.WriteLine("TOP3 POWER:");
            filteredPlayers = players.OrderByDescending(player => player.Power).Take(numberTopPlayers);
            terminal.ShowPlayers(filteredPlayers);
        }
    }

    class Terminal
    {
        public void ShowPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.ShowInfo();
            }
        }
    }

    class Player
    {
        public Player(string fullName, int level, int power)
        {
            FullName = fullName;
            Level = level;
            Power = power;
        }

        public string FullName { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{FullName}, Level: {Level}, Power: {Power}");
        }
    }
}