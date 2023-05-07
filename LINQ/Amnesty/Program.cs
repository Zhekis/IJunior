namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Criminal> criminals = new List<Criminal> { new Criminal("RobertPupkin", 180, 75, true, "Татарин", "Anti-government") ,
                new Criminal("BorisJonson", 180, 75, true, "Чукча", "Kill") , new Criminal("AndreiKosov", 180, 70, true, "Еврей", "Anti-government"),
            new Criminal("Vasiliy", 180, 75, true, "Татарин", "Steal")};

            foreach (var criminal in criminals)
            {
                criminal.ShowInfo();
            }

            string offenseAmnesty = "Anti-government";
            Console.WriteLine();
            var filteredCriminals = criminals.Where(crimilal => crimilal.Offense != offenseAmnesty);
            criminals = filteredCriminals.ToList();
            Console.WriteLine("After amnesty:");

            foreach (var criminal in criminals)
            {
                criminal.ShowInfo();
            }
        }
    }

    class Criminal
    {
        public Criminal(string fullName, int height, int weight, bool isIncarcerated, string nationality, string offense)
        {
            FullName = fullName;
            Height = height;
            Weight = weight;
            IsIncarcerated = isIncarcerated;
            Nationality = nationality;
            Offense = offense;
        }

        public string FullName { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public bool IsIncarcerated { get; private set; }
        public string Nationality { get; private set; }
        public string Offense { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{FullName}, {Offense}");
        }
    }
}