using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class TicTacToeViewModel : ViewModelBase
    {
        private readonly Pet _pet;
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty] private ObservableCollection<string> _board = new();
        [ObservableProperty] private string _statusMessage = "Your Turn (X)";
        [ObservableProperty] private bool _isGameOver = false;
        [ObservableProperty] private string _gameOverResult = "";

        private bool _isPlayersTurn = true;
        private DispatcherTimer _happinessTimer;

        public TicTacToeViewModel(Pet pet, Action<ViewModelBase> navigateTo)
        {
            _pet = pet;
            _navigateTo = navigateTo;
            
            _happinessTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _happinessTimer.Tick += (s, e) => _pet.IncreaseHappiness(1);
            _happinessTimer.Start();

            ResetGame();
        }

        private void ResetGame()
        {
            Board = new ObservableCollection<string>(Enumerable.Repeat("", 9));
            StatusMessage = "Your Turn (X)";
            IsGameOver = false;
            _isPlayersTurn = true;
        }

        [RelayCommand]
        private async Task MakeMove(string indexStr)
        {
            if (!_isPlayersTurn || IsGameOver) return;
            int index = int.Parse(indexStr);
            if (Board[index] != "") return;

            Board[index] = "X";
            if (CheckWin("X"))
            {
                EndGame("You Win! +50 Coins", 50);
                return;
            }
            if (Board.All(s => s != ""))
            {
                EndGame("Draw!", 0);
                return;
            }

            _isPlayersTurn = false;
            StatusMessage = "Bot's Turn (O)...";
            await Task.Delay(600);
            BotMove();
        }

        private void BotMove()
        {
            int bestMove = GetBestMove();
            Board[bestMove] = "O";

            if (CheckWin("O"))
            {
                EndGame("You Lose! -100 Coins", -100);
                return;
            }
            if (Board.All(s => s != ""))
            {
                EndGame("Draw!", 0);
                return;
            }

            _isPlayersTurn = true;
            StatusMessage = "Your Turn (X)";
        }

        private int GetBestMove()
        {
            int bestScore = int.MinValue;
            int move = -1;

            for (int i = 0; i < 9; i++)
            {
                if (Board[i] == "")
                {
                    Board[i] = "O";
                    int score = Minimax(Board, 0, false);
                    Board[i] = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }
            return move;
        }

        private int Minimax(ObservableCollection<string> board, int depth, bool isMaximizing)
        {
            if (CheckWinInternal(board, "O")) return 10;
            if (CheckWinInternal(board, "X")) return -10;
            if (board.All(s => s != "")) return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == "")
                    {
                        board[i] = "O";
                        int score = Minimax(board, depth + 1, false);
                        board[i] = "";
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == "")
                    {
                        board[i] = "X";
                        int score = Minimax(board, depth + 1, true);
                        board[i] = "";
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        private bool CheckWin(string player) => CheckWinInternal(Board, player);

        private bool CheckWinInternal(ObservableCollection<string> board, string player)
        {
            int[,] wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (int i = 0; i < 8; i++)
            {
                if (board[wins[i, 0]] == player && board[wins[i, 1]] == player && board[wins[i, 2]] == player)
                    return true;
            }
            return false;
        }

        private void EndGame(string message, int coinDelta)
        {
            IsGameOver = true;
            GameOverResult = message;
            _pet.Money = Math.Max(0, _pet.Money + coinDelta);
        }

        [RelayCommand]
        private void Restart() => ResetGame();

        [RelayCommand]
        private void Back()
        {
            _happinessTimer.Stop();
            _navigateTo(new GamesMenuViewModel(_pet, _navigateTo));
        }
    }
}

