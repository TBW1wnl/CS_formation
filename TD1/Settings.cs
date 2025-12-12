namespace TD_Morpion;

public class Settings
{
    public int Size { get; set; } = 3;
    public GameModes GameMode { get; set; } = GameModes.Pvp;

    private static readonly Settings _instance = new Settings();

    private Settings() { }

    public static Settings Instance => _instance;
}
