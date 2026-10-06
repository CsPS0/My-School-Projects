namespace csomagokLib;

public class UtazasiProgram
{
    public string Azonosito { get; init; }
    public string Megnevezes { get; init; }
    public string Helyszin { get; init; }
    public int Ar { get; init; }

    public UtazasiProgram(string adat)
    {
        var adatok = adat.Split(';');
        Azonosito = adatok[0];
        Megnevezes = adatok[1];
        Helyszin = adatok[2];
        Ar = int.Parse(adatok[3]);
    }

    public void Belfoldi()
    {
        try
        {
            if (Helyszin == "Magyarország")
            {
                Console.WriteLine($"Belföldi program: {Megnevezes}, Ár: {Ar} Ft");
            }
        }
        catch
        {
            Console.WriteLine("Nem magyarországi");
        }
    }

    public void OutPut()
    {
        ToString();
    }
}