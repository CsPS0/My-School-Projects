namespace GUI;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GUI_Library;

public partial class MainWindow : Window
{
    private readonly MonsterApiHandler _apiHandler = new("https://www.dnd5eapi.co/api/2014/");
    private readonly CancellationTokenSource _cts = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1.5) };
    private readonly Random _rnd = new();

    private List<MonsterApiHandler.MonsterItem> _monsters = new();
    private int _score = 0;
    private int _lives = 3;

    public MainWindow()
    {
        InitializeComponent();
        _timer.Tick += Timer_Tick;
        Loaded += async (_, _) =>
        {
            LoadWeaponImage();
            await InitializeApiAsync();
            StartGame();
        };
    }

    private string GetPath(string relativePath)
    {
        if (File.Exists(relativePath)) return Path.GetFullPath(relativePath);
        string p1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        if (File.Exists(p1)) return p1;
        string p2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", relativePath);
        if (File.Exists(p2)) return Path.GetFullPath(p2);
        return relativePath;
    }

    private void LoadWeaponImage()
    {
        string path = GetPath("weapon.png");
        if (File.Exists(path))
        {
            WeaponImage.Source = new BitmapImage(new Uri(Path.GetFullPath(path), UriKind.Absolute));
        }
    }

    private void LoadMonsterImage(string index)
    {
        string path = GetPath(Path.Combine("monsters", $"{index}.png"));
        if (File.Exists(path))
        {
            MonsterImage.Source = new BitmapImage(new Uri(Path.GetFullPath(path), UriKind.Absolute));
        }
    }

    private void StartGame()
    {
        _score = 0;
        _lives = 3;
        UpdateLabels();
        MonsterContainer.Visibility = Visibility.Visible;
        _ = SpawnRandomMonsterAsync();
        MoveMonster();
        _timer.Start();
    }

    private void UpdateLabels()
    {
        ScoreLabel.Text = $"Pont: {_score}";
        LivesLabel.Text = $"Élet: {_lives}";
    }

    private void MoveMonster()
    {
        double x = _rnd.Next(20, Math.Max(21, (int)GameCanvas.ActualWidth - 140));
        double y = _rnd.Next(20, Math.Max(21, (int)GameCanvas.ActualHeight - 200));
        Canvas.SetLeft(MonsterContainer, x);
        Canvas.SetTop(MonsterContainer, y);
    }

    private async void Monster_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _score++;
        UpdateLabels();
        _timer.Stop();
        await SpawnRandomMonsterAsync();
        MoveMonster();
        _timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _lives--;
        UpdateLabels();

        if (_lives <= 0)
        {
            _timer.Stop();
            MonsterContainer.Visibility = Visibility.Collapsed;
            MessageBox.Show($"Játék vége! Végső pontszám: {_score}", "Game Over");
        }
        else
        {
            _ = SpawnRandomMonsterAsync();
            MoveMonster();
        }
    }

    private void GameCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        Point p = e.GetPosition(GameCanvas);
        double w = WeaponImage.ActualWidth > 0 ? WeaponImage.ActualWidth : 140;
        double h = WeaponImage.ActualHeight > 0 ? WeaponImage.ActualHeight : 140;
        Canvas.SetLeft(WeaponImage, p.X - (w / 2));
        Canvas.SetTop(WeaponImage, GameCanvas.ActualHeight - h);
    }

    private void NewGame_Click(object sender, RoutedEventArgs e) => StartGame();

    private async Task InitializeApiAsync()
    {
        var list = await _apiHandler.GetMonstersAsync("monsters", _cts.Token);
        if (list != null)
        {
            ApiCountLabel.Text = $"1. Érték: {list.count} szörny az API-ban";
            
            _monsters = list.results
                .Where(m => File.Exists(GetPath(Path.Combine("monsters", $"{m.index}.png"))))
                .ToList();

            if (_monsters.Count == 0) _monsters = list.results.ToList();

            MonsterComboBox.ItemsSource = _monsters;
        }
    }

    private async Task SpawnRandomMonsterAsync()
    {
        if (_monsters.Count == 0) return;
        var item = _monsters[_rnd.Next(_monsters.Count)];

        if (MonsterComboBox.SelectedItem is MonsterApiHandler.MonsterItem curr && curr.index == item.index)
        {
            await SetTargetMonsterAsync(item.index);
        }
        else
        {
            MonsterComboBox.SelectedItem = item;
        }
    }

    private async Task SetTargetMonsterAsync(string index)
    {
        var detail = await _apiHandler.GetMonsterDetailAsync($"monsters/{index}", _cts.Token);
        if (detail != null)
        {
            TargetInfoLabel.Text = $"{detail.name} ({detail.type}, {detail.hit_points} HP)";
            MonsterTag.Text = $"{detail.name} ({detail.hit_points} HP)";
            LoadMonsterImage(detail.index);
        }
    }

    private async void MonsterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MonsterComboBox.SelectedItem is MonsterApiHandler.MonsterItem selected)
        {
            await SetTargetMonsterAsync(selected.index);
        }
    }

    private async void NextMonster_Click(object sender, RoutedEventArgs e)
    {
        await SpawnRandomMonsterAsync();
    }
}
