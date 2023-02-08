using System.Numerics;
using System.Xml.Linq;

namespace Gladiators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new();
            battle.Fight();
        }
    }

    class Battle
    {
        private List<Fighter> _fighters = new();

        public Battle()
        {
            Human human = new Human("Human", 100, 30, 10);
            Robot robot = new Robot("Robot", 150, 40, 5);
            _fighters.Add(human);
            _fighters.Add(robot);
        }

        public void Fight()
        {
            bool IsChoiсeLeftFighter = false;
            bool IsChoiсeRightFighter = false;

            ShowFighters();

            Console.Write("Боец слева.");

            while (IsChoiсeLeftFighter == false)
            {
                if (TryGetFighter(out Fighter fighter) == true)
                {
                    Fighter fighterLeft = fighter;
                    Console.Write($"Боец слева.");
                    fighter.ShowInfo();
                    IsChoiсeLeftFighter = true;
                }
            }

            Console.WriteLine();
            Console.Write("Боец справа.");

            while (IsChoiсeRightFighter == false)
            {
                if (TryGetFighter(out Fighter fighter) == true)
                {
                    Fighter fighterRight = fighter;
                    Console.Write($"Боец справа.");
                    fighter.ShowInfo();
                    IsChoiсeRightFighter = true;
                }
            }


        }

        private bool TryGetFighter(out Fighter fighter)
        {
            int numberToFind;
            Console.WriteLine("Введите номер.");
            bool isNumber = int.TryParse(Console.ReadLine(), out numberToFind);

            if (numberToFind > 0 && numberToFind <= _fighters.Count)
            {
                numberToFind--;
                fighter = _fighters[numberToFind];
                return true;
            }
            else
            {
                Console.WriteLine("Такого бойца нет.");
                fighter = null;
                return false;
            }
        }

        private void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _fighters[i].ShowInfo();
            }
        }
    }

    class Fighter
    {
        protected string Name;
        protected int Health;
        protected int Armor;
        protected int Damage;

        public Fighter(string name, int health, int armor, int damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage - Armor;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}-{Health} - {Armor} - {Damage}");
        }
    }

    class Human : Fighter
    {
        public Human(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        // Когда уровень жизней меньше 30%, наносит меньше урона противнику. И когда меньше 50%, может нанести 1 удар с двойной силой.
    }

    class Robot : Fighter
    {
        public Robot(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }
        // Дополнительная броня, пропускает только определенный урон.
    }

    //class Animal : Fighter
    //{
    //    // Рычит, увеличивается урон
    //}

    //class Undead : Fighter
    //{
    //    // Забирает жизни у противника и прибавляет себе
    //}

    //class Spirit : Fighter
    //{
    //    // Возврат урона 
    //}
}