using filmekLib;

var data = new DataStore();

// 4. feladat
Console.WriteLine($"4. feladat: {data.Feladat4()} db alkotó született a 20. században.");

// 5. feladat
Console.WriteLine($"5. feladat: {data.Feladat5()} db film ünnepli idén készítésének 50. évfodulóját.");

// 6. feladat
Console.WriteLine("6. feladat: Egy tanórán megnézhető filmek:");
foreach (var film in data.Feladat6())
{
    Console.WriteLine($"\t{film.Cim} ({film.Mufaj}): {film.Hossz} perc");
}

// 7. feladat
Console.Write("7. feladat: A film címe: ");
string searchTitle = Console.ReadLine() ?? "";
var (foundFilm, directors) = data.Feladat7(searchTitle);
if (foundFilm == null)
{
    Console.WriteLine("Ilyen néven nem található film.");
}
else
{
    Console.WriteLine($"Rendező: {string.Join(", ", directors)}");
    Console.WriteLine($"Készítés éve: {foundFilm.Ev}");
    Console.WriteLine($"Link: https://videotorium.hu/hu/recordings/{foundFilm.FilmAzon}");
}

// 8. feladat
Console.WriteLine("8. feladat: A 2 legtöbb filmben főszereplő színész: ");
foreach (var item in data.Feladat8())
{
    Console.WriteLine($"{item.Nev}: {item.Db} alkalom ");
}

// 9. feladat
Console.WriteLine("9. feladat: Dajka Margit színésztársai:");
foreach (var item in data.Feladat9())
{
    Console.WriteLine($"{item.FilmCim}: {string.Join(", ", item.Szineszek)}");
}