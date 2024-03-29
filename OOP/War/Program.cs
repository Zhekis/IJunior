﻿namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            War war = new();
            war.Fight();
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();
        public static int GenerateRandomNumber(int min, int max) { return _random.Next(min, max); }
    }

    class War
    {
        private Squad _team1;
        private Squad _team2;

        public War()
        {
            _team1 = new("USA", 10, 10);
            _team2 = new("China", 10, 10);
        }

        public void Fight()
        {
            Random random = new Random();
            _team1.ShowFighters();
            _team2.ShowFighters();
            Console.WriteLine();
            Console.WriteLine($"Press button to start fight");
            Console.ReadKey();

            while (_team1.CountFighters > 0 && _team2.CountFighters > 0)
            {
                if (_team2.CountFighters > 0)
                    _team1.TakeDamageTeam(_team2.DamageFighters);

                if (_team1.CountFighters > 0)
                    _team2.TakeDamageTeam(_team1.DamageFighters);

                Console.WriteLine($"Произошел обмен ударами, USA {_team1.CountFighters} чел., China {_team2.CountFighters}");
            }

            ShowResult(_team1, _team2);
        }

        private void ShowResult(Squad team1, Squad team2)
        {
            if (team1.CountFighters <= 0 && team2.CountFighters <= 0)
                Console.WriteLine("Ничья");
            else if (team1.CountFighters <= 0)
                Console.WriteLine($"Team {team1.Name} is losed");
            else if (team2.CountFighters <= 0)
                Console.WriteLine($"Team {team2.Name} is losed");
        }
    }

    class Squad
    {
        private List<Fighter> _fighters = new();

        public Squad(string name, int countSoldiers, int countShooters)
        {
            Name = name;
            CreateNewFighters(countSoldiers, countShooters);
        }

        public string Name { get; private set; }
        public int CountFighters => _fighters.Count;
        public List<float> DamageFighters => GetDamageFighters();

        public void TakeDamageTeam(List<float> damageFightersOpponent)
        {
            for (int i = 0; i < damageFightersOpponent.Count; i++)
            {
                int randomNumberFighter = UserUtils.GenerateRandomNumber(0, _fighters.Count);
                _fighters[randomNumberFighter].TakeDamage(damageFightersOpponent[i]);
            }

            for (int i = 0; i < _fighters.Count; i++)
            {
                if (_fighters[i].Health <= 0)
                {
                    _fighters.Remove(_fighters[i]);
                    i--;
                }
            }
        }

        public void ShowFighters()
        {
            int indexAddition = 1;
            Console.WriteLine($"{Name}:");

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                _fighters[i].ShowInfo();
            }
        }

        private List<float> GetDamageFighters()
        {
            List<float> damageFighters = new();

            for (int i = 0; i < _fighters.Count; i++)
            {
                damageFighters.Add(_fighters[i].Damage);
            }

            return damageFighters;
        }

        private void CreateNewFighters(int countSoldiers, int countShooters)
        {
            int minHealth = 800;
            int maxHealth = 1000;
            int minArmor = 30;
            int maxArmor = 50;
            int minDamage = 50;
            int maxDamage = 100;

            for (int i = 0; i < countSoldiers; i++)
            {
                _fighters.Add(new Soldier(UserUtils.GenerateRandomNumber(minHealth, maxHealth), UserUtils.GenerateRandomNumber(minArmor, maxArmor), UserUtils.GenerateRandomNumber(minDamage, maxDamage)));
            }

            for (int i = 0; i < countShooters; i++)
            {
                _fighters.Add(new Shooter(UserUtils.GenerateRandomNumber(minHealth, maxHealth), UserUtils.GenerateRandomNumber(minArmor, maxArmor), UserUtils.GenerateRandomNumber(minDamage, maxDamage)));
            }
        }
    }

    class Fighter
    {
        public Fighter(int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public float Health { get; protected set; }
        public float Armor { get; protected set; }
        public float Damage { get; protected set; }

        public virtual void TakeDamage(float damage)
        {
            if (damage >= Armor)
                Health -= damage - Armor;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Здоровье: {Health}, Броня: {Armor}, Урон: {Damage}");
        }

        public float GetRandomNumber(float minimum, float maximum)
        {
            Random random = new Random();
            double scaled = random.NextDouble() * (maximum - minimum) + minimum;
            return (float)scaled;
        }
    }

    class Soldier : Fighter
    {
        private float _indexPower = 0.7f;
        private float _indexArmor = 0.6f;
        private float _badHealth = 500;
        private float _minLimitDamage = 0.7f;
        private float _maxLimitDamage = 1.0f;
        private bool _isGoodHealth = true;

        public Soldier(int health, int armor, int damage) : base(health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            if (damage >= Armor)
                Health -= (damage - Armor) * GetRandomNumber(_minLimitDamage, _maxLimitDamage);

            if (Health < _badHealth && _isGoodHealth == true)
            {
                Armor = Armor * _indexArmor;
                Damage = Damage * _indexPower;
                _isGoodHealth = false;
            }
        }
    }

    class Shooter : Fighter
    {
        private float _badHealth = 500;
        private float _minLimitDamage = 0.3f;
        private float _maxLimitDamage = 0.8f;

        public Shooter(int health, int armor, int damage) : base(health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            if (Health > _badHealth)
            {
                base.TakeDamage(damage);
            }
            else
            {
                if (damage >= Armor)
                    Health -= (damage - Armor) * GetRandomNumber(_minLimitDamage, _maxLimitDamage);
            }
        }
    }
}