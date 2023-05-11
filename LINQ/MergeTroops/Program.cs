﻿namespace MergeTroops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Soldier> troops1 = new List<Soldier> {
            new Soldier("Ridik", "Tankman", "Officer", 5),
            new Soldier("Patron", "Driver", "Sergeant", 7),
            new Soldier("Balbes", "Infantry", "Common", 3),
            new Soldier("Duren", "Sniper", "Major", 12),
            new Soldier("Boris", "Scout", "Sergeant", 10)};

            List<Soldier> troops2 = new List<Soldier> {
            new Soldier("Ridik", "Tankman", "Officer", 5),
            new Soldier("Patron", "Driver", "Sergeant", 7),
            new Soldier("Duren", "Sniper", "Major", 12),
            new Soldier("Alex", "Scout", "Sergeant", 10)};

            string searchingFirstNameChar = "B";

            var filteredSoldiers = troops1.Where(soldier => soldier.Name.ToUpper().StartsWith(searchingFirstNameChar));

            troops1 = troops1.Except(filteredSoldiers).ToList();
            troops2 = troops2.Union(filteredSoldiers).ToList();
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
    }
}