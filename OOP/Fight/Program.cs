using System.Xml.Linq;

namespace Gladiators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new();
            battle.ShowFighters();

            //Fighter fighter = new Fighter(100, 20, 40);
            //fighter.TakeDamage(100);
            //fighter.ShowInfo();
            //Console.WriteLine("e");
        }
    }

    class Battle
    {
        Human human = new Human("Human", 100, 30, 10);
        Robot robot = new Robot("Robot", 150, 40, 5);
        private List<Fighter> _fighters = new();

        public Battle()
        {
            _fighters.Add(human);
            _fighters.Add(robot);
        }

        public void ShowFighters()
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