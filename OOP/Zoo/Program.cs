namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        class ZooApplication
        {
            private Zoo _zoo;
        }

        class Zoo
        {
            private List<Aviary> _aviaryes;
        }

        class Aviary
        {
            private List<Animal> _animals;
        }

        class Animal
        {

        }

    }
}