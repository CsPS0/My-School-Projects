using System.Collections.ObjectModel;
using System.Windows;

namespace solticsongor_JatekNyilvantarto
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Jatek> Jatekok { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Jatekok = new ObservableCollection<Jatek>();
            jatekListBox.ItemsSource = Jatekok;
        }

        private void btnUjJatek_Click(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Collapsed;
            AddView.Visibility = Visibility.Visible;
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (jatekListBox.SelectedItem is Jatek selectedJatek)
            {
                Jatekok.Remove(selectedJatek);
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy játékot a törléshez!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCim.Text) && !string.IsNullOrWhiteSpace(txtMufaj.Text))
            {
                Jatek ujJatek = new Jatek
                {
                    Cim = txtCim.Text,
                    Mufaj = txtMufaj.Text
                };
                Jatekok.Add(ujJatek);
                
                txtCim.Clear();
                txtMufaj.Clear();
                AddView.Visibility = Visibility.Collapsed;
                MainView.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Kérem töltse ki mindkét mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            txtCim.Clear();
            txtMufaj.Clear();
            AddView.Visibility = Visibility.Collapsed;
            MainView.Visibility = Visibility.Visible;
        }
    }
}