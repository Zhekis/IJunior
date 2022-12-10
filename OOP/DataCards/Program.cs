using System.Numerics;

namespace DataCards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Player player = new Player();
            player.Work(deck);
        }
    }

    class Deck
    {
        private Stack<Card> _cards = new Stack<Card>();

        public Deck()
        {
            List<string> suits = new() { "♥", "♠", "♦", "♣" };
            List<string> ranks = new() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            List<Card> deck = new List<Card>();

            for (int i = 0; i < ranks.Count; i++)
            {
                for (int j = 0; j < suits.Count; j++)
                {
                    deck.Add(new Card(ranks[i], suits[j]));
                }
            }

            Shuffle(deck);

            for (int i = 0; i < deck.Count; i++)
            {
                _cards.Push(deck[i]);
            }

        }

        private void Shuffle(List<Card> cards)
        {
            Random random = new Random();

            for (int i = cards.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cards[j];
                cards[j] = cards[i];
                cards[i] = temp;
            }
        }

        public int LengthDeck
        {
            get 
            { 
                return _cards.Count; 
            }
        }

        public Card GiveCard()
        {
            return _cards.Pop();
        }
    }
    class Card
    {
            private string _suit;
            private string _rank;

            public Card(string rank, string suit)
            {
                _suit = suit;
                _rank = rank;
            }

            public string Suit
            { 
                get 
                { 
                    return _suit;
                } 
            }
            public string Rank
            {
                get
                {
                    return _rank;
                }
            }
    }

    class Player
    {
        public Player(int cardTakesNumber, Deck deck)
        {
            int initialDeckLength = deck.LengthDeck;

            for (int i = 1; i <= cardTakesNumber && i <= initialDeckLength; i++)
            {
                Console.WriteLine($"{i} " + TakeCardFrom(deck));
            }
        }

        public Player()
        {

        }

        private string TakeCardFrom(Deck deck)
        {
            Card takenCard = deck.GiveCard();

            return $"{takenCard.Rank}{takenCard.Suit}";
        }

        public void Game(Deck deck)
        {
            Console.WriteLine("Введите количество:");
            string userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int number))
            {
                Player player = new Player(number, deck);
            }
            else
            {
                Console.WriteLine("Введите число.");
            }
            Console.ReadKey();
        }

        public void Work(Deck deck)
        {
            const string TakeCards = "1";
            const string Exit = "6";

            bool isPlaying = true;
            string userInput;

            while (isPlaying)
            {
                Console.WriteLine(TakeCards + " - Взять карты.\n" + Exit + " - Завершить.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case TakeCards:
                        Game(deck);
                        break;

                    case Exit:
                        isPlaying = false;
                        break;
                }
            }
        }
    }
}