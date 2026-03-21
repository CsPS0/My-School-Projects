namespace kemiaLib;

public abstract class KemiaiElem : IReakcioKepes
{
    public string Vegyjel { get; }
    public int Rendszam { get; }
    public int Focsoport { get; }

    protected KemiaiElem(string vegyjel, int rendszam, int focsoport)
    {
        Vegyjel = vegyjel;
        Rendszam = rendszam;
        Focsoport = focsoport;
    }

    public abstract bool ReakciobaLephet();
    public abstract bool ReakciobaLephet(IReakcioKepes other);
}
