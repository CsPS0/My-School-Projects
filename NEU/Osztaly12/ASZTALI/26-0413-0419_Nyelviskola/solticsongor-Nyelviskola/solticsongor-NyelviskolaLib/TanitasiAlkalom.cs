using System.Text.Json.Serialization;

namespace solticsongor_NyelviskolaLib;

public class TanitasiAlkalom
{
    [JsonPropertyName("alkalom_id")]
    public int AlkalomID { get; init; }

    [JsonPropertyName("tanar_id")]
    public int TanarID { get; init; }

    [JsonPropertyName("datum")]
    public DateTime Datum { get; init; }

    [JsonPropertyName("kezdesido")]
    public TimeSpan KezdesIdo { get; init; }

    [JsonPropertyName("orak_szama")]
    public int OrakSzama { get; init; }

    static DateTime ParseDate(string str)
    {
        var split = str.Split('.');
        return new DateTime(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
    }

    static TimeSpan ParseTime(string str)
    {
        var split = str.Split(':');
        return new TimeSpan(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
    }

    public TanitasiAlkalom(string adatokSor)
    {
        string[] adatok = adatokSor.Split(';');
        AlkalomID = int.Parse(adatok[0]);
        TanarID = int.Parse(adatok[1]);
        Datum = ParseDate(adatok[2]);
        KezdesIdo = ParseTime(adatok[3]);
        OrakSzama = int.Parse(adatok[4]);
    }

    public int GetAlkalomDij(int oradij)
    {
        return OrakSzama * oradij;
    }

    public bool AdottHonapbanVane(int ev, int honap)
    {
        return Datum.Year == ev && Datum.Month == honap;
    }
}