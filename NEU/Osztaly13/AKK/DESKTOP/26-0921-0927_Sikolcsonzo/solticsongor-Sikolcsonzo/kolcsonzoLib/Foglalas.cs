namespace kolcsonzoLib;

public class Foglalas
{
    private readonly List<Berles> berlesek = [];

    public IReadOnlyList<Berles> FoglaltIdoszakok => berlesek;

    public bool SzabadE(DateOnly kezdet, int napokSzama)
    {
        DateOnly veg = kezdet.AddDays(napokSzama - 1);

        return !berlesek.Any(b =>
        {
            int extraSzervizNap = b.NapokSzama > 5 ? 1 : 0;
            DateOnly foglaltVeg = b.BerlesVege.AddDays(extraSzervizNap);

            return kezdet <= foglaltVeg && b.Berleskezdet <= veg;
        });
    }

    public static Foglalas operator +(Foglalas f, Berles b)
    {
        if (!f.SzabadE(b.Berleskezdet, b.NapokSzama))
        {
            throw new HibasFoglalasException();
        }

        f.berlesek.Add(b);
        return f;
    }

    public override string ToString()
    {
        List<string> sorok = [];

        foreach (var b in berlesek.OrderBy(b => b.Berleskezdet))
        {
            sorok.Add(b.ToString());

            if (b.NapokSzama > 5)
            {
                sorok.Add($"{b.BerlesVege:yyyy. MM. dd.} - {b.BerlesVege.AddDays(1):yyyy. MM. dd.} szervíz");
            }
        }

        return string.Join(Environment.NewLine, sorok);
    }
}