#region 1.fel
using Epuletek;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

List<Epulet> lista = new List<Epulet>();

string[] sorok = File.ReadAllLines("legmagasabb.txt");

for (int i = 1; i < sorok.Length; i++)
{
    string[] tomb = sorok[i].Split(';');
    string nev = tomb[0];
    string varos = tomb[1];
    string orszag = tomb[2];
    float magassag = float.Parse(tomb[3]);
    int emelet = int.Parse(tomb[4]);
    int epult = int.Parse(tomb[5]);

    lista.Add(new Epulet(nev, varos, orszag, magassag, emelet, epult));
}
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájlok beolvasása sikeres!");
Console.ResetColor();
#endregion

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

Console.WriteLine($"Épületek száma: {lista.Count} db");
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

int emeletOssz = lista.Sum(x => x.Emelet);
double emeletAtlag = lista.Average(x => x.Emelet);

Console.WriteLine($"Emeletek összege: {emeletOssz} átlagos száma: {emeletAtlag:F1}");
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

var legmagasabb = lista.OrderByDescending(x => x.Magassag).ToList();

Console.WriteLine($"A legmagasabb épület adatai:");
Console.WriteLine($"\tNév: {legmagasabb[0].Nev}");
Console.WriteLine($"\tVáros: {legmagasabb[0].Varos}");
Console.WriteLine($"\tOrszág: {legmagasabb[0].Orszag}");
Console.WriteLine($"\tMagasság: {legmagasabb[0].Magassag}");
Console.WriteLine($"\tEmeletek száma: {legmagasabb[0].Emelet}");
Console.WriteLine($"\tÉpítés éve: {legmagasabb[0].Epult}");
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

Console.Write("Adj meg egy országot: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string forszag = Console.ReadLine();
Console.ResetColor();

bool vanEpulet = lista.Any(e => string.Equals(e.Orszag, forszag, StringComparison.OrdinalIgnoreCase));
if (vanEpulet)
{
    Console.WriteLine($"\tVan {forszag} területéről tárolt magas épület.");
}
else
{
    Console.WriteLine($"\tNincs {forszag} területéről tárolt magas épület.");
}
    #endregion

#region 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

int db = lista.Count(x => x.Magassag * 3.280839895 > 666);
Console.WriteLine($"666 lábnál magasabb épületek száma: {db}");
#endregion

#region 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

Console.Write("Országok: ");
var kiosztas = lista.OrderBy(x => x.Orszag).GroupBy(y => y.Orszag).Distinct().ToList();
for (int i = 0;i < kiosztas.Count;i++)
{
    Console.Write($"{kiosztas[i].Key}, ");
}
Console.WriteLine();
#endregion

#region 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

var epulet = lista.OrderByDescending(x => x.Emelet).ThenBy(x => x.Nev).ToList();
Console.WriteLine("Épületek:");
foreach (var i in epulet)
{
    Console.WriteLine($"\t{i.Nev}: {i.Varos};{i.Emelet}");
}
#endregion

#region 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

Console.WriteLine("A 3 legrégebben épült épület:");
var legregebb = lista.OrderBy(x => x.Epult).ToList();

for (int i = 0; i < 3; i++)
{
    Console.WriteLine($"\t{legregebb[i].Epult} - {legregebb[i].Varos}: {legregebb[i].Nev}");
}
#endregion

#region 10.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat");
Console.ResetColor();

Console.Write("Német városok: ");
var nemet = lista.Where(x => x.Orszag == "Németország").Select(y => y.Varos).Distinct().OrderBy(x => x).ToList();

foreach (var i in nemet)
{
    Console.Write($"{i}, ");
}
Console.WriteLine();
#endregion