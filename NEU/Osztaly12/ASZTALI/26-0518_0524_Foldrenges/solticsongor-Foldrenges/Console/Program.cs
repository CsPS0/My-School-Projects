using foldrengesLib;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

CultureInfo.CurrentCulture = new CultureInfo("hu-HU");

var db = new Database();
string naploPath = File.Exists("naplo.txt") ? "naplo.txt" : "../../../naplo.txt";
string telepulesPath = File.Exists("telepules.txt") ? "telepules.txt" : "../../../telepules.txt";

if (!File.Exists(naploPath) || !File.Exists(telepulesPath))
{
    naploPath = "../../../../naplo.txt";
    telepulesPath = "../../../../telepules.txt";
}

db.LoadData(naploPath, telepulesPath);

Console.WriteLine($"4. feladat: {db.TelepulesList.Count} db");

double avgIntensity = db.NaploList.Average(n => n.Intenzitas);
Console.WriteLine($"5. feladat: Az átlagos intenzitás {avgIntensity:F1} volt");

double maxMagnitude = db.NaploList.Where(n => n.Magnitudo.HasValue).Max(n => n.Magnitudo!.Value);
Console.WriteLine($"6. feladat: A legnagyobb magnitúdó {maxMagnitude:F1} volt");

int countOver4 = db.NaploList.Count(n => n.Magnitudo > 4.0);
Console.WriteLine($"7. feladat: Összesen {countOver4} db 4-nél nagyobb magnitúdójú földrengés volt.");

bool wasInJuly2003 = db.NaploList.Any(n => n.Datum.Year == 2003 && n.Datum.Month == 7);
Console.WriteLine($"8. feladat: {(wasInJuly2003 ? "Volt" : "Nem volt")} földrengés 2003 júliusában.");

Console.Write("9. feladat: Adjon meg egy települést: ");
string? inputCity = Console.ReadLine();
if (!string.IsNullOrEmpty(inputCity))
{
    var city = db.TelepulesList.FirstOrDefault(t => t.Nev.Equals(inputCity, StringComparison.OrdinalIgnoreCase));
    if (city == null)
    {
        Console.WriteLine("Nincs ilyen nevű település.");
    }
    else
    {
        var firstQuake = db.NaploList.FirstOrDefault(n => n.TelepId == city.Id);
        if (firstQuake != null)
        {
            Console.WriteLine($"{firstQuake.Magnitudo} - {firstQuake.Intenzitas} - {firstQuake.Ido}");
        }
        else
        {
            Console.WriteLine("Ezen a településen nem volt földrengés.");
        }
    }
}

Console.WriteLine("10. feladat: A 3 legnagyobb magnitúdójú földrengést elszenvedő település.");
var top3 = db.NaploList
    .OrderByDescending(n => n.Magnitudo)
    .Take(3)
    .Select(n => new { 
        City = db.TelepulesList.First(t => t.Id == n.TelepId).Nev,
        n.Magnitudo,
        n.RichterSkala
    });

foreach (var item in top3)
{
    Console.WriteLine($"{item.City} - {item.Magnitudo:F1} - {item.RichterSkala}");
}

Console.Write("11. feladat: Kérem adjon meg egy v\u0336á\u0336r\u0336megye nevet:  ");
string? inputCounty = Console.ReadLine();
if (!string.IsNullOrEmpty(inputCounty))
{
    var countyQuakes = db.NaploList
        .Where(n => db.TelepulesList.Any(t => t.Id == n.TelepId && t.Varmegye.Equals(inputCounty, StringComparison.OrdinalIgnoreCase)))
        .Select(n => new {
            City = db.TelepulesList.First(t => t.Id == n.TelepId).Nev,
            n.Datum,
            n.RichterSkala
        })
        .OrderBy(n => n.City);

    foreach (var quake in countyQuakes)
    {
        Console.WriteLine($"{quake.City} - {quake.Datum:yyyy-MM-dd} ({quake.RichterSkala})");
    }
}
