using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleLogic
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
            
            // Determine orientation
            bool isHorizontal = placedTiles.Count > 1 && placedTiles[0].y == placedTiles[1].y;
            if (placedTiles.Count == 1)
            {
                // Check neighbors to decide orientation or if it forms both
                // Simplification: We calculate both Horizontal and Vertical words for the single tile
                totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, true);
                totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, false);
                return totalScore;
            }

            // Primary word
            totalScore += GetWordScore(board, placedTiles, placedTiles[0].x, placedTiles[0].y, isHorizontal);

            // Secondary words (cross words for each placed tile)
            foreach (var tile in placedTiles)
            {
                // Check if there is a word in the perpendicular direction
                int score = GetWordScore(board, placedTiles, tile.x, tile.y, !isHorizontal);
                // Only add if it formed a valid word of length > 1 (GetWordScore returns 0 if length < 2)
                totalScore += score;
            }

            return totalScore;
        }

        public bool ValidateMove(Board board, List<(int x, int y, Tile tile)> placedTiles)
        {
            // 1. Collision check
            foreach (var pt in placedTiles)
            {
                if (board.GetCell(pt.x, pt.y).OccupyingTile != null)
                    return false; // Cannot place where tile exists
            }

            // 2. Dictionary check (Mock logic: Form words and check validator)
             // Simulating check: gather all words formed and call _validator.IsValid()
             // For this exercise, we focus on the logic structure.
             
             // Get all formed words strings
             var words = GetAllFormedWords(board, placedTiles);
             foreach(var w in words)
             {
                 if (!_validator.IsValid(w)) return false;
             }

            return true;
        }

        private List<string> GetAllFormedWords(Board board, List<(int x, int y, Tile tile)> placedTiles)
        {
            var words = new List<string>();
            if (placedTiles == null || placedTiles.Count == 0) return words;

            // Determine orientation
            bool isHorizontal = placedTiles.Count > 1 && placedTiles[0].y == placedTiles[1].y;
            if (placedTiles.Count == 1)
            {
                // Single tile can form horizontal and/or vertical words
                string hWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, true);
                if (hWord.Length > 1) words.Add(hWord);
                
                string vWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, false);
                if (vWord.Length > 1) words.Add(vWord);
                
                return words;
            }

            // Primary word
            string pWord = GetWordAt(board, placedTiles, placedTiles[0].x, placedTiles[0].y, isHorizontal);
            if (pWord.Length > 1) words.Add(pWord);

            // Secondary words
            foreach (var tile in placedTiles)
            {
                string sWord = GetWordAt(board, placedTiles, tile.x, tile.y, !isHorizontal);
                if (sWord.Length > 1) words.Add(sWord);
            }

            return words;
        }

        private string GetWordAt(Board board, List<(int x, int y, Tile tile)> placedTiles, int startX, int startY, bool horizontal)
        {
            // Find start
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
                else
                {
                    break;
                }
            }

            string word = "";
            while (true)
            {
                Tile tile = GetTile(board, placedTiles, currX, currY);
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
            
            // Find start of the word
            int currX = startX;
            int currY = startY;

            while (true)
            {
                int prevX = horizontal ? currX - 1 : currX;
                int prevY = horizontal ? currY : currY - 1;
                
                // Check if previous cell has a tile (either on board or currently being placed)
                if (HasTile(board, placedTiles, prevX, prevY))
                {
                    if (horizontal) currX--; else currY--;
                }
                else
                {
                    break;
                }
            }

            // Scan forward to calculate score
            List<Tile> wordTiles = new List<Tile>();
            
            while (true)
            {
                Tile tile = GetTile(board, placedTiles, currX, currY);
                if (tile == null) break;

                wordTiles.Add(tile);

                // Calculate score for this tile
                int tileValue = tile.Value; // Joker usually 0
                if (tile.IsJoker) tileValue = 0; 

                // Apply modifiers ONLY if the tile is one of the newly placed ones
                bool isNew = IsNewTile(placedTiles, currX, currY);
                
                if (isNew)
                {
                    var cell = board.GetCell(currX, currY);
                    if (cell.Bonus == BonusType.DoubleLetter) tileValue *= 2;
                    if (cell.Bonus == BonusType.TripleLetter) tileValue *= 3;
                    if (cell.Bonus == BonusType.DoubleWord) wordMultiplier *= 2;
                    if (cell.Bonus == BonusType.TripleWord) wordMultiplier *= 3;
                }

                wordScore += tileValue;

                if (horizontal) currX++; else currY++;
            }

            if (wordTiles.Count < 2) return 0; // Not a word

            return wordScore * wordMultiplier;
        }

        private bool HasTile(Board board, List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            return GetTile(board, placedTiles, x, y) != null;
        }

        private Tile GetTile(Board board, List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            // Check placed tiles first
            var placed = placedTiles.FirstOrDefault(pt => pt.x == x && pt.y == y);
            if (placed.tile != null) return placed.tile;

            // Check board
            return board.GetCell(x, y)?.OccupyingTile;
        }

        private bool IsNewTile(List<(int x, int y, Tile tile)> placedTiles, int x, int y)
        {
            return placedTiles.Any(pt => pt.x == x && pt.y == y);
        }
    }
}
