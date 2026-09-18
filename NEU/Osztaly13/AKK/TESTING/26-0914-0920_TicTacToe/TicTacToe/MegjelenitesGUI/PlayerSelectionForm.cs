using JatekLogika;

namespace MegjelenitesGUI
{
    /// <summary>
    /// Modális ablak, amelyen a felhasználó kiválasztja, ki legyen X és ki legyen O.
    /// </summary>
    public class PlayerSelectionForm : Form
    {
        private readonly ComboBox _xComboBox;
        private readonly ComboBox _oComboBox;
        private readonly List<PlayerCandidate> _candidates;

        public Player SelectedPlayerX { get; private set; } = null!;
        public Player SelectedPlayerO { get; private set; } = null!;

        public PlayerSelectionForm(List<PlayerCandidate> candidates)
        {
            _candidates = candidates;

            Text = "Játékosok kiválasztása";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(320, 180);

            var xLabel = new Label { Text = "X jelű játékos:", Left = 20, Top = 20, Width = 120 };
            _xComboBox = new ComboBox
            {
                Left = 150,
                Top = 17,
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var oLabel = new Label { Text = "O jelű játékos:", Left = 20, Top = 60, Width = 120 };
            _oComboBox = new ComboBox
            {
                Left = 150,
                Top = 57,
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            foreach (PlayerCandidate candidate in _candidates)
            {
                _xComboBox.Items.Add(candidate);
                _oComboBox.Items.Add(candidate);
            }

            if (_xComboBox.Items.Count > 0)
            {
                _xComboBox.SelectedIndex = 0;
            }

            if (_oComboBox.Items.Count > 1)
            {
                _oComboBox.SelectedIndex = 1;
            }

            var startButton = new Button
            {
                Text = "Kezdés",
                Left = 110,
                Top = 110,
                Width = 100,
                DialogResult = DialogResult.OK
            };
            startButton.Click += StartButton_Click;

            Controls.Add(xLabel);
            Controls.Add(_xComboBox);
            Controls.Add(oLabel);
            Controls.Add(_oComboBox);
            Controls.Add(startButton);

            AcceptButton = startButton;
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            var xCandidate = _xComboBox.SelectedItem as PlayerCandidate;
            var oCandidate = _oComboBox.SelectedItem as PlayerCandidate;

            if (xCandidate is null || oCandidate is null)
            {
                MessageBox.Show(this, "Válassz ki mindkét játékost.", "Hiányzó adat",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            if (xCandidate.Id == oCandidate.Id)
            {
                MessageBox.Show(this, "A két játékos nem lehet ugyanaz a személy.", "Érvénytelen választás",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            SelectedPlayerX = xCandidate.ToPlayer(Symbol.X);
            SelectedPlayerO = oCandidate.ToPlayer(Symbol.O);
        }
    }
}
