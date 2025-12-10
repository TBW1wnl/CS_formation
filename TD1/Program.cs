namespace TD_Morpion;

class Program
{
    static void Main(string[] args)
    {
        Game board = new();
        char joueurActuel = 'X';

        while (true)
        {
            board.Display();
            Console.WriteLine($"\nJoueur {joueurActuel}, choisissez une position (1-9) : ");

            if (int.TryParse(Console.ReadLine(), out int position))
            {
                if (board.PlayMove(position, joueurActuel))
                {
                    joueurActuel = (joueurActuel == 'X') ? 'O' : 'X';
                }
                else
                {
                    Console.WriteLine("Appuyez sur une touche pour réessayer...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Entrée invalide ! Appuyez sur une touche pour réessayer...");
                Console.ReadKey();
            }
        }
    }
}