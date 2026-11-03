namespace ConsoleApp;

public class Urparancsnoksag
{
    public List<Urhajo> Hajok { get; set; } = new List<Urhajo>();
    public List<LegenysegiTag> SzabadLegenyseg { get; set; } = new List<LegenysegiTag>();

    public void HajoRegisztracio(Urhajo hajo)
    {
        if (hajo != null && !Hajok.Contains(hajo))
        {
            Hajok.Add(hajo);
        }
    }

    public void LegenysegiTagRegisztracio(LegenysegiTag tag)
    {
        if (tag != null && !SzabadLegenyseg.Contains(tag) && tag.HozzaRendeltHajo == null)
        {
            SzabadLegenyseg.Add(tag);
        }
    }

    public void Hozzarendel(Urhajo hajo, LegenysegiTag tag)
    {
        if (!Hajok.Contains(hajo))
        {
            throw new InvalidOperationException("A hajó nem található!");
        }

        if (!SzabadLegenyseg.Contains(tag))
        {
            throw new InvalidOperationException("A tag nem elérhető!");
        }

        if (hajo.Legenyseg.Count >= hajo.Ferohely)
        {
            throw new InvalidOperationException("Nincs több szabad hely!");
        }

        SzabadLegenyseg.Remove(tag);
        hajo.Legenyseg.Add(tag);
        tag.HozzaRendeltHajo = hajo;
    }

    public void Visszavon(Urhajo hajo, LegenysegiTag tag)
    {
        if (!hajo.Legenyseg.Contains(tag))
        {
            throw new InvalidOperationException("A tag nincs a hajón!");
        }

        hajo.Legenyseg.Remove(tag);
        tag.HozzaRendeltHajo = null;

        if (!SzabadLegenyseg.Contains(tag))
        {
            SzabadLegenyseg.Add(tag);
        }
    }

    public void Athelyez(Urhajo honnan, Urhajo hova, LegenysegiTag tag)
    {
        Visszavon(honnan, tag);
        Hozzarendel(hova, tag);
    }
}
