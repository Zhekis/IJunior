using System.Net.NetworkInformation;

namespace Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AquariumOperation aquariumOperation = new AquariumOperation();
            aquariumOperation.Work();
        }
    }

    class AquariumOperation
    {
        private Aquarium _aquarium;
        public AquariumOperation()
        {
            _aquarium = new Aquarium(5);
        }

        public void Work()
        {
            const string AddFishCommand = "1";
            const string RemoveFishCommand = "2";
            const string ExitCommand = "3";
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                string existence = GetInfoPresenceFishes();
                Console.WriteLine(existence);

                ShowFishes();

                Console.WriteLine();
                Console.WriteLine(AddFishCommand + " - Add fish.\n" + RemoveFishCommand + " - Remove fish.");
                Console.WriteLine(ExitCommand + " - Exit");
                Console.WriteLine("Press any button to skip time.");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddFishCommand:
                        AddFish();
                        break;

                    case RemoveFishCommand:
                        RemoveFish();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        SkipTime();
                        break;
                }

                Console.ReadKey();
            }
        }

        private void AddFish()
        {
            if (_aquarium.CountFishes == _aquarium.Capacity)
            {
                Console.WriteLine("The maximum number of fish in the aquarium.");
            }
            else
            {
                Console.WriteLine("Input name.");
                string nameFish = Console.ReadLine();
                Console.WriteLine("Input ExpentancyLife.");
                int ageExpentancy = ReadInt();
                Fish fish = new Fish(nameFish, ageExpentancy);
                _aquarium.AddFish(fish);
            }
        }

        private void RemoveFish()
        {
            if (_aquarium.CountFishes == 0)
            {
                Console.WriteLine("There are no fish in the aquarium.");
            }
            else
            {
                Console.WriteLine("Input number of fish.");
                int removeIndex = ReadInt();

                if (_aquarium.TryRemoveFish(removeIndex))
                {
                    Console.WriteLine("Fish removed successfully.");
                }
                else
                {
                    Console.WriteLine("Fish removed unsuccessfully.");
                }
            }
        }

        private void ShowFishes()
        {
            _aquarium.ShowInfo();
        }

        private void SkipTime()
        {
            _aquarium.SkipTime();
            Console.WriteLine("You skip time!");
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

        private string GetInfoPresenceFishes()
        {
            if (_aquarium.CountFishes > 0)
                return "There are fish in the aquarium.";
            else
                return "There are no fish in the aquarium.";
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium(int maxCountFishes)
        {
            Capacity = maxCountFishes;
            _fishes = new List<Fish>();
            _fishes.Add(new Fish("Dori", 3));
            _fishes.Add(new Fish("Ben", 5));
        }

        public int CountFishes => _fishes.Count;
        public int Capacity { get; private set; }

        public void AddFish(Fish fish)
        {
            _fishes.Add(fish);
        }

        public bool TryRemoveFish(int index)
        {
            if (index < 1 || index > _fishes.Count)
            {
                Console.WriteLine("Uncorrected number of fish.");
                return false;
            }

            index--;
            _fishes.RemoveAt(index);

            return true;
        }

        public void SkipTime()
        {
            foreach (var fish in _fishes)
            {
                fish.Grow();
            }
        }

        public void ShowInfo()
        {
            int numberFish = 1;

            foreach (var fish in _fishes)
            {
                Console.Write($"{numberFish}. ");
                fish.ShowInfo();
                numberFish++;
            }
        }
    }

    class Fish
    {
        public Fish(string name, int lifeExpentacyInYears)
        {
            Name = name;
            AgeInYears = 0;
            LifeExpentacyInYears = lifeExpentacyInYears;
        }

        public string Name { get; private set; }
        public int AgeInYears { get; private set; }
        public int LifeExpentacyInYears { get; private set; }
        public bool IsAlive => AgeInYears <= LifeExpentacyInYears;

        public void Grow()
        {
            AgeInYears++;
        }

        public void ShowInfo()
        {
            if (IsAlive == false)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Name: {Name}, Age: {AgeInYears}, Is alive({IsAlive})");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}