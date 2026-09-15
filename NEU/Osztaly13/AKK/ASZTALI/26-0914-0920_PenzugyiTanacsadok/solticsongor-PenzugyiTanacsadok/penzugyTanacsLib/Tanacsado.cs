namespace penzugyTanacsLib;

public class Tanacsado
{
    public int TanacsadoID { get; init; }
    public string Nev { get; init; }
    public int SzakteruletID { get; init; }
    public int Oradij { get; init; }
    public string Telefon { get; init; }
    public string Email { get; init; }

    public Tanacsado(string data)
    {
        string[] datas = data.Split(";");
        TanacsadoID = int.Parse(datas[0]);
        Nev = datas[1];
        SzakteruletID = int.Parse(datas[2]);
        Oradij = int.Parse(datas[3]);
        Telefon = datas[4];
        Email = datas[5];
    }
}
