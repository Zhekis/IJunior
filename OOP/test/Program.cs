namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            float _minLimitDamage = 0.5f;
            float _maxLimitDamage = 1.1f;
            //Random random = new Random();
            //double range = (double)_minLimitDamage - (double)_maxLimitDamage;


            //for (int i = 0; i < 10; i++)
            //{
            //    double sample = random.NextDouble();
            //    double scaled = (sample * range) + _minLimitDamage;
            //    float f = (float)scaled;
            //    Console.WriteLine(f);
            //}

            static float GetRandomNumber(float minimum, float maximum)
            {
                Random random = new Random();                
                double scaled = random.NextDouble() * (maximum - minimum) + minimum;
                float result = (float)scaled;
                return result;
            }

            Console.WriteLine(GetRandomNumber(_minLimitDamage, _maxLimitDamage));
            Console.ReadKey();

        }
    }
}