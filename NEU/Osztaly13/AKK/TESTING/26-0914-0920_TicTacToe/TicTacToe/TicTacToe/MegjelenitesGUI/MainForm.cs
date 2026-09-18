using JatekLogika;

namespace MegjelenitesGUI
{
    public class MainForm : Form
    {
        private const string PlayersCsvPath = "players.csv";
        private const int CellSize = 100;

        private readonly Button[,] _cellButtons = new Button[GameBoard.Size, GameBoard.Size];
        private readonly Label _statusLabel;
        private readonly Button _restartButton;

        private Game _game = null!;

        public MainForm()
        {
            Text = "Amőba (TicTacToe)";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(CellSize * GameBoard.Size + 40, CellSize * GameBoard.Size + 100);

            _statusLabel = new Label
            {
                Left = 20,
                Top = 10,
                Width = ClientSize.Width - 40,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold)
            };
            Controls.Add(_statusLabel);

            CreateBoardButtons();

            _restartButton = new Button
            {
                Text = "Új játék",
                Left = 20,
                Top = CellSize * GameBoard.Size + 55,
                Width = ClientSize.Width - 40,
                Height = 30
            };
            _restartButton.Click += RestartButton_Click;
            Controls.Add(_restartButton);

            StartNewGameWithPlayerSelection();
        }

        private void CreateBoardButtons()
        {
            const int boardTop = 50;
            const int boardLeft = 20;

            for (int row = 0; row < GameBoard.Size; row++)
            {
                for (int col = 0; col < GameBoard.Size; col++)
                {
                    var button = new Button
                    {
                        Left = boardLeft + col * CellSize,
                        Top = boardTop + row * CellSize,
                        Width = CellSize,
                        Height = CellSize,
                        Font = new Font(Font.FontFamily, 24, FontStyle.Bold),
                        Tag = (row, col)
                    };
                    button.Click += CellButton_Click;

                    _cellButtons[row, col] = button;
                    Controls.Add(button);
                }
            }
        }

        private void StartNewGameWithPlayerSelection()
        {
            List<PlayerCandidate> candidates;
            try
            {
                candidates = PlayerCsvLoader.LoadCandidates(PlayersCsvPath);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(this, $"Nem található a(z) '{PlayersCsvPath}' fájl a program mellett.",
                    "Hiányzó fájl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (candidates.Count < 2)
            {
                MessageBox.Show(this, "A players.csv legalább két játékost kell tartalmazzon.",
                    "Hiányos adat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            using var selectionForm = new PlayerSelectionForm(candidates);
            if (selectionForm.ShowDialog(this) != DialogResult.OK)
            {
                Close();
                return;
            }

            _game = new Game(selectionForm.SelectedPlayerX, selectionForm.SelectedPlayerO);
            RefreshBoard();
        }

        private void CellButton_Click(object? sender, EventArgs e)
        {
            if (_game.Status != GameStatus.InProgress)
            {
                return;
            }

            var button = (Button)sender!;
            var (row, col) = ((int, int))button.Tag!;

            if (!_game.MakeMove(row, col))
            {
                return;
            }

            RefreshBoard();

            if (_game.Status != GameStatus.InProgress)
            {
                ShowGameEndMessage();
            }
        }

        private void RestartButton_Click(object? sender, EventArgs e)
        {
            _game.Reset();
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            for (int row = 0; row < GameBoard.Size; row++)
            {
                for (int col = 0; col < GameBoard.Size; col++)
                {
                    Symbol symbol = _game.Board.GetCell(row, col);
                    Button button = _cellButtons[row, col];
                    button.Text = SymbolToText(symbol);
                    button.Enabled = symbol == Symbol.None && _game.Status == GameStatus.InProgress;
                }
            }

            _statusLabel.Text = BuildStatusText();
        }

        private string BuildStatusText()
        {
            return _game.Status switch
            {
                GameStatus.InProgress => $"{_game.CurrentPlayer.Name} ({_game.CurrentPlayer.Symbol}) következik",
                GameStatus.XWins => $"{_game.PlayerX.Name} (X) nyert",
                GameStatus.OWins => $"{_game.PlayerO.Name} (O) nyert",
                GameStatus.Draw => "Döntetlen",
                _ => string.Empty
            };
        }

        private void ShowGameEndMessage()
        {
            string message = _game.Status switch
            {
                GameStatus.XWins => $"{_game.PlayerX.Name} (X) nyert!",
                GameStatus.OWins => $"{_game.PlayerO.Name} (O) nyert!",
                GameStatus.Draw => "A játszma döntetlennel zárult.",
                _ => string.Empty
            };

            MessageBox.Show(this, message, "Játék vége", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string SymbolToText(Symbol symbol) => symbol switch
        {
            Symbol.X => "X",
            Symbol.O => "O",
            _ => string.Empty
        };
    }
}
