namespace Blazor8s.Shared
{
    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public bool IsFaceDown { get; set; } = true;

        public override string ToString() => $"{Value} of {Suit}";
    }
}
