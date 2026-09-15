using penzugyTanacsLib;
using System.Windows;
using System.Windows.Controls;

namespace TanacsadoGUI;

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
        _db.LoadData("ugyfel.csv", "szakterulet.csv", "tanacsado.csv", "talalkozo.csv");
        cbSzakterulet.ItemsSource = _db.SzakteruletList;
    }

    private void cbSzakterulet_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ClearDetails();

        if (cbSzakterulet.SelectedItem is Szakterulet selectedSzakterulet)
        {
            var szakteruletTanacsadói = _db.TanacsadoList
                .Where(t => t.SzakteruletID == selectedSzakterulet.SzakteruletID)
                .OrderBy(t => t.Nev)
                .ToList();

            cbTanacsado.ItemsSource = szakteruletTanacsadói;
            cbTanacsado.SelectedIndex = -1;
        }
        else
        {
            cbTanacsado.ItemsSource = null;
        }
    }

    private void cbTanacsado_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbTanacsado.SelectedItem is Tanacsado selectedTanacsado)
        {
            tbTelefon.Text = selectedTanacsado.Telefon;
            tbEmail.Text = selectedTanacsado.Email;
            tbOradij.Text = selectedTanacsado.Oradij.ToString();
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
    }
}