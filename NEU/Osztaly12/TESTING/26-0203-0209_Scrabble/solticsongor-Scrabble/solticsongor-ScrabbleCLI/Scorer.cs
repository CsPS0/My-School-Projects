using System;
using System.Collections.Generic;
using System.Linq;

namespace solticsongor_ScrabbleCLI
{
    public class Scorer
    {
        private readonly IWordValidator _validator;

        public Scorer(IWordValidator validator)
        {
            _validator = validator;
        }

        public int CalculateScore(Board board, List<(int x, int y, Tile tile)> placedTiles)
        {
            if (placedTiles == null || placedTiles.Count == 0) return 0;

            int totalScore = 0;

            bool isHorizontal = placedTiles.Count > 1 && placedTiles[0].y == placedTiles[1].y;
            if (placedTiles.Count == 1)
            {
                totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, true);
                totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, false);
                return totalScore;
            }

            totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, isHorizontal);

            foreach (var tile in placedTiles)
            {
                int score = GetWordScore(board, placedTiles, tile.x, tile.y, !isHorizontal);
                totalScore += score;
            }

            return totalScore;
        }

        public bool ValidateMove(Board board, List<(int x, int y, Tile tile)> placedTiles)
        {
            foreach (var pt in placedTiles)
            {
                var cell = board.GetCell(pt.x, pt.y);
                if (cell != null && cell.OccupyingTile != null)
                    return false;
            }

            var words = GetAllFormedWords(board, placedTiles);
            foreach (var w in words)
            {
                if (!_validator.IsValid(w)) return false;
            }

            return true;
        }

        private List<string> GetAllFormedWords(Board board, List<(int x, int y, Tile tile)> placedTiles)
        {
            var words = new List<string>();
            if (placedTiles == null || placedTiles.Count == 0) return words;

            bool isHorizontal = placedTiles.Count > 1 && placedTiles[0].y == placedTiles[1].y;
            if (placedTiles.Count == 1)
            {
                string hWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, true);
                if (hWord.Length > 1) words.Add(hWord);

                string vWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, false);
                if (vWord.Length > 1) words.Add(vWord);

                return words;
            }

            string pWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, isHorizontal);
            if (pWord.Length > 1) words.Add(pWord);

            foreach (var tile in placedTiles)
            {
                string sWord = GetWordAt(board, placedTiles, tile.x, tile.y, !isHorizontal);
                if (sWord.Length > 1) words.Add(sWord);
            }

            return words;
        }

        private string GetWordAt(Board board, List<(int x, int y, Tile tile)> placedTiles, int startX, int startY, bool horizontal)
        {
            int currX = startX;
            int currY = startY;

            while (true)
            {
                int prevX = horizontal ? currX - 1 : currX;
                int prevY = horizontal ? currY : currY - 1;
                if (HasTile(board, placedTiles, prevX, prevY))
                {
                    if (horizontal) currX--; else currY--;
                }
                else break;
            }

            string word = "";
            while (true)
            {
                Tile? tile = GetTile(board, placedTiles, currX, currY);
                if (tile == null) break;
                word += tile.Letter;
                if (horizontal) currX++; else currY++;
            }
            return word;
        }

        private int GetWordScore(Board board, List<(int x, int y, Tile tile)> placedTiles, int startX, int startY, bool horizontal)
        {
            int wordScore = 0;
            int wordMultiplier = 1;
            int currX = startX;
            int currY = startY;

            while (true)
            {
                int prevX = horizontal ? currX - 1 : currX;
                int prevY = horizontal ? currY : currY - 1;
                if (HasTile(board, placedTiles, prevX, prevY))
                {
                    if (horizontal) currX--; else currY--;
                }
                else break;
            }

            List<Tile> wordTiles = new List<Tile>();
            while (true)
            {
                Tile? tile = GetTile(board, placedTiles, currX, currY);
                if (tile == null) break;

                wordTiles.Add(tile);
                int tileValue = tile.IsJoker ? 0 : tile.Value;

                if (IsNewTile(placedTiles, currX, currY))
                {
                    var cell = board.GetCell(currX, currY);
                    if (cell != null)
                    {
                        if (cell.Bonus == BonusType.DoubleLetter) tileValue *= 2;
                        if (cell.Bonus == BonusType.TripleLetter) tileValue *= 3;
                        if (cell.Bonus == BonusType.DoubleWord) wordMultiplier *= 2;
                        if (cell.Bonus == BonusType.TripleWord) wordMultiplier *= 3;
                    }
                }

                wordScore += tileValue;
                if (horizontal) currX++; else currY++;
            }

            if (wordTiles.Count < 2) return 0;
            return wordScore * wordMultiplier;
        }

        private bool HasTile(Board board, List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            return GetTile(board, placedTiles, x, y) != null;
        }

        private Tile? GetTile(Board board, List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            var placed = placedTiles.FirstOrDefault(pt => pt.x == x && pt.y == y);
            if (placed.tile != null) return placed.tile;
            return board.GetCell(x, y)?.OccupyingTile;
        }

        private bool IsNewTile(List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            return placedTiles.Any(pt => pt.x == x && pt.y == y);
        }
    }
}
