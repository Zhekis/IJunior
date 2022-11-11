namespace Player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player hero = new Player(5, 10, "Zhek", '$');
            Renderer renderer = new Renderer();

            renderer.DrawPlayer(hero.PositionX, hero.PositionY, hero.SymbolPlayer);
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public string Name { get; private set; }
        public char SymbolPlayer { get; private set; }

        public Player(int positionX, int positionY, string name, char symbolPlayer)
        {
            PositionX = positionX;
            PositionY = positionY;
            Name = name;
            SymbolPlayer = symbolPlayer;
        }
    }

    class Renderer
    {
        public void DrawPlayer(int PositionX, int PositionY, char SymbolPlayer)
        {
            Console.SetCursorPosition(PositionY, PositionX);
            Console.WriteLine(SymbolPlayer);
        }
    }
}