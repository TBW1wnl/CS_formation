namespace TD_Morpion;

public record Player : IPlayer
{
    public Symbol Symbol { get; }
    public string Name { get; }

    public Player(in Symbol symbol)
    {
        Symbol = symbol;
        Name = symbol.ToString();
    }
}
