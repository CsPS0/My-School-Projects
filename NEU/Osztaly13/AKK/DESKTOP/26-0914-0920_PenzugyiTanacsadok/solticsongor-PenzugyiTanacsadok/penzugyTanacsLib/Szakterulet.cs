namespace penzugyTanacsLib;

public class Szakterulet
{
    public int SzakteruletID { get; init; }
    public string Megnevezes { get; init; }

    public Szakterulet(string data)
    {
        string[] datas = data.Split(";");
        SzakteruletID = int.Parse(datas[0]);
        Megnevezes = datas[1];
    }
}
