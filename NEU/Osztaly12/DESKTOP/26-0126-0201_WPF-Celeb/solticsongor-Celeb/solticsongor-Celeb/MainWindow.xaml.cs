using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace solticsongor_Celeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FilePath = "hires.txt";

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            cmbFoglalkozas.ItemsSource = new List<string> { "színész", "zenész", "énekes", "sportoló" };
            
            LoadNationalities();

            ResetFields();
        }

        private void LoadNationalities()
        {
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("A hires.txt fájl nem található!");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(FilePath, Encoding.UTF8);
                var nationalities = new HashSet<string>();

                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(';');
                    if (parts.Length >= 3)
                    {
                        nationalities.Add(parts[2]);
                    }
                }

                var sortedNationalities = nationalities.OrderBy(n => n).ToList();
                cmbNemzetiseg.ItemsSource = sortedNationalities;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a fájl olvasásakor: {ex.Message}");
            }
        }

        private void ResetFields()
        {
            txtNev.Text = "";
            
            cmbFoglalkozas.SelectedItem = "színész";

            if (cmbNemzetiseg.Items.Count > 0)
            {
                cmbNemzetiseg.SelectedIndex = 0;
            }

            rbFerfi.IsChecked = true;
            rbNo.IsChecked = false;

            chkVilaghiru.IsChecked = false;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNev.Text))
            {
                MessageBox.Show("Adja meg a híres ember nevét!", "Hiba!");
                return;
            }

            string nev = txtNev.Text.Trim();
            string foglalkozas = cmbFoglalkozas.SelectedItem?.ToString() ?? "";
            string nemzetiseg = cmbNemzetiseg.SelectedItem?.ToString() ?? "";
            string vilaghiru = (chkVilaghiru.IsChecked == true) ? "igen" : "nem";
            string nem = (rbFerfi.IsChecked == true) ? "férfi" : "nő";

            string newLine = $"{nev};{foglalkozas};{nemzetiseg};{vilaghiru};{nem}";

            try
            {
                File.AppendAllText(FilePath, Environment.NewLine + newLine, Encoding.UTF8);
                ResetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a mentés során: {ex.Message}");
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }
    }
}