using System.Windows;
using irodaLib;

namespace ForditoIrodaGUI;

public partial class MainWindow : Window
{
    private AdatSzolgaltatas? _service;

    public MainWindow()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        string forditoPath = "fordito.csv";
        string nyelvPath = "nyelv.csv";
        string megrendelesPath = "megrendeles.csv";

        try 
        {
            _service = new AdatSzolgaltatas(forditoPath, nyelvPath, megrendelesPath);
            cbNyelvek.ItemsSource = _service.Nyelvek.OrderBy(n => n.NyelvNev).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hiba az adatok betöltésekor: {ex.Message}");
        }
    }

    private void cbNyelvek_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (_service != null && cbNyelvek.SelectedItem is Nyelv valasztottNyelv)
        {
            var forditok = _service.Forditok
                .Where(f => f.NyelvId == valasztottNyelv.Id)
                .OrderBy(f => f.Nev)
                .ToList();

            cbForditok.ItemsSource = forditok;
            cbForditok.IsEnabled = forditok.Any();
            cbForditok.SelectedIndex = -1;
            ClearFields();
        }
    }

    private void cbForditok_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (cbForditok.SelectedItem is Fordito valasztottFordito)
        {
            tbTelefon.Text = valasztottFordito.Telefon;
            tbEmail.Text = valasztottFordito.Email;
            tbForditasiDij.Text = valasztottFordito.ForditasiDij.ToString();
            tbOldalszam.Text = valasztottFordito.NapiOldalszam.ToString();
        }
        else
        {
            ClearFields();
        }
    }

    private void ClearFields()
    {
        tbTelefon.Clear();
        tbEmail.Clear();
        tbForditasiDij.Clear();
        tbOldalszam.Clear();
    }
}
