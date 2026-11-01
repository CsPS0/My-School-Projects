using System.Windows;
using System.Windows.Controls;

namespace solticsongor_BukkMaraton
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SzamolButton_Click(object sender, RoutedEventArgs e)
        {
            if (TavComboBox.SelectedItem is ComboBoxItem selectedItem && !string.IsNullOrEmpty(IdoTextBox.Text))
            {
                if (double.TryParse(selectedItem.Tag.ToString(), out double tavKm))
                {
                    if (TimeSpan.TryParse(IdoTextBox.Text, out TimeSpan ido))
                    {
                        double totalHours = ido.TotalHours;
                        if (totalHours > 0)
                        {
                            double kmh = tavKm / totalHours;
                            double ms = (tavKm * 1000) / ido.TotalSeconds;

                            SebessegKmhTextBlock.Text = kmh.ToString("F2");
                            SebessegMsTextBlock.Text = ms.ToString("F2");
                        }
                    }
                }
            }
        }
    }
}
