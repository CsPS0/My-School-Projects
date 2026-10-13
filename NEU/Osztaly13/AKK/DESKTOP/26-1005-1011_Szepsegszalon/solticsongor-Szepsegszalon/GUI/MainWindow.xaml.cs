using System.Windows;
using System.Windows.Controls;
using szalonLib;

namespace GUI;

public partial class MainWindow : Window
{
    DataStore data = new();

    public MainWindow()
    {
        InitializeComponent();
        cbSzakmak.ItemsSource = data.Szakmak.Select(x => x.Nev).ToList();
    }

    void CbSzakmak_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        tbTelefon.Text = "";

        if (cbSzakmak.SelectedItem is string selectedJob)
        {
            var job = data.Szakmak.FirstOrDefault(x => x.Nev == selectedJob);
            cbAlkalmazottak.ItemsSource = data.Alkalmazottak
                .Where(x => x.SzakmaID == job?.SzakmaID)
                .OrderBy(x => x.Nev)
                .Select(x => x.Nev)
                .ToList();
        }
        else
        {
            cbAlkalmazottak.ItemsSource = null;
        }
    }

    void CbAlkalmazottak_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbAlkalmazottak.SelectedItem is string empName)
        {
            var emp = data.Alkalmazottak.FirstOrDefault(x => x.Nev == empName);
            tbTelefon.Text = emp?.Telefon ?? "";
        }
        else
        {
            tbTelefon.Text = "";
        }
    }
}