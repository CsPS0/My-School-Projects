using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace solticsongor_KeresztrejtvenyGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            for (int i = 6; i <= 15; i++)
            {
                cbSorok.Items.Add(i);
                cbOszlopok.Items.Add(i);
            }
            cbSorok.SelectedItem = 15;
            cbOszlopok.SelectedItem = 15;

            for (int i = 1; i <= 10; i++)
            {
                cbIndex.Items.Add(i);
            }
            cbIndex.SelectedItem = 1;
        }

        private void btnLetrehoz_Click(object sender, RoutedEventArgs e)
        {
            int sorok = (int)cbSorok.SelectedItem;
            int oszlopok = (int)cbOszlopok.SelectedItem;

            ugRacs.Children.Clear();
            ugRacs.Rows = sorok;
            ugRacs.Columns = oszlopok;

            for (int i = 0; i < sorok * oszlopok; i++)
            {
                TextBox tb = new TextBox
                {
                    Text = "-",
                    MaxLength = 1,
                    Width = 30,
                    Height = 30,
                    TextAlignment = TextAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                tb.MouseDoubleClick += TextBox_MouseDoubleClick;
                ugRacs.Children.Add(tb);
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text == "-")
                {
                    tb.Text = "#";
                }
                else if (tb.Text == "#")
                {
                    tb.Text = "-";
                }
            }
        }

        private void btnMent_Click(object sender, RoutedEventArgs e)
        {
            if (ugRacs.Children.Count == 0) return;

            int index = (int)cbIndex.SelectedItem;
            int sorok = ugRacs.Rows;
            int oszlopok = ugRacs.Columns;

            string[] adatok = new string[sorok];
            for (int i = 0; i < sorok; i++)
            {
                string sor = "";
                for (int j = 0; j < oszlopok; j++)
                {
                    TextBox tb = (TextBox)ugRacs.Children[i * oszlopok + j];
                    sor += tb.Text;
                }
                adatok[i] = sor;
            }

            try
            {
                File.WriteAllLines($"kr{index}.txt", adatok);
                MessageBox.Show("A keresztrejtveny mentése sikeres!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a mentés során: {ex.Message}");
            }
        }
    }
}
