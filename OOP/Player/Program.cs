namespace Player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(5, 10, "Zhek", '$');
            Renderer renderer = new Renderer();

            renderer.DrawPlayer(player.PositionX, player.PositionY, player.Symbol);
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public string Name { get; private set; }
        public char Symbol { get; private set; }

        public Player(int positionX, int positionY, string name, char symbol)
        {
            PositionX = positionX;
            PositionY = positionY;
            Name = name;
            Symbol = symbol;
        }
    }

    class Renderer
    {
        public void DrawPlayer(int positionX, int positionY, char symbol)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.WriteLine(symbol);
        }
    }
}