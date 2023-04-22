using System.Xml.Linq;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ObserverZoo observerZoo = new ObserverZoo();
            observerZoo.Work();
        }
    }

    class ObserverZoo
    {
        private Zoo _zoo = new Zoo();

        public void Work()
        {
            const string Exit = "EXIT";
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Zoo.");
                _zoo.ShowAviaryes();
                Console.WriteLine();
                Console.WriteLine("Press any button to go the aviary.\nExit + \" - enter exit.");
                userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case Exit:
                        isWork = false;
                        break;

                    default:
                        _zoo.ShowAviary();
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaryes;

        public Zoo()
        {
            _aviaryes = new List<Aviary>();
            _aviaryes.Add(new AviaryBirds("Birds"));
            _aviaryes.Add(new AviaryMonkey("Monkeys"));
            _aviaryes.Add(new AviaryExotic("Exotic"));
            _aviaryes.Add(new AviaryBears("Bears"));
        }

        public void ShowAviaryes()
        {
            int number = 1;

            foreach (var aviary in _aviaryes)
            {
                Console.WriteLine($"{number}. {aviary.Name}.");
                number++;
            }
        }

        public void ShowAviary()
        {
            Console.WriteLine("Input number of aviary to see information.");
            int numberAviary = ReadInt();

            if (numberAviary > 0 && numberAviary <= _aviaryes.Count)
            {
                numberAviary--;
                _aviaryes[numberAviary].ShowInfo();
            }
            else
            {
                Console.WriteLine("Number isn't correct.Try again.");
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

    class Aviary
    {
        protected List<Animal> Animals;

        public Aviary(string name)
        {
            Name = name;
            Animals = new List<Animal>();
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Description aviary: name {Name}, count animals {Animals.Count}.");
            ShowAnimals();
        }

        private void ShowAnimals()
        {
            int number = 1;

            foreach (var animal in Animals)
            {
                Console.Write($"{number}. ");
                animal.Show();
                number++;
            }
        }
    }

    class AviaryBirds : Aviary
    {
        public AviaryBirds(string name) : base(name)
        {
            Animals.Add(new Animal("Pheasant", "Male", "Гур гур"));
            Animals.Add(new Animal("Owl", "Female", "uu uu"));
            Animals.Add(new Animal("Woodpecker", "Male", "Tk tk tk"));
        }
    }

    class AviaryMonkey : Aviary
    {
        public AviaryMonkey(string name) : base(name)
        {
            Animals.Add(new Animal("Cebus olivaceus", "Male", "A Aa"));
            Animals.Add(new Animal("Gorilla beringei", "Female", "uhhh uhhh"));
            Animals.Add(new Animal("Pan troglodytes", "Female", "Waaa wwwaaa"));
            Animals.Add(new Animal("Macaca fuscata", "Male", "chik chik"));
        }
    }

    class AviaryExotic : Aviary
    {
        public AviaryExotic(string name) : base(name)
        {
            Animals.Add(new Animal("Mini pig", "Male", "hryu hryu"));
            Animals.Add(new Animal("Fretka", "Female", "guk guuk"));
            Animals.Add(new Animal("Red-eared turtle", "Female", "---"));
            Animals.Add(new Animal("Chinchilla", "Male", "ii iii ii"));
        }
    }

    class AviaryBears : Aviary
    {
        public AviaryBears(string name) : base(name)
        {
            Animals.Add(new Animal("Brown bear", "Male", "rrrr"));
            Animals.Add(new Animal("Grizzly", "Female", "raaahhh"));
            Animals.Add(new Animal("White bear", "Male", "arrrrr"));
        }
    }

    class Animal
    {
        public Animal(string name, string sex, string sound)
        {
            Name = name;
            Sex = sex;
            Sound = sound;
        }

        public string Name { get; private set; }
        public string Sex { get; private set; }
        public string Sound { get; private set; }

        public void Show()
        {
            Console.WriteLine($"{Name}, {Sex}, {Sound}.");
        }
    }
}