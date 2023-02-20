using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
            Human human = new Human("Human", 1000, 40, 100);
            Robot robot = new Robot("Robot", 1500, 50, 60);
            Animal animal = new Animal("Animal", 1300, 50, 70);
            Undead undead = new Undead("Undead", 2000, 30, 60);
            Spirit spirit = new Spirit("Spirit", 1700, 20, 50);
            _fighters.Add(human);
            _fighters.Add(robot);
            _fighters.Add(animal);
            _fighters.Add(undead);
            _fighters.Add(spirit);
        }

        public void Fight()
        {
            ShowFighters();

            Console.Write("Боец слева.");
            Fighter fighterLeft = GetFighter();
            Console.WriteLine();
            ShowFighters();
            Console.Write("Боец справа.");
            Fighter fighterRight = GetFighter();
            Console.WriteLine("Press button to start fight!");
            Console.ReadKey();

            while (fighterLeft.HealthFighter > 0 && fighterRight.HealthFighter > 0)
            {
                fighterLeft.TakeDamage(fighterRight.DamageFighter);
                fighterLeft.UseSkills();
                fighterRight.TakeDamage(fighterLeft.DamageFighter);
                fighterRight.UseSkills();
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
                _fighters.Remove(_fighters[numberToFind]);
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
            int indexAddition = 1;

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                _fighters[i].ShowInfo();
            }
        }
    }

    class Fighter
    {
        protected string Name;
        protected float Health;
        protected float Armor;
        protected float Damage;

        public float HealthFighter
        {
            get
            {
                return Health;
            }
        }

        public float DamageFighter
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

        public virtual void TakeDamage(float damage)
        {
            Health -= damage - Armor;
        }


        public virtual void UseSkills()
        {

        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, Здоровье: {Health}, Броня: {Armor}, Урон: {Damage}");
        }
    }

    class Human : Fighter
    {
        bool isDoubleAtack = true;

        public Human(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }


        public override void UseSkills()
        {
            float _indexPower = 2;
            float _halfHealth = 600;

            if (Health > _halfHealth && isDoubleAtack == true)
            {
                Damage = Damage * _indexPower;
                isDoubleAtack = false;
            }
            else if (isDoubleAtack == false)
            {
                Damage = Damage / _indexPower;
                isDoubleAtack = true;
            }
        }
    }

    class Robot : Fighter
    {
        private float _limitDamage = 0.7f;
        public Robot(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            Health -= (damage  - Armor) * _limitDamage;
        }

        public override void UseSkills()
        {
            
        }
    }

    class Animal : Fighter
    {
        private float _severeDamage = 100;
        private bool _isHighAtack = false;
        private float _indexPower = 3;

        public Animal(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if(_isHighAtack == true)
            {
                Damage = Damage / _indexPower;
                _isHighAtack = false;
            }
            
            if (damage > _severeDamage) 
            {
                Damage = Damage * _indexPower;
                _isHighAtack = true;
            }
        }

        public override void UseSkills()
        {

        }
    }

    class Undead : Fighter
    {
        public Undead(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        public override void UseSkills()
        {

        }

        // Забирает жизни у противника и прибавляет себе
    }

    class Spirit : Fighter
    {
        public Spirit(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        public override void UseSkills()
        {

        }

        // Возврат урона 
    }
}