using JatekLogika;

namespace MegjelenitesKonzol
{
    internal static class Program
    {
        private const string PlayersCsvPath = "players.csv";

        private static void Main()
        {
            Console.WriteLine("=== Amőba (TicTacToe) ===");
            Console.WriteLine();

            List<PlayerCandidate> candidates = LoadCandidatesOrExit();

            Player playerX = SelectPlayer(candidates, Symbol.X, excludedId: null);
            Player playerO = SelectPlayer(candidates, Symbol.O, excludedId: playerX.Id);

            var game = new Game(playerX, playerO);

            bool playAgain = true;
            while (playAgain)
            {
                RunGameLoop(game);
                playAgain = AskPlayAgain();
                if (playAgain)
                {
                    game.Reset();
                }
            }

            Console.WriteLine("Viszlát!");
        }

        private static List<PlayerCandidate> LoadCandidatesOrExit()
        {
            try
            {
                List<PlayerCandidate> candidates = PlayerCsvLoader.LoadCandidates(PlayersCsvPath);
                if (candidates.Count < 2)
                {
                    Console.WriteLine("A players.csv legalább két játékost kell tartalmazzon.");
                    Environment.Exit(1);
                }

                return candidates;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Nem található a(z) '{PlayersCsvPath}' fájl a program mellett.");
                Environment.Exit(1);
                return new List<PlayerCandidate>(); // sosem fut le, a fordítónak kell
            }
        }

        private static Player SelectPlayer(List<PlayerCandidate> candidates, Symbol symbol, int? excludedId)
        {
            Console.WriteLine();
            Console.WriteLine($"Válassz játékost a(z) '{symbol}' jelhez:");
            foreach (PlayerCandidate candidate in candidates)
            {
                if (candidate.Id != excludedId)
                {
                    Console.WriteLine($"  {candidate.Id}: {candidate.Name}");
                }
            }

            while (true)
            {
                Console.Write("Azonosító: ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Érvénytelen azonosító, próbáld újra.");
                    continue;
                }

                if (id == excludedId)
                {
                    Console.WriteLine("Ez a játékos már ki lett választva, válassz másikat.");
                    continue;
                }

                PlayerCandidate? found = candidates.FirstOrDefault(c => c.Id == id);
                if (found is null)
                {
                    Console.WriteLine("Nincs ilyen azonosítójú játékos, próbáld újra.");
                    continue;
                }

                return found.ToPlayer(symbol);
            }
        }

        private static void RunGameLoop(Game game)
        {
            while (game.Status == GameStatus.InProgress)
            {
                PrintBoard(game.Board);
                Console.WriteLine();
                Console.WriteLine($"{game.CurrentPlayer.Name} ({game.CurrentPlayer.Symbol}) következik.");

                (int row, int col) = ReadMove();

                if (!game.MakeMove(row, col))
                {
                    Console.WriteLine("Foglalt vagy érvénytelen mező, próbáld újra.");
                }
            }

            PrintBoard(game.Board);
            Console.WriteLine();
            PrintResult(game);
        }

        private static (int Row, int Col) ReadMove()
        {
            while (true)
            {
                Console.Write("Lépés (sor oszlop, 1-3 között, szóközzel elválasztva): ");
                string? input = Console.ReadLine();
                string[] parts = (input ?? string.Empty)
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2
                    && int.TryParse(parts[0], out int row)
                    && int.TryParse(parts[1], out int col)
                    && row is >= 1 and <= 3
                    && col is >= 1 and <= 3)
                {
                    return (row - 1, col - 1);
                }

                Console.WriteLine("Érvénytelen formátum. Például: 2 3");
            }
        }

        private static void PrintBoard(GameBoard board)
        {
            Console.WriteLine();
            for (int row = 0; row < GameBoard.Size; row++)
            {
                var cells = new string[GameBoard.Size];
                for (int col = 0; col < GameBoard.Size; col++)
                {
                    cells[col] = SymbolToChar(board.GetCell(row, col));
                }

                Console.WriteLine(" " + string.Join(" | ", cells));

                if (row < GameBoard.Size - 1)
                {
                    Console.WriteLine("---+---+---");
                }
            }
        }

        private static string SymbolToChar(Symbol symbol) => symbol switch
        {
            Symbol.X => "X",
            Symbol.O => "O",
            _ => " "
        };

        private static void PrintResult(Game game)
        {
            switch (game.Status)
            {
                case GameStatus.XWins:
                case GameStatus.OWins:
                    Player winner = game.GetWinnerPlayer()!;
                    Console.WriteLine($"{winner.Name} ({winner.Symbol}) nyert!");
                    break;
                case GameStatus.Draw:
                    Console.WriteLine("A játszma döntetlennel zárult.");
                    break;
            }
        }

        private static bool AskPlayAgain()
        {
            Console.Write("Kérsz még egy kört ugyanezzel a két játékossal? (i/n): ");
            string? input = Console.ReadLine();
            return string.Equals(input?.Trim(), "i", StringComparison.OrdinalIgnoreCase);
        }
    }
}
