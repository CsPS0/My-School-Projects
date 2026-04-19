using System.Text.Json.Serialization;

namespace solticsongor_NyelviskolaLib;

public class Nyelv
{
    [JsonPropertyName("nyelv_id")]
    public int NyelvID { get; init; }

    [JsonPropertyName("nyelvnev")]
    public string NyelvNev { get; init; }

    public Nyelv(string adatSor)
    {
        string[] adatok = adatSor.Split(';');
        NyelvID = int.Parse(adatok[0]);
        NyelvNev = adatok[1];
    }

    public override string ToString()
    {
        return NyelvNev;
    }
}