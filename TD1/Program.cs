using TD_Morpion;

ConsoleUserInterface ui = new();
Game game = new(ui);
game.Settings.Size = 3;
game.Start();
