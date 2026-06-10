using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class SnakePart : ObservableObject
    {
        [ObservableProperty] private int _x;
        [ObservableProperty] private int _y;

        public SnakePart(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public partial class SnakeViewModel : ViewModelBase
    {
        private readonly Pet _pet;
        private readonly Action<ViewModelBase> _navigateTo;
        private DispatcherTimer _timer;
        private readonly Random _random = new();
        
        private const int GridSize = 20;
        private const int CanvasWidth = 600;
        private const int CanvasHeight = 400;
        private const int MaxWidth = (CanvasWidth / GridSize) - 1;
        private const int MaxHeight = (CanvasHeight / GridSize) - 1;

        [ObservableProperty] private ObservableCollection<SnakePart> _snake = new();
        [ObservableProperty] private SnakePart _food = new(0, 0);
        [ObservableProperty] private int _score = 0;
        [ObservableProperty] private bool _isGameOver = false;
        [ObservableProperty] private string _gameOverMessage = "";
        
        public int HighScore => _pet.SnakeHighScore;

        private string _direction = "right";
        private double _partialFoodPoints = 0;
        private int _happinessTicks = 0;

        public SnakeViewModel(Pet pet, Action<ViewModelBase> navigateTo)
        {
            _pet = pet;
            _navigateTo = navigateTo;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += (s, e) => Move();
            
            ResetGame();
        }

        private void ResetGame()
        {
            _timer.Stop();
            Snake.Clear();
            
            
            for (int i = 0; i < 5; i++)
            {
                Snake.Add(new SnakePart(10 - i, 5));
            }
            
            SpawnFood();
            Score = 0;
            IsGameOver = false;
            _direction = "right";
            _timer.Start();
        }

        private void SpawnFood()
        {
            int x, y;
            do
            {
                x = _random.Next(1, MaxWidth);
                y = _random.Next(1, MaxHeight);
            } while (Snake.Any(p => p.X == x && p.Y == y));

            Food.X = x;
            Food.Y = y;
        }

        private void Move()
        {
            if (IsGameOver) return;

            
            int lastX = Snake.Last().X;
            int lastY = Snake.Last().Y;

            
            for (int i = Snake.Count - 1; i > 0; i--)
            {
                Snake[i].X = Snake[i - 1].X;
                Snake[i].Y = Snake[i - 1].Y;
            }

            
            switch (_direction)
            {
                case "left": Snake[0].X--; break;
                case "right": Snake[0].X++; break;
                case "down": Snake[0].Y++; break;
                case "up": Snake[0].Y--; break;
            }

            
            if (Snake[0].X < 0) Snake[0].X = MaxWidth;
            if (Snake[0].X > MaxWidth) Snake[0].X = 0;
            if (Snake[0].Y < 0) Snake[0].Y = MaxHeight;
            if (Snake[0].Y > MaxHeight) Snake[0].Y = 0;

            
            if (Snake[0].X == Food.X && Snake[0].Y == Food.Y)
            {
                Score++;
                if (Score > _pet.SnakeHighScore) 
                {
                    _pet.SnakeHighScore = Score;
                    OnPropertyChanged(nameof(HighScore));
                }

                _partialFoodPoints += 0.5;
                if (_partialFoodPoints >= 1.0)
                {
                    _pet.FoodStock++;
                    _partialFoodPoints -= 1.0;
                }

                
                Snake.Add(new SnakePart(lastX, lastY));
                SpawnFood();
            }

            
            for (int i = 1; i < Snake.Count; i++)
            {
                if (Snake[0].X == Snake[i].X && Snake[0].Y == Snake[i].Y)
                {
                    GameOver();
                    return;
                }
            }

            _happinessTicks++;
            if (_happinessTicks >= 50)
            {
                _happinessTicks = 0;
                _pet.IncreaseHappiness(1);
            }
        }

        [RelayCommand]
        public void ChangeDirection(string dir)
        {
            dir = dir.ToLower();
            if (dir == "left" && _direction != "right") _direction = "left";
            if (dir == "right" && _direction != "left") _direction = "right";
            if (dir == "up" && _direction != "down") _direction = "up";
            if (dir == "down" && _direction != "up") _direction = "down";
        }

        private void GameOver()
        {
            _timer.Stop();
            IsGameOver = true;
            GameOverMessage = $"I scored: {Score} and my Highscore is {HighScore}!";
        }

        [RelayCommand]
        private void Restart() => ResetGame();

        [RelayCommand]
        private void Back()
        {
            _timer.Stop();
            _navigateTo(new GamesMenuViewModel(_pet, _navigateTo));
        }
    }
}

