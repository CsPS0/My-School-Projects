using bukkMaratonLib;

var versenyzok = new List<Versenyzo>();
string fileName = "bukkm2019.txt";
string currentPath = AppDomain.CurrentDomain.BaseDirectory;
string filePath = "";

while (!string.IsNullOrEmpty(currentPath))
{
    string checkPath = Path.Combine(currentPath, fileName);
    if (File.Exists(checkPath))
    {
        filePath = checkPath;
        break;
    }
    currentPath = Path.GetDirectoryName(currentPath);
}

if (string.IsNullOrEmpty(filePath))
{
    currentPath = Directory.GetCurrentDirectory();
    while (!string.IsNullOrEmpty(currentPath))
    {
        string checkPath = Path.Combine(currentPath, fileName);
        if (File.Exists(checkPath))
        {
            filePath = checkPath;
            break;
        }
        currentPath = Path.GetDirectoryName(currentPath);
    }
}

if (string.IsNullOrEmpty(filePath))
{
    return;
}

try
{
    var sorok = File.ReadAllLines(filePath);
    for (int i = 1; i < sorok.Length; i++)
    {
        versenyzok.Add(new Versenyzo(sorok[i]));
    }
}
catch
{
    return;
}

int osszesIndulo = 691;
double nemTeljesitokAranya = (double)(osszesIndulo - versenyzok.Count) / osszesIndulo * 100;
Console.WriteLine($"4. feladat: Versenytávot nem teljesítők: {nemTeljesitokAranya}%");

int noiRovidtav = versenyzok.Count(v => v.Versenytav.Tav == "Rövid" && v.Kategoria.EndsWith("n"));
Console.WriteLine($"5. feladat: Női versenyzők száma a rövid távú versenyen: {noiRovidtav}fő");

bool voltHathoras = versenyzok.Any(v => v.Idotartam.TotalHours > 6);
Console.WriteLine($"6. feladat: {(voltHathoras ? "Volt ilyen versenyző" : "Nem volt ilyen versenyző")}");

var ffWinner = versenyzok
    .Where(v => v.Versenytav.Tav == "Rövid" && v.Kategoria == "ff")
    .OrderBy(v => v.Idotartam)
    .FirstOrDefault();

if (ffWinner != null)
{
    Console.WriteLine("7. feladat: A felnőtt férfi (ff) kategória győztese rövid távon");
    Console.WriteLine($"\tRajtszám: {ffWinner.Rajtszam}");
    Console.WriteLine($"\tNév: {ffWinner.Nev}");
    Console.WriteLine($"\tEgyesület: {(string.IsNullOrEmpty(ffWinner.Egyesulet) ? "nincs" : ffWinner.Egyesulet)}");
    Console.WriteLine($"\tIdő: {ffWinner.Ido}");
}

Console.WriteLine("8. feladat: Statisztika");
var statisztika = versenyzok
    .Where(v => v.Kategoria.EndsWith("f"))
    .GroupBy(v => v.Kategoria)
    .Select(g => new { Kategoria = g.Key, Count = g.Count() });

foreach (var item in statisztika)
{
    Console.WriteLine($"\t{item.Kategoria} - {item.Count}fő");
}