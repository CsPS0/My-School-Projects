using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace VedettGUI;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public class SzerkesztesCommand : ICommand
    {
        MainWindowViewModel model;
        bool ujAllat;
        int? id;

        public SzerkesztesCommand(MainWindowViewModel model, bool ujAllat)
        {
            this.model = model;
            this.ujAllat = ujAllat;
        }

        public int? ID
        {
            get { return id; }
            set
            {
                id = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (ujAllat)
            {
                return true;
            }
            return id != null && id > 0;
        }

        public void Execute(object? parameter)
        {
            AllatSzerkeszto szerkeszto = new AllatSzerkeszto();
            if (!ujAllat)
            {
                VedettAllat? allat = null;
                try
                {
                    allat = model._vedettApi.AnimalsByIdAsync(id!.Value).Result;
                }
                catch
                {
                    allat = null;
                }
                if (allat == null)
                {
                    MessageBox.Show("Nem letezo allat!");
                    return;
                }
                szerkeszto.ViewModel.SzerkesztettAllat = allat;
            }

            if (szerkeszto.ShowDialog() ?? false)
            {
                try
                {
                    if (ujAllat)
                    {
                        int ujId = model._vedettApi.AnimalsPOSTAsync(szerkeszto.ViewModel.SzerkesztettAllat).Result;
                        MessageBox.Show("Sikeres felvitel! Az uj ID: " + ujId);
                    }
                    else
                    {
                        model._vedettApi.AnimalsPUTAsync(id!.Value, szerkeszto.ViewModel.SzerkesztettAllat).Wait();
                        MessageBox.Show("Sikeres mentes!");
                    }
                }
                catch
                {
                    MessageBox.Show("Nem sikerult a mentes!");
                }
                model.Frissites();
            }
        }
    }

    internal VedettRestAPI _vedettApi;
    string? valasztottKategoria = null;
    VedettAllat? valasztottAllat = null;

    public MainWindowViewModel()
    {
        _vedettApi = new VedettRestAPI("http://localhost:5243", new HttpClient());
        Kategoriak = new ObservableCollection<string>();
        VedettAllatok = new ObservableCollection<VedettAllat>();
        Tulajdonsagok = new ObservableCollection<string>();
        Szerkeszt = new SzerkesztesCommand(this, false);
        HozzaAd = new SzerkesztesCommand(this, true);
        KategoriakBetoltese();
    }

    public ObservableCollection<string> Kategoriak { get; init; }
    public ObservableCollection<VedettAllat> VedettAllatok { get; init; }
    public ObservableCollection<string> Tulajdonsagok { get; init; }
    public SzerkesztesCommand Szerkeszt { get; init; }
    public SzerkesztesCommand HozzaAd { get; init; }

    public string? ValasztottKategoria
    {
        get { return valasztottKategoria; }
        set
        {
            valasztottKategoria = value;
            Notify();
            VedettAllatok.Clear();
            if (value == null)
            {
                return;
            }
            try
            {
                foreach (VedettAllat allat in _vedettApi.AnimalsByCategoryAsync(value).Result.OrderBy(x => x.Nev))
                {
                    VedettAllatok.Add(allat);
                }
            }
            catch
            {
                MessageBox.Show("Nem sikerult lekerni az allatokat!");
            }
        }
    }

    public VedettAllat? ValasztottAllat
    {
        get { return valasztottAllat; }
        set
        {
            valasztottAllat = value;
            Notify();
            Tulajdonsagok.Clear();
            if (value != null)
            {
                Tulajdonsagok.Add($"ID: {value.Id}");
                Tulajdonsagok.Add($"Név: {value.Nev}");
                Tulajdonsagok.Add($"Érték: {value.Ertek} Ft");
                Tulajdonsagok.Add($"Védett lett: {value.Ev}");
                Tulajdonsagok.Add($"Kategória: {value.Kategoria}");
            }
        }
    }

    void KategoriakBetoltese()
    {
        Kategoriak.Clear();
        try
        {
            foreach (string kategoria in _vedettApi.CategoriesAsync().Result)
            {
                Kategoriak.Add(kategoria);
            }
        }
        catch
        {
            MessageBox.Show("Nem sikerult elerni az API-t! Inditsd el elobb a solticsongor-VedettAllatok projektet.");
        }
    }

    public void Frissites()
    {
        string? regiKategoria = valasztottKategoria;
        KategoriakBetoltese();
        if (regiKategoria != null && Kategoriak.Contains(regiKategoria))
        {
            ValasztottKategoria = regiKategoria;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    void Notify([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
