namespace szalonLib;

public class Kezelesek
{
    public int KezelesID { get; init; }
    public int AlkalmazottID { get; init; }
    public int VendegID { get; init; }
    public DateOnly Datum { get; init; }
    public TimeOnly Ido { get; init; }
    public int Ar { get; init; }

    public Kezelesek(string data)
    {
        string[] datas = data.Split(";");
        KezelesID = int.Parse(datas[0]);
        AlkalmazottID = int.Parse(datas[1]);
        VendegID = int.Parse(datas[2]);
        Datum = DateOnly.Parse(datas[3]);
        Ido = TimeOnly.Parse(datas[4]);
        Ar = int.Parse(datas[5]);
    }
}