namespace ArmsReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Soldier> soldiers = new List<Soldier> {
            new Soldier("Ridik", "Tankman", "Officer", 5),
            new Soldier("Patron", "Driver", "Sergeant", 7),
            new Soldier("Balbes", "Infantry", "Common", 3),
            new Soldier("Duren", "Sniper", "Major", 12),
            new Soldier("Boris", "Scout", "Sergeant", 10)};
            string name = "Boris";
            string rank = "Sergeant";

            var filteredSoldiers = soldiers.Where(soldier => soldier.Name == name && soldier.Rank == rank);

            foreach (var soldier in filteredSoldiers)
            {
                soldier.ShowInfo();
            }
        }

    }

    class Soldier
    {
        public Soldier(string name, string armament, string rank, int timeService)
        {
            Name = name;
            Armament = armament;
            Rank = rank;
            TimeService = timeService;
        }

        public string Name { get; private set; }
        public string Armament { get; private set; }
        public string Rank { get; private set; }
        public int TimeService { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, {Rank}");
        }
    }
}