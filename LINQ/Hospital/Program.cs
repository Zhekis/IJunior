namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Patient> patiens = new List<Patient> { 
            new Patient("RobertPupkin", 50, "ORVI"),
            new Patient("LarisaDolina", 40, "ORVI"), 
            new Patient("BorisBritva", 50, "Pneomonia"),
            new Patient("AndreyMorj", 30, "Diabet"),
            new Patient("SamPopov", 20, "Pneomonia"),
            new Patient("GorgWashington", 70, "Insomnia"),
            new Patient("PetrSidorov", 60, "Covid"),
            new Patient("VasiliyPervii", 55, "Flu"),
            new Patient("IvanGraga", 48, "Covid"),
            new Patient("MelisaKirova", 66, "Flu")};

            Viewing viewing = new Viewing(patiens);
            viewing.Work();
        }
    }

    class Viewing
    {
        private List<Patient> _patiens;

        public Viewing(List<Patient> patiens)
        {
            _patiens = patiens;
        }

        public void Work()
        {
            const string SortName = "1";
            const string SortAge = "2";
            const string FindDisease = "3";
            const string Exit = "4";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Hospital.");
                Console.WriteLine(SortName + " - Отсортировать по ФИО.\n" + SortAge + " - Отсортировать по возрасту.");
                Console.WriteLine(FindDisease + " - поиск по болезни.\n" + Exit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SortName:
                        ShowPatiens(SortPatiensByName(_patiens));
                        break;

                    case SortAge:
                        ShowPatiens(SortPatiensByAge(_patiens));
                        break;

                    case FindDisease:
                        ShowPatiens(FindPatiensByDisease(_patiens));
                        break;

                    case Exit:
                        isWork = false;
                        break;
                }

                Console.ReadKey();
            }
        }

        private IEnumerable<Patient> SortPatiensByName(List<Patient> patiens)
        {
            var sortPatiens = patiens.OrderBy(patien => patien.FullName);
            return sortPatiens;
        }

        private IEnumerable<Patient> SortPatiensByAge(List<Patient> patiens)
        {
            var sortPatiens = patiens.OrderBy(patien => patien.Age);
            return sortPatiens;
        }

        private IEnumerable<Patient> FindPatiensByDisease(List<Patient> patiens)
        {
            string disease = Console.ReadLine().ToUpper();
            var filteredPatiens = patiens.Where(patien => patien.Disease.ToUpper() == disease);
            return filteredPatiens;
        }

        private void ShowPatiens(IEnumerable<Patient> patients)
        {
            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }
        }
    }

    class Patient
    {
        public Patient(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }

        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{FullName}, {Age}, {Disease}");
        }
    }
}