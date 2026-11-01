using kemiaLib;

string[] nobleGases = { "He", "Ne", "Ar", "Kr", "Xe", "Rn" };

Console.Write("Kérem a bemeneti fájl nevét (pl. viz.txt): ");
string? filename = Console.ReadLine();

if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
{
    Console.WriteLine("A fájl nem található!");
    return;
}

try
{
    List<(KemiaiElem Elem, int Arany)> alkotok = new();
    string[] lines = File.ReadAllLines(filename);

    foreach (string line in lines)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;

        string[] parts = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4) continue;

        string vegyjel = parts[0];
        int rendszam = int.Parse(parts[1]);
        int focsoport = int.Parse(parts[2]);
        int arany = int.Parse(parts[3]);

        KemiaiElem elem;
        bool isNoble = false;
        foreach (string ng in nobleGases)
        {
            if (ng == vegyjel)
            {
                isNoble = true;
                break;
            }
        }

        if (isNoble)
        {
            elem = new NemesGaz(vegyjel, rendszam, focsoport);
        }
        else
        {
            elem = new NemFem(vegyjel, rendszam, focsoport);
        }

        alkotok.Add((elem, arany));
    }

    Vegyulet vegyulet = new Vegyulet(alkotok);
    Console.WriteLine($"Vegyület: {vegyulet.ToString()}");
    Console.WriteLine($"A vegyület reakcióba léphet: {(vegyulet.ReakciobaLephet() ? "igen" : "nem")}");
    
    if (alkotok.Count >= 2)
    {
        var e1 = alkotok[0].Elem;
        var e2 = alkotok[1].Elem;
        bool reactivePair = e1.ReakciobaLephet(e2);
        Console.WriteLine($"Az összetevők ({e1.Vegyjel} és {e2.Vegyjel}) egymással reakcióba tudnának lépni? {(reactivePair ? "igen" : "nem")}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba történt a beolvasás során: {ex.Message}");
}