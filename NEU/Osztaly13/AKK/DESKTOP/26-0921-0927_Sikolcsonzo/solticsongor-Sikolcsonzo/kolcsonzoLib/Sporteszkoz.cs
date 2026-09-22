namespace kolcsonzoLib;

public abstract class Sporteszkoz(string azonosito, string leiras, int meret)
{
    public string Azonosito { get; } = azonosito;
    public string Leiras { get; } = leiras;
    public int Meret { get; } = meret;
    public Foglalas Foglalasok { get; } = new();

    public void UjBerles(Berles berles) => _ = Foglalasok + berles;

    public abstract int Bevetel();

    public bool this[DateOnly datum] => Foglalasok.SzabadE(datum, 1);
}