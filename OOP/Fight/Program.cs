﻿namespace Gladiators
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
            Human human = new Human("Human", 1400, 40, 90);
            Robot robot = new Robot("Robot", 1500, 50, 60);
            Animal animal = new Animal("Animal", 1300, 40, 80);
            Undead undead = new Undead("Undead", 2000, 30, 60);
            Spirit spirit = new Spirit("Spirit", 1700, 30, 50);
            _fighters.Add(human);
            _fighters.Add(robot);
            _fighters.Add(animal);
            _fighters.Add(undead);
            _fighters.Add(spirit);
        }

        public void Fight()
        {
            ChoiceFighters(out Fighter fighterLeft, out Fighter fighterRight);
            Scramble(fighterLeft, fighterRight);
            ShowResult(fighterLeft, fighterRight);
        }

        private void ChoiceFighters(out Fighter fighterLeft, out Fighter fighterRight)
        {
            ShowFighters();
            Console.WriteLine();
            Console.Write("Боец слева.");
            fighterLeft = GetFighter();
            Console.WriteLine();
            ShowFighters();
            Console.WriteLine();
            Console.Write("Боец справа.");
            fighterRight = GetFighter();
            Console.WriteLine();
        }

        private void Scramble(Fighter fighterLeft, Fighter fighterRight)
        {
            Console.WriteLine($"Press button to start fight {fighterLeft.Name} vs {fighterRight.Name}!");
            Console.ReadKey();

            while (fighterLeft.Health > 0 && fighterRight.Health > 0)
            {
                fighterLeft.TakeDamage(fighterRight.Damage);
                fighterLeft.UseSkills();
                fighterRight.TakeDamage(fighterLeft.Damage);
                fighterRight.UseSkills();
                fighterLeft.ShowInfo();
                fighterRight.ShowInfo();
                Console.WriteLine();
            }
        }

        private void ShowResult(Fighter fighterLeft, Fighter fighterRight)
        {
            if (fighterLeft.Health <= 0 && fighterRight.Health <= 0)
                Console.WriteLine("Ничья");
            else if (fighterLeft.Health <= 0)
                Console.WriteLine($"{fighterLeft.Name} losed");
            else if (fighterRight.Health <= 0)
                Console.WriteLine($"{fighterRight.Name} losed");
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
                _fighters.Remove(fighter);
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
        public Fighter(string name, int health, int armor, int damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public string Name { get; protected set; }
        public float Health { get; protected set; }
        public float Armor { get; protected set; }
        public float Damage { get; protected set; }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage - Armor;
        }

        public virtual void UseSkills()
        {
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}: Здоровье: {Health}, Броня: {Armor}, Урон: {Damage}");
        }
    }

    class Human : Fighter
    {
        private bool _isDoubleAtack = true;

        public Human(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void UseSkills()
        {
            float _indexPower = 2;
            float _halfHealth = 600;

            if (Health > _halfHealth && _isDoubleAtack == true)
            {
                Damage = Damage * _indexPower;
                _isDoubleAtack = false;
            }
            else if (_isDoubleAtack == false)
            {
                Damage = Damage / _indexPower;
                _isDoubleAtack = true;
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
    }

    class Animal : Fighter
    {
        private float _severeDamage = 100;
        private float _indexPower = 3;
        private bool _isHighAtack = false;

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
    }

    class Undead : Fighter
    {
        private float _returnHealth = 0.5f;

        public Undead(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Health += (damage - Armor) * _returnHealth;
        }
    }

    class Spirit : Fighter
    {
        private float _mana = 300;
        private int _countRegeneration = 2;

        public Spirit(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }

        public override void UseSkills()
        {
            float halfHealth = 800;

            if (Health < halfHealth && _countRegeneration > 0)
            {
                Health += _mana;
                _countRegeneration--;
            }
        }
    }
}