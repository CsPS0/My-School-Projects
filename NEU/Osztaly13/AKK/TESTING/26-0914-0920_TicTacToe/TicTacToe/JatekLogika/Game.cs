using System;

namespace JatekLogika
{
    /// <summary>
    /// Vezényli a két játékos menetét: nyilvántartja a soron következő
    /// játékost, a táblát, és a játszma állapotát.
    /// </summary>
    public class Game
    {
        public GameBoard Board { get; }
        public Player PlayerX { get; }
        public Player PlayerO { get; }
        public Player CurrentPlayer { get; private set; }
        public GameStatus Status { get; private set; }

        public Game(Player playerX, Player playerO)
        {
            if (playerX is null)
            {
                throw new ArgumentNullException(nameof(playerX));
            }

            if (playerO is null)
            {
                throw new ArgumentNullException(nameof(playerO));
            }

            if (playerX.Symbol != Symbol.X || playerO.Symbol != Symbol.O)
            {
                throw new ArgumentException("Az egyik játékosnak X, a másiknak O jellel kell rendelkeznie.");
            }

            PlayerX = playerX;
            PlayerO = playerO;
            Board = new GameBoard();
            CurrentPlayer = PlayerX;
            Status = GameStatus.InProgress;
        }

        /// <summary>
        /// Megpróbálja az aktuális játékos lépését végrehajtani a megadott mezőn.
        /// Sikertelen, ha a játszma véget ért, vagy a mező foglalt/érvénytelen.
        /// Sikeres lépés után frissíti az állapotot, és átadja a kört.
        /// </summary>
        public bool MakeMove(int row, int col)
        {
            if (Status != GameStatus.InProgress)
            {
                return false;
            }

            if (!Board.PlaceMark(row, col, CurrentPlayer.Symbol))
            {
                return false;
            }

            UpdateStatusAfterMove();

            if (Status == GameStatus.InProgress)
            {
                CurrentPlayer = CurrentPlayer == PlayerX ? PlayerO : PlayerX;
            }

            return true;
        }

        /// <summary>
        /// Visszaadja a nyertes játékost, vagy null-t, ha nincs nyertes
        /// (döntetlen vagy még folyamatban lévő játszma esetén).
        /// </summary>
        public Player? GetWinnerPlayer()
        {
            return Status switch
            {
                GameStatus.XWins => PlayerX,
                GameStatus.OWins => PlayerO,
                _ => null
            };
        }

        /// <summary>
        /// Visszaállítja a táblát és az állapotot a kezdő helyzetbe.
        /// X kezd minden újrakezdett játszmában.
        /// </summary>
        public void Reset()
        {
            Board.Reset();
            CurrentPlayer = PlayerX;
            Status = GameStatus.InProgress;
        }

        private void UpdateStatusAfterMove()
        {
            Symbol winner = Board.GetWinner();

            if (winner == Symbol.X)
            {
                Status = GameStatus.XWins;
            }
            else if (winner == Symbol.O)
            {
                Status = GameStatus.OWins;
            }
            else if (Board.IsFull())
            {
                Status = GameStatus.Draw;
            }
        }
    }
}
