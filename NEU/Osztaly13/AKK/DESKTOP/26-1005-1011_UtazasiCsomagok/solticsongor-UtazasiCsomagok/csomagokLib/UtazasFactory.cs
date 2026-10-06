namespace csomagokLib;

public class UtazasFactory
{
    public string Azonosito { get; init; }
    public string Megnevezes { get; init; }
    public string Helyszin { get; init; }
    public int Ar { get; init; }

    public UtazasFactory(string adat)
    {
        var adatok = adat.Split(';');
        Azonosito = adatok[0];
        Megnevezes = adatok[1];
        Helyszin = adatok[2];
        Ar = int.Parse(adatok[3]);
    }
}