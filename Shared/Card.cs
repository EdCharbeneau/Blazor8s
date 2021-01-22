namespace Blazor8s.Shared
{
    public record Card(
        CardValue Value,
        CardSuit Suit,
        bool IsFaceDown = true)
    {
        public override string ToString() => $"{Value} of {Suit}";
    }
}
