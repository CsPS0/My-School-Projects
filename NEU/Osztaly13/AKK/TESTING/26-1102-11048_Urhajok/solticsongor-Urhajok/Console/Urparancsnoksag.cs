namespace ConsoleApp;

public class Urparancsnoksag
{
    public List<Urhajo> Hajok { get; } = new();
    public List<LegenysegiTag> SzabadLegenyseg { get; } = new();

    public void HajoRegisztracio(Urhajo hajo)
    {
        if (hajo == null)
        {
            throw new ArgumentNullException(nameof(hajo));
        }

        if (!Hajok.Contains(hajo))
        {
            Hajok.Add(hajo);
        }
    }

    public void LegenysegiTagRegisztracio(LegenysegiTag tag)
    {
        if (tag == null)
        {
            throw new ArgumentNullException(nameof(tag));
        }

        if (!SzabadLegenyseg.Contains(tag) && tag.HozzaRendeltHajo == null)
        {
            SzabadLegenyseg.Add(tag);
        }
    }

    public void Hozzarendel(Urhajo hajo, LegenysegiTag tag)
    {
        if (hajo == null) throw new ArgumentNullException(nameof(hajo));
        if (tag == null) throw new ArgumentNullException(nameof(tag));

        if (!Hajok.Contains(hajo))
        {
            throw new InvalidOperationException("Az űrhajó nem tartozik a parancsnoksághoz.");
        }

        if (!SzabadLegenyseg.Contains(tag))
        {
            throw new InvalidOperationException("A legénységi tag nem elérhető vagy már hajóhoz van rendelve.");
        }

        if (hajo.Legenyseg.Count >= hajo.Ferohely)
        {
            throw new InvalidOperationException("Az űrhajón nincs több szabad férőhely.");
        }

        SzabadLegenyseg.Remove(tag);
        hajo.Legenyseg.Add(tag);
        tag.HozzaRendeltHajo = hajo;
    }

    public void Visszavon(Urhajo hajo, LegenysegiTag tag)
    {
        if (hajo == null) throw new ArgumentNullException(nameof(hajo));
        if (tag == null) throw new ArgumentNullException(nameof(tag));

        if (!hajo.Legenyseg.Contains(tag))
        {
            throw new InvalidOperationException("A megadott tag nem része az űrhajó legénységének.");
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
