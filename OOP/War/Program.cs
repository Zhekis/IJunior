namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            War war = new();
            war.Fight();
        }
    }

    class War
    {
        private List<Fighter> _fightersUSA = new();
        private List<Fighter> _fightersChina = new();

        public War()
        {
            Random random = new Random();
            CreateNewFighters(10, 10, _fightersUSA, random);
            CreateNewFighters(10, 10, _fightersChina, random);
        }

        public void Fight()
        {
            Scramble(_fightersUSA, _fightersChina);
        }

        private void Scramble(List<Fighter> fightersUSA, List<Fighter> fightersChina)
        {
            Random random = new Random();
            ShowFighters();
            Console.WriteLine();
            Console.WriteLine($"Press button to start fight USA vs China!");
            Console.ReadKey();

            while (fightersUSA.Count > 0 && fightersChina.Count > 0)
            {
                float averageDamageUSA;
                float averageGamageChina;
                averageDamageUSA = GetAverageDamage(fightersUSA);
                averageGamageChina = GetAverageDamage(fightersChina);

                TakeDamageTeam(fightersChina, random, averageDamageUSA);
                TakeDamageTeam(fightersUSA, random, averageGamageChina);
                Console.WriteLine($"Произошел обмен ударами, USA {fightersUSA.Count} чел., China {fightersChina.Count}");
            }

            ShowResult(fightersUSA, fightersChina);
        }

        private void ShowResult(List<Fighter> fightersUSA, List<Fighter> fightersChina)
        {
            if (fightersUSA.Count <= 0 && fightersChina.Count <= 0)
                Console.WriteLine("Ничья");
            else if (fightersUSA.Count <= 0)
                Console.WriteLine($"USA is losed");
            else if (fightersChina.Count <= 0)
                Console.WriteLine($"China is losed");
        }

        private void TakeDamageTeam(List<Fighter> fighters, Random random, float averageDamage)
        {
            for (int i = 0; i < fighters.Count; i++)
            {
                int randomNumberFighter = random.Next(0, fighters.Count);

                if (fighters[randomNumberFighter].Health > 0)
                {
                    fighters[randomNumberFighter].TakeDamage(averageDamage);

                    if (fighters[randomNumberFighter].Health <= 0)
                    {
                        fighters.Remove(fighters[randomNumberFighter]);
                    }
                }
            }
        }

        private float GetAverageDamage(List<Fighter> fighters)
        {
            float totalDamage = 0;
            float averageDamage;

            for (int i = 0; i < fighters.Count; i++)
            {
                totalDamage += fighters[i].Damage;
            }

            averageDamage = totalDamage / fighters.Count;
            return averageDamage;
        }

        private void CreateNewFighters(int countSoldiers, int countShooters, List<Fighter> fighters, Random random)
        {
            int minHealth = 800;
            int maxHealth = 1000;
            int minArmor = 30;
            int maxArmor = 50;
            int minDamage = 50;
            int maxDamage = 100;

            for (int i = 0; i < countSoldiers; i++)
            {
                fighters.Add(new Soldier(random.Next(minHealth, maxHealth), random.Next(minArmor, maxArmor), random.Next(minDamage, maxDamage)));
            }

            for (int i = 0; i < countShooters; i++)
            {
                fighters.Add(new Shooter(random.Next(minHealth, maxHealth), random.Next(minArmor, maxArmor), random.Next(minDamage, maxDamage)));
            }
        }

        private void ShowFighters()
        {
            int indexAddition = 1;
            Console.WriteLine("USA");

            for (int i = 0; i < _fightersUSA.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                _fightersUSA[i].ShowInfo();
            }

            Console.WriteLine();
            Console.WriteLine("China");

            for (int i = 0; i < _fightersChina.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                _fightersChina[i].ShowInfo();
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

        public virtual void UseSkills()
        {
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Здоровье: {Health}, Броня: {Armor}, Урон: {Damage}");
        }

        public float GetRandomNumber(float minimum, float maximum)
        {
            Random random = new Random();
            double scaled = random.NextDouble() * (maximum - minimum) + minimum;
            float result = (float)scaled;
            return result;
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