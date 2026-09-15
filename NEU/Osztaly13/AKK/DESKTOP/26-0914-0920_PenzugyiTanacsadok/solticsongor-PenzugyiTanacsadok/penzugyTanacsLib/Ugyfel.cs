namespace penzugyTanacsLib;

public class Ugyfel
{
    public int UgyfelID { get; init; }
    public string Nev { get; init; }
    public string Telefon { get; init; }
    public string Email { get; init; }

    public Ugyfel(string data)
    {
        string[] datas = data.Split(";");
        UgyfelID = int.Parse(datas[0]);
        Nev = datas[1];
        Telefon = datas[2];
        Email = datas[3];
    }
}
