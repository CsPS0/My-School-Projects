using System.IO;
using System.Windows;
using System.Windows.Controls;
using szoftverLib;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private Bekeresek? _repository;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _repository = new Bekeresek();
                CmbKategoria.ItemsSource = _repository.GetKategoriak();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Hiányzó adatfájl", MessageBoxButton.OK, MessageBoxImage.Error);
                CmbKategoria.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Váratlan hiba az adatok betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                CmbKategoria.IsEnabled = false;
            }
        }

        private void CmbKategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_repository != null && CmbKategoria.SelectedItem is string selectedCategory)
            {
                CmbSzoftver.ItemsSource = _repository.GetSzoftverNevekByKategoria(selectedCategory);
                CmbSzoftver.IsEnabled = true;
                CmbSzoftver.SelectedIndex = -1;
                
                DgEredmenyek.ItemsSource = null;
            }
        }

        private void CmbSzoftver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_repository != null && CmbSzoftver.SelectedItem is string selectedSoftwareName && CmbKategoria.SelectedItem is string selectedCategory)
            {
                DgEredmenyek.ItemsSource = _repository.GetTelepitesekBySzoftverEsKategoria(selectedSoftwareName, selectedCategory);
            }
        }
    }
}
