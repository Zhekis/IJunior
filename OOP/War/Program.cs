namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            War war = new();
            war.ShowFighters();
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

        public void ShowFighters()
        {
            int indexAddition = 1;

            for (int i = 0; i < _fightersUSA.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                _fightersUSA[i].ShowInfo();
            }

            Console.WriteLine();

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
            Health -= damage - Armor;
        }

        public virtual void UseSkills()
        {
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Здоровье: {Health}, Броня: {Armor}, Урон: {Damage}");
        }
    }

    class Soldier : Fighter
    {
        private float _indexPower = 0.7f;
        private float _badHealth = 500;
        private float _minLimitDamage = 0.5f;
        private float _maxLimitDamage = 1.0f;
        private bool _isGoodHealth = true;

        public Soldier(int health, int armor, int damage) : base(health, armor, damage) { }

        public override void TakeDamage(float damage)
        {
            Health -= (damage - Armor) * GetRandomNumber(_minLimitDamage, _maxLimitDamage);

            if (Health < _badHealth && _isGoodHealth == true)
            {
                Damage = Damage * _indexPower;
                _isGoodHealth = false;
            }
        }

        private float GetRandomNumber(float minimum, float maximum)
        {
            Random random = new Random();
            double scaled = random.NextDouble() * (maximum - minimum) + minimum;
            float result = (float)scaled;
            return result;
        }
    }

    class Shooter : Fighter
    {
        public Shooter(int health, int armor, int damage) : base(health, armor, damage) { }

    }
}