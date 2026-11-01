using dobozokLib;

List<Filter> filterLista = new List<Filter>();
Filterek? filterek = null;

try
{
    string[] sorok = File.ReadAllLines("filterek.txt").Skip(1).ToArray();
    foreach (string sor in sorok)
    {
        if (string.IsNullOrWhiteSpace(sor)) continue;
        string[] adatok = sor.Split(';');
        string id = adatok[0];
        string tipus = adatok[1];
        int ar = int.Parse(adatok[2]);
        filterLista.Add(new Filter(id, tipus, ar));
    }
    filterek = new Filterek(filterLista);
}
catch (FileNotFoundException)
{
    Console.WriteLine("Hiba: a 'filterek.txt' fájl nem található.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba a 'filterek.txt' olvasása során: {ex.Message}");
    return;
}

Console.WriteLine("Elérhető gyógynövény filterek:");
foreach (var filter in filterek.GyogynovenyFilterek())
{
    Console.WriteLine(filter);
}
Console.WriteLine();

Raktar raktar = new Raktar();
File.WriteAllText("hibalista.txt", string.Empty);

try
{
    string[] sorok = File.ReadAllLines("dobozok.txt").Skip(1).ToArray();
    foreach (string sor in sorok)
    {
        if (string.IsNullOrWhiteSpace(sor)) continue;
        
        try
        {
            TeasDoboz doboz = DobozFactory.Factory(sor, filterek);
            raktar.Hozzaad(doboz);
        }
        catch (HibasAzonositoException)
        {
            string hiba = $"A megadott filter azonosító nem létezik. ({sor})";
            File.AppendAllText("hibalista.txt", hiba + Environment.NewLine);
        }
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine("Hiba: a 'dobozok.txt' fájl nem található.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Hiba a 'dobozok.txt' olvasása során: {ex.Message}");
    return;
}

Console.WriteLine("Elkészített teásdobozok:");
foreach (var doboz in raktar.Dobozok)
{
    Console.WriteLine(doboz);
}