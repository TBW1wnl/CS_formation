namespace TD_Morpion;

using System.ComponentModel;

public enum GameModes
{
    [Description("Joueur contre Joueur")]
    Pvp = 0,

    [Description("Joueur contre IA Facile")]
    EasyAi = 1,

    [Description("Joueur contre IA Moyenne")]
    MediumAi = 2,

    [Description("Joueur contre IA Difficile")]
    HardAi = 3,
}
