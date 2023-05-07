namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Patient> criminals = new List<Patient> { new Patient("RobertPupkin", 50, "") ,
                new Patient("RobertPupkin", 50, "") , new Patient("RobertPupkin", 50, ""),
            new Patient("RobertPupkin", 50, "")};
        }
    }

    class Viewing
    {
        private List<Patient> _patiens;
        public Viewing(List<Patient> patiens)
        {
            _patiens = patiens;
        }

        
    }

    class Patient
    {
        public Patient(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            this.Disease = disease;
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