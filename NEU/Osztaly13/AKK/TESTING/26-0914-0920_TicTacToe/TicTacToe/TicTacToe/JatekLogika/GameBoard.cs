namespace JatekLogika
{
    /// <summary>
    /// A 3x3-as amőba tábla. A tábla nem ismeri a játékosokat, kizárólag
    /// a mezők tartalmát és a győzelmi feltételt kezeli.
    /// </summary>
    public class GameBoard
    {
        public const int Size = 3;

        private readonly Symbol[,] _cells = new Symbol[Size, Size];

        /// <summary>
        /// Visszaadja egy mező tartalmát. Tábla határon kívüli koordinátára
        /// <see cref="System.ArgumentOutOfRangeException"/>-t dob.
        /// </summary>
        public Symbol GetCell(int row, int col)
        {
            ValidateCoordinates(row, col);
            return _cells[row, col];
        }

        /// <summary>
        /// Igaz, ha a megadott mező üres. Határon kívüli koordinátára hamis.
        /// </summary>
        public bool IsCellEmpty(int row, int col)
        {
            if (!IsInBounds(row, col))
            {
                return false;
            }

            return _cells[row, col] == Symbol.None;
        }

        /// <summary>
        /// Megpróbálja elhelyezni a jelet a megadott mezőn.
        /// Sikertelen, ha a koordináta érvénytelen, vagy a mező foglalt.
        /// </summary>
        public bool PlaceMark(int row, int col, Symbol symbol)
        {
            if (!IsInBounds(row, col))
            {
                return false;
            }

            if (_cells[row, col] != Symbol.None)
            {
                return false;
            }

            _cells[row, col] = symbol;
            return true;
        }

        /// <summary>
        /// Igaz, ha minden mező foglalt.
        /// </summary>
        public bool IsFull()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (_cells[row, col] == Symbol.None)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Visszaállítja a táblát üres állapotba.
        /// </summary>
        public void Reset()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    _cells[row, col] = Symbol.None;
                }
            }
        }

        /// <summary>
        /// Megkeresi, van-e nyerő sor (vízszintes, függőleges vagy átlós).
        /// Visszatérési érték a nyertes jele, vagy <see cref="Symbol.None"/>,
        /// ha nincs nyerő sor.
        /// </summary>
        public Symbol GetWinner()
        {
            for (int i = 0; i < Size; i++)
            {
                if (IsWinningLine(_cells[i, 0], _cells[i, 1], _cells[i, 2]))
                {
                    return _cells[i, 0];
                }

                if (IsWinningLine(_cells[0, i], _cells[1, i], _cells[2, i]))
                {
                    return _cells[0, i];
                }
            }

            if (IsWinningLine(_cells[0, 0], _cells[1, 1], _cells[2, 2]))
            {
                return _cells[0, 0];
            }

            if (IsWinningLine(_cells[0, 2], _cells[1, 1], _cells[2, 0]))
            {
                return _cells[0, 2];
            }

            return Symbol.None;
        }

        private static bool IsWinningLine(Symbol a, Symbol b, Symbol c)
        {
            return a != Symbol.None && a == b && b == c;
        }

        private static bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        private static void ValidateCoordinates(int row, int col)
        {
            if (!IsInBounds(row, col))
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(row), $"A koordinátának 0 és {Size - 1} között kell lennie.");
            }
        }
    }
}
