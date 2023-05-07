namespace SearchCriminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Criminal> criminals = new List<Criminal> { new Criminal("RobertPupkin", 180, 75, true, "Татарин") , 
                new Criminal("BorisJonson", 180, 75, false, "Чукча") , new Criminal("AndreiKosov", 180, 70, true, "Еврей"),
            new Criminal("Vasiliy", 180, 75, false, "Татарин")};

            Search search = new Search();
            search.SearchCriminals(criminals);
        }
    }

    class Search
    {
        public void SearchCriminals(List<Criminal> criminals) 
        {
            Console.WriteLine("Input height,weight,nationality.");
            int height = ReadInt();
            int weight = ReadInt();
            string nationality = Console.ReadLine().ToUpper();
            Console.WriteLine($"Parametrs of search : {height}, {weight}, {nationality}");

            //var filteredCriminals = criminals.Where(crimilal => crimilal.Height == height).
            //    Where(crimilal => crimilal.Weight == weight).
            //    Where(crimilal => crimilal.Nationality.ToUpper() == nationality).
            //    Where(crimilal => crimilal.IsIncarcerated == false);

            var filteredCriminals = criminals.Where(crimilal => crimilal.Height == height 
            && crimilal.Weight == weight 
            && crimilal.Nationality.ToUpper() == nationality
            && crimilal.IsIncarcerated == false);

            Console.WriteLine("Список найденных преступников:");

            foreach (var criminal in filteredCriminals)
            {
                Console.WriteLine(criminal.FullName);
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
    }

    class Criminal
    {
        public Criminal(string fullName, int height, int weight, bool isIncarcerated, string nationality)
        {
            FullName = fullName;
            Height = height;
            Weight = weight;
            IsIncarcerated = isIncarcerated;
            Nationality = nationality;
        }

        public string FullName { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public bool IsIncarcerated { get; private set; }
        public string Nationality { get; private set; }
    }
}