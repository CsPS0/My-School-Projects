namespace kemiaLib;

public sealed class NemesGaz : KemiaiElem
{
    public NemesGaz(string vegyjel, int rendszam, int focsoport) 
        : base(vegyjel, rendszam, focsoport)
    {
        if (focsoport != 8)
        {
            throw new ArgumentException("A nemesgáz főcsoportja csak 8-as lehet!");
        }
    }

    public override bool ReakciobaLephet() => false;
    public override bool ReakciobaLephet(IReakcioKepes other) => false;
}
