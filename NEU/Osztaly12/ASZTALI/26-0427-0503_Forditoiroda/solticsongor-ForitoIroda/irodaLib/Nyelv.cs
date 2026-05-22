namespace irodaLib;

public class Nyelv
{
    public int Id { get; set; }
    public string NyelvNev { get; set; }

    public Nyelv(string sor)
    {
        var m = sor.Split(';');
        Id = int.Parse(m[0]);
        NyelvNev = m[1];
    }
}
