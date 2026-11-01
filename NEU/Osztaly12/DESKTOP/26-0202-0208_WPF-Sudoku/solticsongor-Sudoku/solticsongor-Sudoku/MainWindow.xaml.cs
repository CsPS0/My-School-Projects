using System.Windows;
using System.Windows.Controls;

namespace solticsongor_Sudoku
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sizeTextBox.Text, out int size) && size > 4)
            {
                sizeTextBox.Text = (size - 1).ToString();
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sizeTextBox.Text, out int size) && size < 9)
            {
                sizeTextBox.Text = (size + 1).ToString();
            }
        }

        private void PuzzleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            charCountLabel.Content = $"Hossz: {puzzleTextBox.Text.Length}";
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sizeTextBox.Text, out int size))
            {
                int expectedLength = size * size;
                int currentLength = puzzleTextBox.Text.Length;

                if (currentLength == expectedLength)
                {
                    MessageBox.Show("A feladvány megfelelő hosszúságú!");
                }
                else if (currentLength < expectedLength)
                {
                    MessageBox.Show($"A feladvány rövid: kell még {expectedLength - currentLength} számjegy!");
                }
                else
                {
                    MessageBox.Show($"A feladvány hosszú: törlendő {currentLength - expectedLength} számjegy!");
                }
            }
        }
    }
}