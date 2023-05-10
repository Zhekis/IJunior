using System.Numerics;

namespace Define
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Stew> stews = new List<Stew> {
            new Stew("RED", 2000, 5),
            new Stew("Dolina", 2001, 6),
            new Stew("Sweet", 2000, 6),
            new Stew("BEST", 2002, 5),
            new Stew("AAAMMM", 2003, 5)};
            int currentYear = 2006;

            var filteredStews = stews.Where(stew => (stew.ExpirationYear) < currentYear);
        }
    }

    class Stew
    {
        public Stew(string name, int prodactionYear, int expiration)
        {
            Name = name;
            ProdactionYear = prodactionYear;
            Expiration = expiration;
        }

        public string Name { get; private set; }
        public int ProdactionYear { get; private set; }
        public int Expiration { get; private set; }
        public int ExpirationYear => ProdactionYear + Expiration;
    }
}