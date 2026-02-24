using totoLib;

string path = "toto.txt";
if (!File.Exists(path)) path = "toto.txt";

List<TotoFordulo> fordulok = new List<TotoFordulo>();
foreach (var sor in File.ReadAllLines(path).Skip(1))
{
    if (!string.IsNullOrWhiteSpace(sor))
        fordulok.Add(new TotoFordulo(sor));
}

Console.WriteLine($"3. feladat: Fordulók száma: {fordulok.Count}");

int telitalalatosSzelvenyekSzama = fordulok.Sum(f => f.T13p1);
Console.WriteLine($"4. feladat: Telitalálatos szelvények száma: {telitalalatosSzelvenyekSzama} db");

double osszNyeremeny = fordulok.Sum(f => (double)f.T13p1 * f.Ny13p1);
double atlagNyeremeny = osszNyeremeny / fordulok.Count;
Console.WriteLine($"5. feladat: Átlag: {Math.Round(atlagNyeremeny)} Ft");

var nyeremenyesFordulok = fordulok.Where(f => f.Ny13p1 > 0).ToList();
var legnagyobb = nyeremenyesFordulok.OrderByDescending(f => f.Ny13p1).First();
var legkisebb = nyeremenyesFordulok.OrderBy(f => f.Ny13p1).First();

Console.WriteLine("6. feladat:");
Console.WriteLine("	Legnagyobb:");
Console.WriteLine($"	Év: {legnagyobb.Ev}");
Console.WriteLine($"	Hét: {legnagyobb.Het}.");
Console.WriteLine($"	Forduló: {legnagyobb.Fordulo}.");
Console.WriteLine($"	Telitalálat: {legnagyobb.T13p1} db");
Console.WriteLine($"	Nyeremény: {legnagyobb.Ny13p1} Ft");
Console.WriteLine($"	Eredmények: {legnagyobb.Eredmenyek}");
Console.WriteLine();
Console.WriteLine("	Legkisebb:");
Console.WriteLine($"	Év: {legkisebb.Ev}");
Console.WriteLine($"	Hét: {legkisebb.Het}.");
Console.WriteLine($"	Forduló: {legkisebb.Fordulo}.");
Console.WriteLine($"	Telitalálat: {legkisebb.T13p1} db");
Console.WriteLine($"	Nyeremény: {legkisebb.Ny13p1} Ft");
Console.WriteLine($"	Eredmények: {legkisebb.Eredmenyek}");

bool voltE = false;
foreach (var f in fordulok)
{
    EredmenyElemzo ee = new EredmenyElemzo(f.Eredmenyek);
    if (ee.NemvoltDontetlenMerkozes)
    {
        voltE = true;
        break;
    }
}
Console.WriteLine($"8. feladat: {(voltE ? "Volt" : "Nem volt")} döntetlen nélküli forduló!");
