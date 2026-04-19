using System.Text.Json.Serialization;

namespace solticsongor_NyelviskolaLib;

public class Tanar
{
    [JsonPropertyName("tanar_id")]
    public int TanarID {get; init;}

    [JsonPropertyName("nev")]
    public string Nev {get; init;}

    [JsonPropertyName("nyelv_id")]
    public int NyelvID {get; init;}

    [JsonPropertyName("oradij")]
    public int Oradij {get; init;}

    [JsonPropertyName("telefon")]
    public string Telefon {get; init;}

    [JsonPropertyName("email")]
    public string Email {get; init;}

    [JsonPropertyName("net")]
    public bool Net {get; init;}

    public Tanar(string adatSor)
    {
        string[] adatok = adatSor.Split(';');
        TanarID = int.Parse(adatok[0]);
        Nev  = adatok[1];
        NyelvID = int.Parse(adatok[2]);
        Oradij = int.Parse(adatok[3]);
        Telefon = adatok[4];
        Email = adatok[5];
        Net = adatok[6] == "1";
    }
}