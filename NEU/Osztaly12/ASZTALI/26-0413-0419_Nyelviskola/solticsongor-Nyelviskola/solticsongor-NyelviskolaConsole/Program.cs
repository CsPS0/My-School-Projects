using System.Globalization;
using solticsongor_NyelviskolaLib;

DataStore.InitCSV();
DataStore.Instance!.ExportToJson();

Console.WriteLine($"6. feladat: {DataStore.Instance.TanitasiAlkalmak.Count(x => x.AdottHonapbanVane(2023, 4))} alkalmat jegyeztek fel 2023. áprilisában.");

Console.Write($"7. feladat: A tanár neve: ");
var nev = Console.ReadLine();
var keresettTanar = DataStore.Instance.Tanarok.FirstOrDefault(x => x.Nev == nev);
if (keresettTanar is null)
{
    Console.WriteLine("\tIlyen néven nem található tanár.");
}
else
{
    Console.WriteLine($"\tTelefon: {keresettTanar.Telefon}");
    Console.WriteLine($"\tEmail: {keresettTanar.Email}");
}

Console.WriteLine("8. feladat: A 3 legtöbb alkalmat tanító tanár:");
var list8 = DataStore.Instance.TanitasiAlkalmak
    .GroupBy(x => x.TanarID)
    .Select(x => new { id = x.Key, db = x.Count() })
    .OrderByDescending(x => x.db)
    .Take(3)
    .ToList();

foreach (var item in list8)
{
    var tanar = DataStore.Instance.Tanarok.First(x => x.TanarID == item.id);
    var nyelv = DataStore.Instance.Nyelvek.First(x => x.NyelvID == tanar.NyelvID);
    Console.WriteLine($"\t{tanar.Nev} ({nyelv.NyelvNev}): {item.db} alkalom");
}

Console.WriteLine("9. feladat: A 3 legtöbb pénzt kereső tanár:");
var list9 = DataStore.Instance.TanitasiAlkalmak
    .GroupBy(x => x.TanarID)
    .Select(x =>
    {
        var tanar = DataStore.Instance.Tanarok.First(t => t.TanarID == x.Key);
        return new { id = x.Key, osszBevetel = x.Sum(y => y.GetAlkalomDij(tanar.Oradij)) };
    })
    .OrderByDescending(x => x.osszBevetel)
    .Take(3)
    .ToList();

var huCulture = new CultureInfo("hu-HU");
foreach (var item in list9)
{
    var tanar = DataStore.Instance.Tanarok.First(x => x.TanarID == item.id);
    var nyelv = DataStore.Instance.Nyelvek.First(x => x.NyelvID == tanar.NyelvID);
    Console.WriteLine($"\t{tanar.Nev} ({nyelv.NyelvNev}): {item.osszBevetel.ToString("N0", huCulture)} Ft");
}
