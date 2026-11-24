using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RotaryGUI;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public class FelvitelCommand : ICommand
    {
        MainWindowViewModel model;

        public FelvitelCommand(MainWindowViewModel model)
        {
            this.model = model;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            model.Felvitel();
        }
    }

    RotaryRestAPI _rotaryApi;
    List<Versenyzo> versenyzok = new List<Versenyzo>();
    bool ferfi = true;

    public MainWindowViewModel()
    {
        _rotaryApi = new RotaryRestAPI("http://localhost:5250", new HttpClient());
        Kategoriak = new ObservableCollection<int>();
        Orszagkodok = new ObservableCollection<string>();
        Felvesz = new FelvitelCommand(this);
        Betoltes();
    }

    public ObservableCollection<int> Kategoriak { get; init; }
    public ObservableCollection<string> Orszagkodok { get; init; }
    public FelvitelCommand Felvesz { get; init; }

    public int? ValasztottKategoria { get; set; }
    public string? ValasztottOrszagkod { get; set; }
    public string Poz { get; set; } = string.Empty;
    public string Rajtsz { get; set; } = string.Empty;
    public string Szul { get; set; } = string.Empty;
    public string Telepules { get; set; } = string.Empty;
    public string Kpoz { get; set; } = string.Empty;
    public string Npoz { get; set; } = string.Empty;
    public string Ido { get; set; } = string.Empty;

    public bool Ferfi
    {
        get { return ferfi; }
        set
        {
            ferfi = value;
            Notify();
            Notify(nameof(No));
        }
    }

    public bool No
    {
        get { return !ferfi; }
        set
        {
            ferfi = !value;
            Notify();
            Notify(nameof(Ferfi));
        }
    }

    void Betoltes()
    {
        try
        {
            versenyzok = _rotaryApi.ResultsAllAsync().Result.ToList();
        }
        catch
        {
            MessageBox.Show("Nem sikerult elerni az API-t! Inditsd el elobb a solticsongor-Rotary projektet.");
            return;
        }
        Kategoriak.Clear();
        foreach (int kategoria in versenyzok.Select(x => x.Kategoria).Distinct().OrderBy(x => x))
        {
            Kategoriak.Add(kategoria);
        }
        Orszagkodok.Clear();
        foreach (string orszagkod in versenyzok.Select(x => x.Orszagkod).Distinct().OrderBy(x => x))
        {
            Orszagkodok.Add(orszagkod);
        }
    }

    static bool JoIdo(string ido)
    {
        string[] reszek = ido.Split(':');
        if (reszek.Length != 3)
        {
            return false;
        }
        if (!int.TryParse(reszek[0], out int ora) || !int.TryParse(reszek[1], out int perc) || !int.TryParse(reszek[2], out int mp))
        {
            return false;
        }
        if (reszek[1].Length != 2 || reszek[2].Length != 2)
        {
            return false;
        }
        return ora >= 0 && perc >= 0 && perc < 60 && mp >= 0 && mp < 60;
    }

    void Felvitel()
    {
        if (ValasztottKategoria == null || ValasztottOrszagkod == null)
        {
            MessageBox.Show("Valassz kategoriat es orszagkodot!");
            return;
        }
        if (!int.TryParse(Poz, out int poz) || !int.TryParse(Szul, out int szul) || !int.TryParse(Kpoz, out int kpoz) || !int.TryParse(Npoz, out int npoz))
        {
            MessageBox.Show("A pozicio, a szuletesi ev, a kategoria- es a nemenkenti helyezes csak szam lehet!");
            return;
        }
        if (Rajtsz == "" || Telepules == "")
        {
            MessageBox.Show("Add meg a rajtszamot es a telepulest!");
            return;
        }
        if (versenyzok.Any(x => x.Poz == poz))
        {
            MessageBox.Show("Ilyen pozicio mar szerepel az adatok kozott!");
            return;
        }
        if (versenyzok.Any(x => x.Rajtsz == Rajtsz))
        {
            MessageBox.Show("Ilyen rajtszam mar szerepel az adatok kozott!");
            return;
        }
        if (!JoIdo(Ido))
        {
            MessageBox.Show("Az idoeredmeny formatuma nem megfelelo! (pl. 3:24:15)");
            return;
        }

        Versenyzo uj = new Versenyzo()
        {
            Poz = poz,
            Rajtsz = Rajtsz,
            Szul = szul,
            Telepules = Telepules,
            Orszagkod = ValasztottOrszagkod,
            Kategoria = ValasztottKategoria.Value,
            Kpoz = kpoz,
            Nem = Ferfi ? "Férfi" : "Nő",
            Npoz = npoz,
            Ido = Ido
        };

        try
        {
            _rotaryApi.ResultsPOSTAsync(uj).Wait();
            MessageBox.Show("Sikeres felvitel!");
            versenyzok.Add(uj);
        }
        catch
        {
            MessageBox.Show("Nem sikerult a felvitel!");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    void Notify([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
