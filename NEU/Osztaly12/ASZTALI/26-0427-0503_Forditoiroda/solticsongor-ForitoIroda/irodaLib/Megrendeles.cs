namespace irodaLib;

public class Megrendeles
{
    public int Id { get; set; }
    public int ForditoId { get; set; }
    public int MegrendeloId { get; set; }
    public DateTime Datum { get; set; }
    public int Oldalszam { get; set; }

    public Megrendeles(string sor)
    {
        var m = sor.Split(';');
        Id = int.Parse(m[0]);
        ForditoId = int.Parse(m[1]);
        MegrendeloId = int.Parse(m[2]);
        Datum = DateTime.Parse(m[3]);
        Oldalszam = int.Parse(m[4]);
    }

    public int KiszamolAr(int forditasiDij)
    {
        return Oldalszam * forditasiDij;
    }
}
