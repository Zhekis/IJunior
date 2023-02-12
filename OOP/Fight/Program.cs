using System.Numerics;
using System.Security.Cryptography;
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
            Human human = new Human("Human", 1000, 30, 80);
            Robot robot = new Robot("Robot", 1500, 50, 50);
            _fighters.Add(human);
            _fighters.Add(robot);
        }

        public void Fight()
        {
            ShowFighters();

            Console.Write("Боец слева.");
            Fighter fighterLeft = GetFighter();
            Console.WriteLine();
            Console.Write("Боец справа.");
            Fighter fighterRight = GetFighter();
            Console.WriteLine("Press button to start fight!");
            Console.ReadKey();

            while (fighterLeft.HealthFighter > 0 && fighterRight.HealthFighter > 0)
            {
                fighterLeft.TakeDamage(fighterRight.DamageFighter);
                fighterLeft.AdditionalSkills();
                fighterRight.TakeDamage(fighterLeft.DamageFighter);
                fighterLeft.ShowInfo();
                fighterRight.ShowInfo();
            }

        }

        private Fighter GetFighter()
        {
            bool isChoiseFighter = false;

            while (isChoiseFighter == false)
            {
                if (TryGetFighter(out Fighter fighter) == true)
                {
                    Console.Write($"Выбран боец: ");
                    fighter.ShowInfo();
                    isChoiseFighter = true;
                    return fighter;
                }
            }

            return null;
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

        public int HealthFighter
        {
            get
            {
                return Health;
            }
        }

        public int DamageFighter
        {
            get
            {
                return Damage;
            }
        }

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

        public virtual void AdditionalSkills()
        {

        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}-{Health} - {Armor} - {Damage}");
        }
    }

    class Human : Fighter
    {
        public Human(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void AdditionalSkills()
        {
            Console.WriteLine("Waa");
        }
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