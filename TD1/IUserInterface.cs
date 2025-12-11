namespace TD_Morpion;

public interface IUserInterface
{
    void Render(in Board board);
    int AskMove(in IPlayer player);
    void ShowInvalidInput();
    void ShowInvalidMove(in string? reason);
    void ShowWin(in IPlayer player);
    void ShowDraw();
    bool AskReplay();
}

