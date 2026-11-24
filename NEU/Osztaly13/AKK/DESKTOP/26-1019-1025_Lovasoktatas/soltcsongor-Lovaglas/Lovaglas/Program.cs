using System.Globalization;
using LovaglasLibrary;

var culture = new CultureInfo("hu-HU");
DataStore.InitCSV();

#region 5. feladat
Console.WriteLine($"5. feladat: {DataStore.Instance!.Orarend.Count(x => x.Hossz >= 45)} lovaglás tartott legalább 45 percig.");
#endregion

#region 6. feladat
Console.Write("6. feladat: Az oktató neve: ");
string nevBekeres = Console.ReadLine() ?? "";
var oktato = DataStore.Instance!.Oktatok.FirstOrDefault(x => x.Nev.Equals(nevBekeres.Trim(), StringComparison.OrdinalIgnoreCase));
if (oktato is null)
{
    Console.WriteLine("\tIlyen néven nem található oktató!");
}
else
{
    Console.WriteLine($"\tEmail: {oktato.Email}" +
        $"\n\tTelefon: {oktato.Telefon}" +
        $"\n\tÓradíj: {oktato.Oradij.ToString("N0", culture)} Ft");
}
#endregion

#region 7. feladat
var top3 = DataStore.Instance!.Orarend
    .GroupBy(x => x.Oktato_ID)
    .Select(g => new
    {
        Oktato = DataStore.Instance!.Oktatok.First(ok => ok.Oktato_ID == g.Key),
        OsszKereset = g.Sum(o => o.Keresett)
    })
    .OrderByDescending(x => x.OsszKereset)
    .Take(3);

Console.WriteLine("7. feladat: A 3 legtöbbet kereső oktató:");
foreach (var item in top3)
{
    Console.WriteLine($"\t{item.Oktato.Nev}: {item.OsszKereset.ToString("N0", culture)} Ft");
}
#endregion
