using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace solticsongor_Towers_GUI;

public partial class MainWindow : Window
{
    private int N;
    private TextBox[,] boardCells;
    private TextBox[] topVisible;
    private TextBox[] bottomVisible;
    private TextBox[] leftVisible;
    private TextBox[] rightVisible;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnPrepare_Click(object sender, RoutedEventArgs e)
    {
        if (cbSize.SelectedItem == null) return;
        N = (int)cbSize.SelectedItem;

        GameGrid.Children.Clear();
        GameGrid.RowDefinitions.Clear();
        GameGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < N + 2; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
        }

        boardCells = new TextBox[N, N];
        topVisible = new TextBox[N];
        bottomVisible = new TextBox[N];
        leftVisible = new TextBox[N];
        rightVisible = new TextBox[N];

        for (int i = 0; i < N + 2; i++)
        {
            for (int j = 0; j < N + 2; j++)
            {
                if ((i == 0 || i == N + 1) && (j == 0 || j == N + 1))
                    continue;

                TextBox tb = new TextBox
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(2),
                    FontSize = 16
                };

                if (i == 0)
                {
                    tb.IsReadOnly = true;
                    tb.Background = Brushes.LightGray;
                    topVisible[j - 1] = tb;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GameGrid.Children.Add(tb);
                }
                else if (i == N + 1)
                {
                    tb.IsReadOnly = true;
                    tb.Background = Brushes.LightGray;
                    bottomVisible[j - 1] = tb;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GameGrid.Children.Add(tb);
                }
                else if (j == 0)
                {
                    tb.IsReadOnly = true;
                    tb.Background = Brushes.LightGray;
                    leftVisible[i - 1] = tb;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GameGrid.Children.Add(tb);
                }
                else if (j == N + 1)
                {
                    tb.IsReadOnly = true;
                    tb.Background = Brushes.LightGray;
                    rightVisible[i - 1] = tb;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GameGrid.Children.Add(tb);
                }
                else
                {
                    int r = i - 1;
                    int c = j - 1;
                    boardCells[r, c] = tb;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GameGrid.Children.Add(tb);
                }
            }
        }

        btnCheck.IsEnabled = true;
        btnCalc.IsEnabled = false;
    }

    private void BtnCheck_Click(object sender, RoutedEventArgs e)
    {
        int[,] gridValues = new int[N, N];
        for (int r = 0; r < N; r++)
        {
            for (int c = 0; c < N; c++)
            {
                string text = boardCells[r, c].Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("A játékterület minden mezőjét ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(text, out gridValues[r, c]))
                {
                    MessageBox.Show("A kitöltés nem felel meg a szabályoknak!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        Megoldas m = new Megoldas(gridValues);
        if (!m.Ellenorzes())
        {
            MessageBox.Show("A kitöltés nem felel meg a szabályoknak!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        btnCalc.IsEnabled = true;
    }

    private void BtnCalc_Click(object sender, RoutedEventArgs e)
    {
        int[,] gridValues = new int[N, N];
        for (int r = 0; r < N; r++)
        {
            for (int c = 0; c < N; c++)
            {
                int.TryParse(boardCells[r, c].Text, out gridValues[r, c]);
            }
        }

        Megoldas m = new Megoldas(gridValues);
        int[] f = m.Felso();
        int[] a = m.Also();
        int[] b = m.Bal();
        int[] j = m.Jobb();

        for (int i = 0; i < N; i++)
        {
            topVisible[i].Text = f[i].ToString();
            bottomVisible[i].Text = a[i].ToString();
            leftVisible[i].Text = b[i].ToString();
            rightVisible[i].Text = j[i].ToString();
        }
    }
}
