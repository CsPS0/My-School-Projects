using irodaLib;

var service = new AdatSzolgaltatas("fordito.csv", "nyelv.csv", "megrendeles.csv");

var march2015Orders = service.Megrendelesek
    .Count(m => m.Datum.Year == 2015 && m.Datum.Month == 3);
Console.WriteLine($"5. feladat: {march2015Orders} fordítási feladat rendeltek 2015. márciusában.");

Console.Write("6. feladat: A tanár neve: ");
var searchName = Console.ReadLine();
var foundFordito = service.Forditok.FirstOrDefault(f => f.Nev.Equals(searchName, StringComparison.OrdinalIgnoreCase));

if (foundFordito != null)
{
    Console.WriteLine($"\tTelefon: {foundFordito.Telefon}");
    Console.WriteLine($"\tEmail: {foundFordito.Email}");
}
else
{
    Console.WriteLine("Ilyen néven nem található fordító.");
}

Console.WriteLine("7. feladat: A 3 legtöbb megrendelést kapó fordító:");
var top3ByCount = service.Megrendelesek
    .GroupBy(m => m.ForditoId)
    .Select(g => new
    {
        Fordito = service.Forditok.First(f => f.Id == g.Key),
        Count = g.Count()
    })
    .OrderByDescending(x => x.Count)
    .Take(3);

foreach (var item in top3ByCount)
{
    var nyelv = service.Nyelvek.First(n => n.Id == item.Fordito.NyelvId).NyelvNev;
    Console.WriteLine($"\t{item.Fordito.Nev} ({nyelv}): {item.Count} megrendelés");
}

Console.WriteLine("8. feladat: A 3 legtöbb pénzt kereső fordító:");
var top3ByEarnings = service.Megrendelesek
    .GroupBy(m => m.ForditoId)
    .Select(g =>
    {
        var fordito = service.Forditok.First(f => f.Id == g.Key);
        var earnings = g.Sum(m => m.KiszamolAr(fordito.ForditasiDij));
        return new
        {
            Fordito = fordito,
            Earnings = earnings
        };
    })
    .OrderByDescending(x => x.Earnings)
    .Take(3);

foreach (var item in top3ByEarnings)
{
    var nyelv = service.Nyelvek.First(n => n.Id == item.Fordito.NyelvId).NyelvNev;
    Console.WriteLine($"\t{item.Fordito.Nev} ({nyelv}): {item.Earnings.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("hu-HU")).Replace(",", " ")} Ft");
}
