namespace kemiaLib;

public class Vegyulet : IReakcioKepes
{
    private List<(KemiaiElem Elem, int Arany)> alkotok;

    public Vegyulet(List<(KemiaiElem Elem, int Arany)> alkotok)
    {
        this.alkotok = alkotok;
    }

    public bool IsSzerves()
    {
        return alkotok.Any(a => a.Elem.Vegyjel == "C");
    }

    public bool IsSzenhidrat()
    {
        bool hasC = alkotok.Any(a => a.Elem.Vegyjel == "C");
        bool hasH = alkotok.Any(a => a.Elem.Vegyjel == "H");
        bool hasO = alkotok.Any(a => a.Elem.Vegyjel == "O");
        bool onlyCHO = alkotok.All(a => a.Elem.Vegyjel == "C" || a.Elem.Vegyjel == "H" || a.Elem.Vegyjel == "O");

        if (hasC && hasH && hasO && onlyCHO)
        {
            int hCount = alkotok.First(a => a.Elem.Vegyjel == "H").Arany;
            int oCount = alkotok.First(a => a.Elem.Vegyjel == "O").Arany;
            return hCount == 2 * oCount;
        }

        return false;
    }

    public bool ReakciobaLephet()
    {
        return alkotok.Any(a => a.Elem.ReakciobaLephet());
    }

    public bool ReakciobaLephet(IReakcioKepes other)
    {
        return alkotok.Any(a => a.Elem.ReakciobaLephet(other));
    }

    public override string ToString()
    {
        string formula = string.Join("", alkotok.Select(a => $"{a.Elem.Vegyjel}{(a.Arany > 1 ? a.Arany.ToString() : "")}"));
        string szerves = IsSzerves() ? "szerves" : "szervetlen";
        string szenhidrat = IsSzenhidrat() ? "szénhidrát" : "";
        return $"{formula} ({szerves}{(string.IsNullOrEmpty(szenhidrat) ? "" : ", " + szenhidrat)})";
    }
}
