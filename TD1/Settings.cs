public class Settings
{
    public int Size { get; set; } = 3;

    private static readonly Settings _instance = new Settings();

    private Settings() { }

    public static Settings Instance => _instance;
}
