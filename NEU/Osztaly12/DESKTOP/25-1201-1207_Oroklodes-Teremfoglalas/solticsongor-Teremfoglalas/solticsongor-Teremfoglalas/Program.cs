using teremLib;

TeremNyilvantartas teremNyilvantartas = new TeremNyilvantartas();
List<Foglalas> foglalasiKerelmek = new List<Foglalas>();

try
{
    string[] teremSorok = File.ReadAllLines("termek.txt").Skip(1).ToArray();
    foreach (string sor in teremSorok)
    {
        if (string.IsNullOrWhiteSpace(sor)) continue;
        string[] adatok = sor.Split(';');
        char tipus = char.Parse(adatok[0]);
        int azon = int.Parse(adatok[1]);
        int ferohely = int.Parse(adatok[2]);

        if (tipus == 'A')
        {
            teremNyilvantartas.AddTerem(new AltalanosTerem(azon, ferohely));
        }
        else if (tipus == 'S')
        {
            int takaritasIdo = int.Parse(adatok[3]);
            teremNyilvantartas.AddTerem(new SpecialisTerem(azon, ferohely, takaritasIdo));
        }
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("Hiba: a 'termek.txt' fájl nem található.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba a 'termek.txt' olvasása során: {ex.Message}");
    return;
}

File.WriteAllText("hibalista.txt", string.Empty);

try
{
    string[] foglalasSorok = File.ReadAllLines("foglalasok.txt").Skip(1).ToArray();
    foreach (string sor in foglalasSorok)
    {
        if (string.IsNullOrWhiteSpace(sor)) continue;
        string[] adatok = sor.Split(';');
        DateTime kezdet = DateTime.Parse(adatok[0]);
        int idotartam = int.Parse(adatok[1]);
        int teremAzon = int.Parse(adatok[2]);
        string tanarAzon = adatok[3];

        try
        {
            Foglalas ujFoglalas = new Foglalas(kezdet, idotartam, teremAzon, tanarAzon);
            foglalasiKerelmek.Add(ujFoglalas);
        }
        catch (IdotartamException ex)
        {
            string logMessage = $"{kezdet:yyyy.MM.dd HH:mm:ss};" +
                                $"{idotartam};" +
                                $"{teremAzon};" +
                                $"{tanarAzon} - {ex.Message}";
            File.AppendAllText("hibalista.txt", logMessage + Environment.NewLine);
        }
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("Hiba: a 'foglalasok.txt' fájl nem található.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba a 'foglalasok.txt' olvasása során: {ex.Message}");
    return;
}

teremNyilvantartas.TeremFoglalasok(foglalasiKerelmek);

Console.WriteLine("Az elérhető termek:");
foreach (int azon in teremNyilvantartas.GetTeremAzonositok())
{
    Console.WriteLine($"t{azon}");
}
Console.WriteLine();

Console.WriteLine("A termek a foglalások után:");
foreach (Terem terem in teremNyilvantartas.GetAllTermek().OrderBy(t => t.Azonosito))
{
    Console.WriteLine(terem.ToString());
}

Console.Write("Kérem egy tanár azonosítóját: ");
string keresettTanarAzon = Console.ReadLine() ?? string.Empty;
Console.WriteLine("A tanár foglalásai:");

foreach (Terem terem in teremNyilvantartas.GetAllTermek().OrderBy(t => t.Azonosito))
{
    var tanarFoglalasai = terem.Orarend.ToString()
                                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                .Where(line => line.Contains($" {keresettTanarAzon}"))
                                .ToList();
    if (tanarFoglalasai.Any())
    {
        Console.WriteLine($"t{terem.Azonosito}");
        foreach (var foglalas in tanarFoglalasai)
        {
            Console.WriteLine(foglalas);
        }
    }
}