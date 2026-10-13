namespace fleatsLib;

public class Busz
{
    public string Rendszam { get; }
    public int Ferohely { get; }
    public int AktualisUtasszam { get; private set; }
    public int OsszesSzallitottUtas { get; private set; }
    public int SzabadHelyek => Ferohely - AktualisUtasszam;

    public Busz(string plate, int cap)
    {
        if (string.IsNullOrWhiteSpace(plate))
        {
            throw new ArgumentException("A rendszám nem lehet üres vagy null.", nameof(plate));
        }

        if (cap <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cap), "A férőhelynek 0-nál nagyobbnak kell lennie.");
        }

        Rendszam = plate.Trim();
        Ferohely = cap;
        AktualisUtasszam = 0;
        OsszesSzallitottUtas = 0;
    }

    public int MegallobanKezeles(int off, int on)
    {
        if (off < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(off), "A leszálló utasok száma nem lehet negatív.");
        }

        if (on < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(on), "A felszállni kívánó utasok száma nem lehet negatív.");
        }

        if (off > AktualisUtasszam)
        {
            throw new InvalidOperationException("Nem szállhat le több utas, mint amennyi jelenleg a buszon tartózkodik.");
        }

        AktualisUtasszam -= off;

        int free = SzabadHelyek;
        if (on <= free)
        {
            AktualisUtasszam += on;
            OsszesSzallitottUtas += on;
            return 0;
        }

        AktualisUtasszam += free;
        OsszesSzallitottUtas += free;
        return on - free;
    }

    public void ResetNapiAdatok()
    {
        AktualisUtasszam = 0;
        OsszesSzallitottUtas = 0;
    }
}
