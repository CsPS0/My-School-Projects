namespace kolcsonzoLib;

public sealed class Silec(string azonosito, string leiras, int meret) : Sporteszkoz(azonosito, leiras, meret)
{
    public override int Bevetel()
    {
        int osszeg = 0;
        foreach (var b in Foglalasok.FoglaltIdoszakok)
        {
            int ar = 8000 + (b.NapokSzama -1) * 3000;
            if (b.NapokSzama > 7)
            {
                ar = (int)(ar * 0.9);
            }
            osszeg += ar;
        }
        return osszeg;
    }

    public override string ToString() =>
        $"{Leiras}: {Foglalasok.FoglaltIdoszakok.Sum(b => b.NapokSzama)} nap kölcsönzés, a várható bevétel: {Bevetel()} Ft";
}