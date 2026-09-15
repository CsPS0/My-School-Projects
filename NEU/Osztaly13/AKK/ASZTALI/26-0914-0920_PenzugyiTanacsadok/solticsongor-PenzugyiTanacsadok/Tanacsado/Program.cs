using penzugyTanacsLib;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

CultureInfo.CurrentCulture = new CultureInfo("hu-HU");

var db = new Database();
db.LoadData("ugyfel.csv", "szakterulet.csv", "tanacsado.csv", "talalkozo.csv");

// 5. feladat
int countOver3Hours = db.TalalkozoList.Count(t => t.Idotartam >= 3);
Console.WriteLine($"5. feladat: {countOver3Hours} találkozó tartott legalább 3 órát");

// 6. feladat
Console.Write("6. feladat: A tanácsadó neve: ");
string? keresettNev = Console.ReadLine();

Tanacsado? megtalaltTanacsado = null;
if (!string.IsNullOrWhiteSpace(keresettNev))
{
    foreach (var t in db.TanacsadoList)
    {
        if (string.Equals(t.Nev, keresettNev.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            megtalaltTanacsado = t;
            break;
        }
    }
}

if (megtalaltTanacsado != null)
{
    var szakterulet = db.SzakteruletList.FirstOrDefault(s => s.SzakteruletID == megtalaltTanacsado.SzakteruletID);
    Console.WriteLine($"\t- Telefon: {megtalaltTanacsado.Telefon}");
    Console.WriteLine($"\t- Email: {megtalaltTanacsado.Email}");
    Console.WriteLine($"\t- Szakterület: {szakterulet?.Megnevezes}");
    Console.WriteLine($"\t- Óradíj: {megtalaltTanacsado.Oradij} Ft");
}
else
{
    Console.WriteLine("Ilyen néven nem található tanácsadó");
}

// 7. feladat
Console.WriteLine("7. feladat: A 3 legtöbbet kereső tanácsadó:");
var top3 = db.TanacsadoList
    .Select(t => new
    {
        Tanacsado = t,
        Osszesen = db.TalalkozoList.Where(m => m.TanacsadoID == t.TanacsadoID).Sum(m => m.GetAr(t.Oradij))
    })
    .OrderByDescending(x => x.Osszesen)
    .Take(3);

foreach (var item in top3)
{
    Console.WriteLine($"\t{item.Tanacsado.Nev}: {item.Osszesen} Ft");
}
