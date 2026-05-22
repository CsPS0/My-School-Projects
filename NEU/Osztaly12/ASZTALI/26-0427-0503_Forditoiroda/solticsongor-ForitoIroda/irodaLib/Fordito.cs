namespace irodaLib;

public class Fordito
{
    public int Id { get; set; }
    public string Nev { get; set; }
    public int NyelvId { get; set; }
    public int ForditasiDij { get; set; }
    public int NapiOldalszam { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }

    public Fordito(string sor)
    {
        var m = sor.Split(';');
        Id = int.Parse(m[0]);
        Nev = m[1];
        NyelvId = int.Parse(m[2]);
        ForditasiDij = int.Parse(m[3]);
        NapiOldalszam = int.Parse(m[4]);
        Telefon = m[5];
        Email = m[6];
    }
}
