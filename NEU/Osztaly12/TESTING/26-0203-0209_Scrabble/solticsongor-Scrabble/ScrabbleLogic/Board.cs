using System.Collections.Generic;

namespace ScrabbleLogic
{
    public class Board
    {
        private readonly Cell[,] _cells;
        public int Size { get; }

        public Board(int size = 15)
        {
            Size = size;
            _cells = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size)
                return null;
            return _cells[x, y];
        }

        public void PlaceTile(int x, int y, Tile tile)
        {
            var cell = GetCell(x, y);
            if (cell != null)
            {
                cell.OccupyingTile = tile;
            }
        }

        public void SetBonus(int x, int y, BonusType bonus)
        {
            var cell = GetCell(x, y);
            if (cell != null)
            {
                cell.Bonus = bonus;
            }
        }
    }
}
