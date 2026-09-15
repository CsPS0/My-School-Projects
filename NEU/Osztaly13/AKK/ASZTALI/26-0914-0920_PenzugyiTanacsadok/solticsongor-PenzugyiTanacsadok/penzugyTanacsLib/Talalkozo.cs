namespace penzugyTanacsLib;

public class Talalkozo
{
    public int TalalkozoID { get; init; }
    public int TanacsadoID { get; init; }
    public int UgyfelID { get; init; }
    public DateOnly Datum { get; init; }
    public TimeOnly Idopont { get; init; }
    public int Idotartam { get; init; }

    public Talalkozo(string data)
    {
        string[] datas = data.Split(";");
        TalalkozoID = int.Parse(datas[0]);
        TanacsadoID = int.Parse(datas[1]);
        UgyfelID = int.Parse(datas[2]);

        string[] dateParts = datas[3].TrimEnd('.').Split('.');
        Datum = new DateOnly(int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2]));

        string[] timeParts = datas[4].Split(':');
        Idopont = new TimeOnly(int.Parse(timeParts[0]), int.Parse(timeParts[1]), timeParts.Length > 2 ? int.Parse(timeParts[2]) : 0);

        Idotartam = int.Parse(datas[5]);
    }

    public int GetAr(int oraDij)
    {
        return oraDij * Idotartam;
    }
}
