using System.Windows;
using System.Windows.Controls;
using solticsongor_NyelviskolaLib;

namespace solticsongor_NyelviskolaGUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataStore.InitCSV();
        LoadNyelvek();
    }

    private void LoadNyelvek()
    {
        cbNyelv.ItemsSource = DataStore.Instance?.Nyelvek.OrderBy(x => x.NyelvNev);
    }

    private void CbNyelv_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbNyelv.SelectedItem is Nyelv valasztottNyelv)
        {
            var tanarok = DataStore.Instance?.Tanarok
                .Where(x => x.NyelvID == valasztottNyelv.NyelvID)
                .OrderBy(x => x.Nev)
                .ToList();

            cbTanar.ItemsSource = tanarok;
            cbTanar.IsEnabled = tanarok?.Any() == true;
            cbTanar.SelectedIndex = -1;
            ClearDetails();
        }
    }

    private void CbTanar_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbTanar.SelectedItem is Tanar valasztottTanar)
        {
            tbTelefon.Text = valasztottTanar.Telefon;
            tbEmail.Text = valasztottTanar.Email;
            tbOradij.Text = valasztottTanar.Oradij.ToString();
            cbOnline.IsChecked = valasztottTanar.Net;
        }
        else
        {
            ClearDetails();
        }
    }

    private void ClearDetails()
    {
        tbTelefon.Text = string.Empty;
        tbEmail.Text = string.Empty;
        tbOradij.Text = string.Empty;
        cbOnline.IsChecked = false;
    }
}
