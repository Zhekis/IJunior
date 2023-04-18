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
                    Console.WriteLine();
                    Console.WriteLine("Press any button to go the aviary. ");
                    Console.WriteLine(Exit + " - enter exit.");
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
                _aviaryes.Add(new Birds("Birds"));
                _aviaryes.Add(new Monkey("Monkeys"));
                _aviaryes.Add(new Exotic("Exotic"));
                _aviaryes.Add(new Bears("Bears"));
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
            protected List<Animal> _animals;

            public Aviary(string name)
            {
                Name = name;
                _animals = new List<Animal>();
            }

            public string Name { get; private set; }

            public void ShowInfo()
            {
                Console.WriteLine($"Description aviary: name {Name}, count animals {_animals.Count}.");
                ShowAnimals();
            }

            private void ShowAnimals()
            {
                int number = 1;

                foreach (var animal in _animals)
                {
                    Console.Write($"{number}. ");
                    animal.Show();
                    number++;
                }
            }
        }

        class Birds : Aviary
        {
            public Birds(string name) : base(name)
            {
                _animals.Add(new Animal("Pheasant", "Male", "Гур гур"));
                _animals.Add(new Animal("Owl", "Female", "uu uu"));
                _animals.Add(new Animal("Woodpecker", "Male", "Tk tk tk"));
            }
        }

        class Monkey : Aviary
        {
            public Monkey(string name) : base(name)
            {
                _animals.Add(new Animal("Cebus olivaceus", "Male", "A Aa"));
                _animals.Add(new Animal("Gorilla beringei", "Female", "uhhh uhhh"));
                _animals.Add(new Animal("Pan troglodytes", "Female", "Waaa wwwaaa"));
                _animals.Add(new Animal("Macaca fuscata", "Male", "chik chik"));
            }
        }

        class Exotic : Aviary
        {
            public Exotic(string name) : base(name)
            {
                _animals.Add(new Animal("Mini pig", "Male", "hryu hryu"));
                _animals.Add(new Animal("Fretka", "Female", "guk guuk"));
                _animals.Add(new Animal("Red-eared turtle", "Female", "---"));
                _animals.Add(new Animal("Chinchilla", "Male", "ii iii ii"));
            }
        }

        class Bears : Aviary
        {
            public Bears(string name) : base(name)
            {
                _animals.Add(new Animal("Brown bear", "Male", "rrrr"));
                _animals.Add(new Animal("Grizzly", "Female", "raaahhh"));
                _animals.Add(new Animal("White bear", "Male", "arrrrr"));
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
}