using System.Windows;
using System.Windows.Controls;
using starTrekLib;

namespace StarTrekGUI
{
    public partial class MainWindow : Window
    {
        private readonly Tablak tablak = DataStore.Betolt();

        public MainWindow()
        {
            InitializeComponent();

            // 8. feladat
            cbSzerep.ItemsSource = tablak.SzerepekRendezve();
        }

        // 9. feladat
        private void CbSzerep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdatokElrejtese();

            if (cbSzerep.SelectedValue is int szerepId)
            {
                cbUrhajo.ItemsSource = tablak.UrhajokSzerepSzerint(szerepId).DefaultView;
                cbUrhajo.IsEnabled = true;
            }
        }

        // 10. feladat
        private void CbUrhajo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUrhajo.SelectedValue is int urhajoId)
            {
                txtOsztaly.Text = tablak.OsztalyNev(urhajoId);
                txtFaj.Text = tablak.FajNev(urhajoId);
                Lathatosag(Visibility.Visible);
            }
            else
            {
                AdatokElrejtese();
            }
        }

        private void AdatokElrejtese()
        {
            txtOsztaly.Text = "";
            txtFaj.Text = "";
            Lathatosag(Visibility.Hidden);
        }

        private void Lathatosag(Visibility lathato)
        {
            lblOsztaly.Visibility = lathato;
            txtOsztaly.Visibility = lathato;
            lblFaj.Visibility = lathato;
            txtFaj.Visibility = lathato;
        }
    }
}
