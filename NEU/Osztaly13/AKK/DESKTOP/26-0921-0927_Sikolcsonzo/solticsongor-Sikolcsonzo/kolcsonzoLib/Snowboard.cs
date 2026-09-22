namespace kolcsonzoLib;

public sealed class Snowboard(string azonosito, string leiras, int meret, bool cipovel)
    : Sporteszkoz(azonosito, leiras, meret)
{
    public bool Cipovel { get; } = cipovel;

    public override int Bevetel()
    {
        int osszeg = 0;
        int elsoNap = Cipovel ? 13000 : 9000;
        int tobbiNap = Cipovel ? 4500 : 3500;

        foreach (var b in Foglalasok.FoglaltIdoszakok)
        {
            osszeg += elsoNap + (b.NapokSzama - 1) * tobbiNap;
        }
        return osszeg;
    }

    public override string ToString()
    {
        string cipo = Cipovel ? ", cipővel" : "";
        int napok = Foglalasok.FoglaltIdoszakok.Sum(b => b.NapokSzama);
        return $"{Leiras}{cipo}: {napok} nap kölcsönzés, a várható bevétel: {Bevetel()} Ft";
    }
}