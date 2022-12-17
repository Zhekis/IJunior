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

        public int Length
        {
            get
            {
                return _cards.Count;
            }
        }

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

        public Card Give()
        {
            return _cards.Pop();
        }

        private void Shuffle(List<Card> cards)
        {
            Random random = new Random();

            for (int i = cards.Count - 1; i >= 1; i--)
            {
                int swappingElementIndex = random.Next(i + 1);
                Card temp = cards[swappingElementIndex];
                cards[swappingElementIndex] = cards[i];
                cards[i] = temp;
            }
        }
    }
    class Card
    {
        public string Suit { get; private set; }

        public string Rank { get; private set; }

        public Card(string rank, string suit)
        {
            Suit = suit;
            Rank = rank;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Rank}{Suit}");
        }
    }

    class Player
    {
        public void Play(Deck deck)
        {
            Console.WriteLine("Введите количество:");
            string userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int cardTakesNumber))
            {
                int initialDeckLength = deck.Length;

                for (int i = 1; i <= cardTakesNumber && i <= initialDeckLength; i++)
                {
                    Card takenCard = deck.Give();
                    takenCard.ShowInfo();
                }
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
                Console.WriteLine();
                Console.WriteLine(TakeCards + " - Взять карты.\n" + Exit + " - Завершить.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case TakeCards:
                        Play(deck);
                        break;

                    case Exit:
                        isPlaying = false;
                        break;
                }
            }
        }
    }
}