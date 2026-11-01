using System.Windows;
using System.Windows.Controls;
using filmekLib;

namespace GUI;

public partial class MainWindow : Window
{
    DataStore data = new DataStore();

    public MainWindow()
    {
        InitializeComponent();
        cbAlkotok.ItemsSource = data.Alkotok.OrderBy(a => a.Nev).ToList();
    }

    private void cbAlkotok_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        tbAdatok.Text = "";

        if (cbAlkotok.SelectedItem is Alkotok creator)
        {
            cbFilmek.ItemsSource = data.Filmstab
                .Where(s => s.AlkotoAzon == creator.AlkotoAzon)
                .Select(s => data.Filmek.First(f => f.FilmAzon == s.FilmAzon))
                .Distinct()
                .OrderBy(f => f.Cim)
                .ToList();
        }
    }

    private void cbFilmek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbFilmek.SelectedItem is Filmek film && cbAlkotok.SelectedItem is Alkotok creator)
        {
            var stab = data.Filmstab.FirstOrDefault(s => s.FilmAzon == film.FilmAzon && s.AlkotoAzon == creator.AlkotoAzon);
            var munkakor = data.Munkakor.FirstOrDefault(m => m.MunkakorAzon == stab?.MunkakorAzon);

            tbAdatok.Text = $"Alkotó szerepe a filmben: {munkakor?.Nev}\n\n" +
                            $"Film bemutatási éve: {film.Ev} ({film.HanyEveKeszult} éve)\n" +
                            $"Műfaja: {film.Mufaj}\n" +
                            $"Film színe: {film.Szines}\n" +
                            $"Film hossza: {film.Hossz} perc";
        }
        else
        {
            tbAdatok.Text = "";
        }
    }
}
