using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using LovaglasLibrary;

namespace LovaglasGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CultureInfo culture = new("hu-HU");

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (DataStore.Instance is null)
                {
                    string path = "Forrasok";
                    if (!Directory.Exists(path) && Directory.Exists("../../../Forrasok"))
                    {
                        path = "../../../Forrasok";
                    }
                    DataStore.InitCSV(path);
                }

                kepzesList.ItemsSource = DataStore.Instance?.Kepzes
                    .Select(k => k.Kepzes_Tipus)
                    .OrderBy(x => x)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az adatok betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KepzesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adatokBox.Items.Clear();
            oktatoList.ItemsSource = null;

            if (kepzesList.SelectedItem is not string selectedKepzesNev)
            {
                oktatoList.IsEnabled = false;
                return;
            }

            var kepzes = DataStore.Instance?.Kepzes.FirstOrDefault(k => k.Kepzes_Tipus == selectedKepzesNev);
            if (kepzes is null)
            {
                oktatoList.IsEnabled = false;
                return;
            }

            var oktatoIds = DataStore.Instance!.Orarend
                .Where(o => o.Kepzes_ID == kepzes.Kepzes_ID)
                .Select(o => o.Oktato_ID)
                .Distinct()
                .ToHashSet();

            var oktatok = DataStore.Instance.Oktatok
                .Where(o => oktatoIds.Contains(o.Oktato_ID))
                .Select(o => o.Nev)
                .OrderBy(name => name)
                .ToList();

            if (oktatok.Count > 0)
            {
                oktatoList.IsEnabled = true;
                oktatoList.ItemsSource = oktatok;
            }
            else
            {
                oktatoList.IsEnabled = false;
            }
        }

        private void OktatoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adatokBox.Items.Clear();
            if (oktatoList.SelectedItem is not string selectedOktatoNev)
                return;

            var oktato = DataStore.Instance?.Oktatok.FirstOrDefault(o => o.Nev == selectedOktatoNev);
            if (oktato is null)
                return;

            adatokBox.Items.Add($"Név: {oktato.Nev}");
            adatokBox.Items.Add($"Email cím: {oktato.Email}");
            adatokBox.Items.Add($"Telefonszám: {oktato.Telefon}");
            adatokBox.Items.Add($"Óradíj: {oktato.Oradij.ToString("N0", culture)} Ft");
        }
    }
}
