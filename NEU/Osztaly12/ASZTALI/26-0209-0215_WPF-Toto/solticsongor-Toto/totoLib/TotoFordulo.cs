namespace totoLib;

public class TotoFordulo
{
    public int Ev { get; set; }
    public int Het { get; set; }
    public int Fordulo { get; set; }
    public int T13p1 { get; set; }
    public long Ny13p1 { get; set; }
    public string Eredmenyek { get; set; }

    public TotoFordulo(string sor)
    {
        string[] s = sor.Split(';');
        Ev = int.Parse(s[0]);
        Het = int.Parse(s[1]);
        Fordulo = int.Parse(s[2]);
        T13p1 = int.Parse(s[3]);
        Ny13p1 = long.Parse(s[4]);
        Eredmenyek = s[5];
    }
}
