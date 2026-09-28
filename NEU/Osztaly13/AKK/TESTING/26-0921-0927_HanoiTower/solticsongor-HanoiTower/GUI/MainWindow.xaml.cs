using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using hanoiLib;

namespace GUI;

public partial class MainWindow : Window
{
    private static readonly Color[] DiskColors =
    [
        Color.FromRgb(0xE8, 0x5D, 0x75), Color.FromRgb(0xF2, 0x9E, 0x4C), Color.FromRgb(0xF1, 0xC4, 0x53),
        Color.FromRgb(0x7B, 0xC8, 0x6C), Color.FromRgb(0x4C, 0xB5, 0xAE), Color.FromRgb(0x5B, 0x8D, 0xEF),
        Color.FromRgb(0x9B, 0x6C, 0xE0), Color.FromRgb(0xD8, 0x6C, 0xC4)
    ];

    private static readonly SolidColorBrush WoodBrush = new(Color.FromRgb(0x8A, 0x7A, 0x66));
    private static readonly SolidColorBrush PegBrush = new(Color.FromRgb(0x26, 0x26, 0x2E));
    private static readonly SolidColorBrush SelectedPegBrush = new(Color.FromRgb(0x33, 0x36, 0x48));

    private HanoiGame game = null!;
    private int? selectedPeg;
    private int moves;

    public MainWindow()
    {
        InitializeComponent();
        StartGame(3, 3);
    }

    private void NewGame_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(DiskBox.Text, out int disks) || disks < 1 || disks > 12)
        {
            StatusText.Text = "A korongok száma 1 és 12 között legyen.";
            return;
        }
        if (!int.TryParse(PegBox.Text, out int pegs) || pegs < 3 || pegs > 8)
        {
            StatusText.Text = "A rudak száma 3 és 8 között legyen.";
            return;
        }
        StartGame(disks, pegs);
    }

    private void StartGame(int disks, int pegs)
    {
        game = new HanoiGame(disks, pegs);
        selectedPeg = null;
        moves = 0;
        StatusText.Text = "Kattints egy rúdra, amelyikről mozgatni szeretnél, majd arra, amelyikre.";
        Draw();
    }

    private void Peg_Click(object sender, MouseButtonEventArgs e)
    {
        if (game.IsSolved())
        {
            return;
        }

        int peg = (int)((FrameworkElement)sender).Tag;

        if (selectedPeg is null)
        {
            if (game.Pegs[peg].Count == 0)
            {
                StatusText.Text = "Üres rúdról nem lehet mozgatni.";
                return;
            }
            selectedPeg = peg;
            StatusText.Text = $"Kiválasztva: {peg + 1}. rúd. Hova tegyem?";
        }
        else if (selectedPeg == peg)
        {
            selectedPeg = null;
            StatusText.Text = "Kijelölés visszavonva.";
        }
        else
        {
            try
            {
                game.Move(selectedPeg.Value, peg);
                moves++;
                StatusText.Text = "";
            }
            catch (InvalidOperationException ex)
            {
                StatusText.Text = ex.Message;
            }
            selectedPeg = null;
        }

        if (game.IsSolved())
        {
            string minimum = game.Pegs.Count == 3 ? $" (minimum: {(1 << game.DiskCount) - 1})" : "";
            StatusText.Text = $"Gratulálok, megoldottad {moves} lépésből{minimum}!";
        }

        Draw();
    }

    private void Draw()
    {
        MovesText.Text = $"Lépések: {moves}";
        PegsGrid.Children.Clear();

        for (int i = 0; i < game.Pegs.Count; i++)
        {
            var disks = new StackPanel { VerticalAlignment = VerticalAlignment.Bottom };
            bool first = true;
            foreach (int size in game.Pegs[i])
            {
                disks.Children.Add(CreateDisk(size, highlight: first && selectedPeg == i));
                first = false;
            }

            var area = new Grid { Margin = new Thickness(0, 0, 0, 6) };
            area.Children.Add(new Rectangle
            {
                Width = 10,
                Margin = new Thickness(0, 30, 0, 0),
                RadiusX = 5,
                RadiusY = 5,
                Fill = WoodBrush
            });
            area.Children.Add(disks);

            var basePlate = new Rectangle
            {
                Height = 10,
                RadiusX = 4,
                RadiusY = 4,
                Margin = new Thickness(8, 0, 8, 0),
                Fill = WoodBrush
            };
            var label = new TextBlock
            {
                Text = $"{i + 1}. rúd",
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 6, 0, 0),
                Foreground = Brushes.Gray
            };

            var column = new DockPanel();
            DockPanel.SetDock(label, Dock.Bottom);
            DockPanel.SetDock(basePlate, Dock.Bottom);
            column.Children.Add(label);
            column.Children.Add(basePlate);
            column.Children.Add(area);

            var peg = new Border
            {
                Child = column,
                Tag = i,
                Cursor = Cursors.Hand,
                Margin = new Thickness(6),
                Padding = new Thickness(6),
                CornerRadius = new CornerRadius(10),
                Background = selectedPeg == i ? SelectedPegBrush : PegBrush
            };
            peg.MouseLeftButtonUp += Peg_Click;
            PegsGrid.Children.Add(peg);
        }
    }

    private Grid CreateDisk(int size, bool highlight)
    {
        int n = game.DiskCount;
        var row = new Grid { Height = Math.Min(26, 260.0 / n), Margin = new Thickness(0, 1, 0, 1) };
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(n - size + 1, GridUnitType.Star) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2 * size, GridUnitType.Star) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(n - size + 1, GridUnitType.Star) });

        var disk = new Border
        {
            CornerRadius = new CornerRadius(6),
            Background = new SolidColorBrush(DiskColors[(size - 1) % DiskColors.Length]),
            BorderBrush = Brushes.White,
            BorderThickness = new Thickness(highlight ? 2 : 0),
            Child = new TextBlock
            {
                Text = size.ToString(CultureInfo.InvariantCulture),
                FontSize = 12,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            }
        };
        Grid.SetColumn(disk, 1);
        row.Children.Add(disk);
        return row;
    }
}
