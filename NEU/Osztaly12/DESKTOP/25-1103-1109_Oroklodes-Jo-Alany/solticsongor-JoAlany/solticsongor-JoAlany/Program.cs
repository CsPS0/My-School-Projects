using joalanyLib;

Nyilvantartas nyilvantartas = new Nyilvantartas();

try
{
    string file = "input.txt";
    string[] sorok = File.ReadAllLines(file);

    foreach (string sor in sorok)
    {
        try
        {
            Szemely szemely = SzemelyFactory.Factory(sor);
            nyilvantartas.Hozzaad(szemely);
            Console.WriteLine(szemely);
        }
        catch (HibasEletkorException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Hiba a feldolgozás során: {e.Message}");
        }
    }
}

catch (FileNotFoundException)
{
    Console.WriteLine("A megadott fájl nem található.");
}
catch (Exception e)
{
    Console.WriteLine($"Váratlan hiba: {e.Message}");
}

Console.WriteLine($"\nDiákok száma: {nyilvantartas.Diakok.Count()}");
Console.WriteLine($"Tanárok száma: {nyilvantartas.Tanarok.Count()}");

double atlagosEletkor = nyilvantartas.Tanarok.Average(x => x.Kor);
Console.WriteLine($"Tanárok átlagos életkora: {atlagosEletkor:F2}");

var csoportositottDiakok = nyilvantartas.Diakok
    .GroupBy(x => x.PuskakSzama)
    .OrderBy(y => y.Key);

Console.WriteLine("\nDiákok csoportosítása puskák száma szerint:");
foreach (var csoport in csoportositottDiakok)
{
    Console.WriteLine($"Puskák száma: {csoport.Key}, Diákok száma: {csoport.Count()}");
}