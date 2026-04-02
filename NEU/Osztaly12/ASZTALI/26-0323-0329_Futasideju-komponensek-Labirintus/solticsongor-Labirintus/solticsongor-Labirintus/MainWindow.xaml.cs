using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace solticsongor_Labirintus
{
    public partial class MainWindow : Window
    {
        private Border[,]? cellBorders;
        private bool[,]? isWall;
        private int rows, cols;
        private bool isSolving = false;
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            PopulateControls();
        }

        private void PopulateControls()
        {
            for (int i = 5; i <= 30; i++)
            {
                RowsCombo.Items.Add(i);
                ColsCombo.Items.Add(i);
            }
            RowsCombo.SelectedItem = 12;
            ColsCombo.SelectedItem = 12;

            for (int i = 1; i <= 16; i++)
            {
                SaveFileIndexCombo.Items.Add(i);
            }
            SaveFileIndexCombo.SelectedItem = 3;

            DifficultyCombo.Items.Add("Könnyű");
            DifficultyCombo.Items.Add("Közepes");
            DifficultyCombo.Items.Add("Nehéz");
            DifficultyCombo.SelectedIndex = 0;
        }

        private void CreateMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSolving) return;

            rows = (int)RowsCombo.SelectedItem;
            cols = (int)ColsCombo.SelectedItem;

            string difficulty = DifficultyCombo.SelectedItem.ToString() ?? "Könnyű";
            
            int attempts = 0;
            do
            {
                GenerateBaseGrid(difficulty);
                attempts++;
            } while (difficulty == "Közepes" && !HasPath() && attempts < 100);

            UpdateVisualGrid();
            StatusText.Text = $"Labirintus létrehozva ({difficulty} nehézség).";
        }

        private void GenerateBaseGrid(string difficulty)
        {
            isWall = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (r == 0 || r == rows - 1 || c == 0 || c == cols - 1)
                        isWall[r, c] = true;
                    else
                        isWall[r, c] = false;
                }
            }

            isWall[1, 0] = false;
            isWall[rows - 2, cols - 1] = false;

            if (difficulty == "Közepes")
            {
                for (int r = 1; r < rows - 1; r++)
                {
                    for (int c = 1; c < cols - 1; c++)
                    {
                        if ((r == 1 && c == 1) || (r == rows - 2 && c == cols - 2)) continue;
                        if (rnd.NextDouble() < 0.2) isWall[r, c] = true;
                    }
                }
            }
            else if (difficulty == "Nehéz")
            {
                for (int r = 1; r < rows - 1; r++)
                    for (int c = 1; c < cols - 1; c++)
                        isWall[r, c] = true;

                GenerateMazeDFS(1, 1);
                isWall[1, 1] = false;
                isWall[rows - 2, cols - 2] = false;
            }
        }

        private void GenerateMazeDFS(int r, int c)
        {
            isWall![r, c] = false;
            int[] dr = { 0, 0, 2, -2 };
            int[] dc = { 2, -2, 0, 0 };
            
            var dirs = new List<int> { 0, 1, 2, 3 }.OrderBy(x => rnd.Next()).ToList();

            foreach (int i in dirs)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];

                if (nr > 0 && nr < rows - 1 && nc > 0 && nc < cols - 1 && isWall[nr, nc])
                {
                    isWall[r + dr[i] / 2, c + dc[i] / 2] = false;
                    GenerateMazeDFS(nr, nc);
                }
            }
        }

        private void UpdateVisualGrid()
        {
            MazeContainer.Children.Clear();
            MazeContainer.RowDefinitions.Clear();
            MazeContainer.ColumnDefinitions.Clear();
            cellBorders = new Border[rows, cols];

            for (int r = 0; r < rows; r++)
                MazeContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
            for (int c = 0; c < cols; c++)
                MazeContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Border border = new Border
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(0.5),
                        Background = isWall![r, c] ? Brushes.Black : Brushes.White,
                        Tag = new Point(r, c)
                    };

                    bool isFixed = false;
                    if (r == 0 || r == rows - 1 || c == 0 || c == cols - 1) isFixed = true;
                    if (r == 1 && c == 0) { border.Background = Brushes.LightGreen; isFixed = true; }
                    if (r == rows - 2 && c == cols - 1) { border.Background = Brushes.LightPink; isFixed = true; }

                    if (!isFixed) border.MouseDown += Border_MouseDown;

                    cellBorders[r, c] = border;
                    Grid.SetRow(border, r);
                    Grid.SetColumn(border, c);
                    MazeContainer.Children.Add(border);
                }
            }
        }

        private bool HasPath()
        {
            if (isWall == null) return false;
            Queue<Point> q = new Queue<Point>();
            HashSet<Point> visited = new HashSet<Point>();
            Point start = new Point(1, 0);
            Point end = new Point(rows - 2, cols - 1);

            q.Enqueue(start);
            visited.Add(start);

            while (q.Count > 0)
            {
                Point curr = q.Dequeue();
                if (curr == end) return true;

                int[] dr = { 0, 0, 1, -1 };
                int[] dc = { 1, -1, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    int nr = (int)curr.X + dr[i];
                    int nc = (int)curr.Y + dc[i];
                    Point next = new Point(nr, nc);
                    if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && !isWall[nr, nc] && !visited.Contains(next))
                    {
                        visited.Add(next);
                        q.Enqueue(next);
                    }
                }
            }
            return false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSolving || isWall == null) return;
            if (sender is Border b)
            {
                Point p = (Point)b.Tag;
                int r = (int)p.X;
                int c = (int)p.Y;
                isWall[r, c] = !isWall[r, c];
                b.Background = isWall[r, c] ? Brushes.Black : Brushes.White;
            }
        }

        private async void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cellBorders == null || isWall == null || isSolving) return;
            isSolving = true;
            StatusText.Text = "Keresés folyamatban...";
            ResetPathColors();

            int startR = 1, startC = 0;
            int endR = rows - 2, endC = cols - 1;
            Queue<Point> queue = new Queue<Point>();
            Dictionary<Point, Point> parentMap = new Dictionary<Point, Point>();
            HashSet<Point> visited = new HashSet<Point>();

            Point start = new Point(startR, startC);
            queue.Enqueue(start);
            visited.Add(start);

            bool found = false;
            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                if ((int)current.X == endR && (int)current.Y == endC) { found = true; break; }

                if (!((int)current.X == startR && (int)current.Y == startC))
                    cellBorders[(int)current.X, (int)current.Y].Background = Brushes.Orange;
                
                await Task.Delay(5);

                int[] dr = { 0, 0, 1, -1 };
                int[] dc = { 1, -1, 0, 0 };
                for (int i = 0; i < 4; i++)
                {
                    int nr = (int)current.X + dr[i];
                    int nc = (int)current.Y + dc[i];
                    Point next = new Point(nr, nc);
                    if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && !isWall[nr, nc] && !visited.Contains(next))
                    {
                        visited.Add(next);
                        parentMap[next] = current;
                        queue.Enqueue(next);
                    }
                }
            }

            if (found)
            {
                StatusText.Text = "Útvonal megtalálva!";
                Point curr = new Point(endR, endC);
                while (parentMap.ContainsKey(curr))
                {
                    if (!((int)curr.X == endR && (int)curr.Y == endC))
                        cellBorders[(int)curr.X, (int)curr.Y].Background = Brushes.Green;
                    curr = parentMap[curr];
                    await Task.Delay(15);
                }
            }
            else StatusText.Text = "Nincs megoldás!";
            isSolving = false;
        }

        private void ResetPathColors()
        {
            if (cellBorders == null || isWall == null) return;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (isWall[r, c]) cellBorders[r, c].Background = Brushes.Black;
                    else if (r == 1 && c == 0) cellBorders[r, c].Background = Brushes.LightGreen;
                    else if (r == rows - 2 && c == cols - 1) cellBorders[r, c].Background = Brushes.LightPink;
                    else cellBorders[r, c].Background = Brushes.White;
                }
            }
        }

        private void SaveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isWall == null || isSolving) return;
            int index = (int)SaveFileIndexCombo.SelectedItem;
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++) sb.Append(isWall[r, c] ? 'X' : ' ');
                    if (r < rows - 1) sb.AppendLine();
                }
                File.WriteAllText($"Lab{index}.txt", sb.ToString());
                MessageBox.Show($"Lab{index}.txt mentése sikeres!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
