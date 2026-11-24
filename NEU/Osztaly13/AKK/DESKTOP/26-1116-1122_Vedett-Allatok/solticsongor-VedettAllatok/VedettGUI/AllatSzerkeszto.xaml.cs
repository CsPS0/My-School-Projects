using System.Windows;

namespace VedettGUI;

public partial class AllatSzerkeszto : Window
{
    public AllatSzerkeszto()
    {
        InitializeComponent();
        Owner = Application.Current.MainWindow;
    }

    public AllatSzerkesztoViewModel ViewModel
    {
        get { return (AllatSzerkesztoViewModel)DataContext; }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Nev == "" || ViewModel.Ertek == "" || ViewModel.Kategoria == "" || ViewModel.Ev <= 0)
        {
            MessageBox.Show("Tolts ki minden mezot helyesen!");
            return;
        }
        DialogResult = true;
    }
}
