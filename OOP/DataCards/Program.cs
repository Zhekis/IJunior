using System.Numerics;

namespace DataCards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Croupier croupier = new Croupier();

            croupier.PlayGame(player);
        }
    }

    class Croupier
    {
        private Deck _deck;

        public Croupier()
        {
            _deck = new Deck();
        }

        public Card GetCard()
        {
            return _deck.Give();
        }

        public int GetLength()
        {
            return _deck.Length;
        }

        public void PlayGame(Player player)
        {
            const string TakeCards = "1";
            const string Exit = "2";

            bool isPlaying = true;
            string userInput;

            while (isPlaying)
            {
                Console.WriteLine(TakeCards + " - Взять карты.\n" + Exit + " - Завершить.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case TakeCards:
                        AskCards(player);
                        break;

                    case Exit:
                        isPlaying = false;
                        break;
                }
            }
        }

        public void AskCards(Player player)
        {
            Console.WriteLine("Введите количество:");
            string userInput = Console.ReadLine();
            Console.WriteLine("Ваши карты:");

            if (Int32.TryParse(userInput, out int cardTakesNumber))
            {
                int initialDeckLength = GetLength();

                for (int i = 1; i <= cardTakesNumber && i <= initialDeckLength; i++)
                {
                    Card takenCard = GetCard();
                    player.TakeCard(takenCard);
                    takenCard.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("Введите число.");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }

    class Player
    {
        private List<Card> _hand = new List<Card>();

        public void TakeCard(Card takenCard)
        {
            _hand.Add(takenCard);
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
                int swappingElementIndex = random.Next(cards.Count);
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
}