using szalonLib;

var data = new DataStore();

// 5. feladat
int morning = data.Kezelesek.Count(x => x.Ido < new TimeOnly(12, 0, 0));
Console.WriteLine($"5. feladat: {morning} kezelés esett délelőttre");

// 6. feladat
Console.Write("6. feladat: A vendég neve: ");
string name = Console.ReadLine() ?? "";
var guest = data.Vendegek.FirstOrDefault(x => x.Nev.Equals(name, StringComparison.OrdinalIgnoreCase));

if (guest != null)
{
    Console.WriteLine($"\tCím: {guest.Cim}\n\tTelefon: {guest.Telefon}");
}
else
{
    Console.WriteLine("Ilyen néven nem található vendég");
}

// 7. feladat
Console.WriteLine("7. feladat: A 3 legmagasabb bevételű alkalmazott: ");
var best = data.Alkalmazottak
    .OrderByDescending(x => data.AlkalmazottBevetel(x.Nev))
    .ThenBy(x => x.Nev)
    .Take(3);

foreach (var emp in best)
{
    string job = data.Szakmak.First(x => x.SzakmaID == emp.SzakmaID).Nev;
    Console.WriteLine($"\t{emp.Nev} ({job}): {data.AlkalmazottBevetel(emp.Nev)} Ft");
}