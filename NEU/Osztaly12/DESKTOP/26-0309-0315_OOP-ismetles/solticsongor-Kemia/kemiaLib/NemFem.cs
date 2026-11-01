namespace kemiaLib;

public sealed class NemFem : KemiaiElem
{
    public NemFem(string vegyjel, int rendszam, int focsoport) 
        : base(vegyjel, rendszam, focsoport)
    {
    }

    public override bool ReakciobaLephet() => true;

    public override bool ReakciobaLephet(IReakcioKepes other)
    {
        if (other is KemiaiElem elem && elem.ReakciobaLephet())
        {
            return (this.Focsoport + elem.Focsoport) % 8 == 0;
        }
        return false;
    }
}
