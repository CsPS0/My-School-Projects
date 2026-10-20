using LovaglasLibrary;

DataStore.InitCSV();

#region 5.feladat
Console.WriteLine($"5.feladat: {DataStore.Instance!.Orarend.Count(x => x.Hossz >= 45)} lovaglás tartott 45 percig");
#endregion

#region 6.feladat
Console.Write("6.feladat: adja a nevet: ");
string nevBekeres = Console.ReadLine() ?? "";
var oktato = DataStore.Instance!.Oktatok.FirstOrDefault(x => x.Nev == nevBekeres);
if (oktato is null)
{
    Console.WriteLine("\tIlyen néven nem található");
}
else
{
    Console.WriteLine($"\tEmail: {oktato.Email}" +
        $"\n\tTelefon: {oktato.Telefon}" +
        $"\n\tÓradíj: {oktato.Oradij}");
}
#endregion

#region 7.feladat
var oktatok = DataStore.Instance!.Oktatok
    .Select(x => x.Nev);
var orarend = DataStore.Instance!.Orarend
    .OrderByDescending(x => x.Keresett)
    .Take(3);
Console.WriteLine($"7.feladat: A 3 legtöbbet kereső oktató:" +
    $"\n\t {oktatok}: {orarend} Ft");
#endregion