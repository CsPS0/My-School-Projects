using System.ComponentModel;
using System.Windows;

namespace VedettGUI;

public class AllatSzerkesztoViewModel : INotifyPropertyChanged
{
    public VedettAllat SzerkesztettAllat
    {
        get
        {
            return new VedettAllat()
            {
                Id = this.ID ?? 0,
                Nev = this.Nev,
                Ertek = this.Ertek,
                Ev = this.Ev,
                Kategoria = this.Kategoria
            };
        }
        set
        {
            ID = value.Id;
            Nev = value.Nev;
            Ertek = value.Ertek;
            Ev = value.Ev;
            Kategoria = value.Kategoria;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nev)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ertek)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ev)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Kategoria)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IDVisible)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public int? ID { get; private set; }
    public string Nev { get; set; } = string.Empty;
    public string Ertek { get; set; } = string.Empty;
    public int Ev { get; set; }
    public string Kategoria { get; set; } = string.Empty;

    public Visibility IDVisible
    {
        get
        {
            if (ID == null)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
    }
}
