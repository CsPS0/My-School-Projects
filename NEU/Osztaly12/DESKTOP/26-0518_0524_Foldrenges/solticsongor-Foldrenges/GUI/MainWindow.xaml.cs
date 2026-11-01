using System.IO;
using System.Windows;
using System.Windows.Controls;
using foldrengesLib;

namespace GUI;

public partial class MainWindow : Window
{
    private Database _db = new Database();

    public MainWindow()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        string[] searchPaths = {
            "naplo.txt",
            "../../../naplo.txt",
            "../../../../naplo.txt",
            "../../../../../naplo.txt"
        };

        string? naploPath = searchPaths.FirstOrDefault(File.Exists);
        string? telepulesPath = searchPaths.Select(p => p.Replace("naplo.txt", "telepules.txt")).FirstOrDefault(File.Exists);

        if (naploPath != null && telepulesPath != null)
        {
            _db.LoadData(naploPath, telepulesPath);

            var counties = _db.TelepulesList.Select(t => t.Varmegye).Distinct().OrderBy(c => c).ToList();
            CountyComboBox.ItemsSource = counties;
        }
        else
        {
            MessageBox.Show("Adatfájlok nem találhatók!");
        }
    }

    private void CountyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CountyComboBox.SelectedItem is string selectedCounty)
        {
            var settlements = _db.TelepulesList
                .Where(t => t.Varmegye == selectedCounty)
                .Select(t => t.Nev)
                .OrderBy(n => n)
                .ToList();

            SettlementComboBox.ItemsSource = settlements;
            SettlementComboBox.IsEnabled = settlements.Any();
            SettlementComboBox.SelectedIndex = -1;
            
            ClearFields();
        }
    }

    private void SettlementComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SettlementComboBox.SelectedItem is string selectedSettlement)
        {
            var city = _db.TelepulesList.First(t => t.Nev == selectedSettlement);
            var quake = _db.NaploList.FirstOrDefault(n => n.TelepId == city.Id);

            if (quake != null)
            {
                DateTextBox.Text = quake.Datum.ToString("yyyy-MM-dd");
                TimeTextBox.Text = quake.Ido.ToString(@"hh\:mm\:ss");
                MagnitudeTextBox.Text = quake.Magnitudo?.ToString("F1") ?? "";
                IntensityTextBox.Text = quake.Intenzitas.ToString("F1");
                RichterTextBox.Text = quake.RichterSkala;
            }
            else
            {
                ClearFields();
            }
        }
    }

    private void ClearFields()
    {
        DateTextBox.Text = "";
        TimeTextBox.Text = "";
        MagnitudeTextBox.Text = "";
        IntensityTextBox.Text = "";
        RichterTextBox.Text = "";
    }
}
