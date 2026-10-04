using cukraszdaLib;

// 12.a
var keszitesAdatLista = File.ReadAllLines("keszites.txt")
    .Skip(1)
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => new KeszitesAdat(line))
    .ToList();

var keszitesAdatok = new KeszitesAdatok(keszitesAdatLista);

// 12.b
Console.WriteLine($"Elérhető sütemény készítési azonosítók: {string.Join("; ", keszitesAdatok.ElerhetoKeszitesAzonositok)}");
Console.WriteLine();

// 12.c
var sutemenyLista = File.ReadAllLines("sutemenyek.txt")
    .Skip(1)
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => SutemenyFactory.Factory(line, keszitesAdatok))
    .ToList();

var sutemenyek = new Sutemenyek(sutemenyLista);

// 12.d
Console.WriteLine("A készíthető sütemények:");
foreach (var sutemeny in sutemenyek.SutemenyTipusok)
{
    Console.WriteLine($"    {sutemeny}");
}
Console.WriteLine();

// 12.e
File.WriteAllText("hibalista.txt", string.Empty);
var hibalista = new List<string>();
var feladatok = new Feladatok();

foreach (var sor in File.ReadAllLines("feladatok.txt").Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)))
{
    var adatok = sor.Split(';');
    string sutiAzonosito = adatok[0].Trim();
    int adag = int.Parse(adatok[1].Trim());

    var sutemeny = sutemenyek.SutemenyTipusok.FirstOrDefault(s => s.Azonosito == sutiAzonosito);
    if (sutemeny == null)
    {
        hibalista.Add($"{sutiAzonosito}: Ilyen azonosítójú süteményt nem készítenek.");
        continue;
    }

    try
    {
        feladatok += new Feladat(sutemeny, adag);
    }
    catch (TulSokFeladatException ex)
    {
        hibalista.Add($"{ex.Message} - {adag} adag {sutemeny.Megnevezes}: {sutemeny.ElkeszitesiIdo * adag} perc");
    }
}

File.WriteAllLines("hibalista.txt", hibalista);

// 12.f
Console.WriteLine("Az elvégzendő feladatok:");
foreach (var feladat in feladatok.FeladatLista)
{
    Console.WriteLine($"    {feladat}");
}
Console.WriteLine();

// 12.g
Console.WriteLine("Süteményenként az elkészítendő adagok száma:");
var osszesites = feladatok.FeladatLista
    .GroupBy(f => f.Sutemeny.Megnevezes);

foreach (var csoport in osszesites)
{
    Console.WriteLine($"    {csoport.Key}: {csoport.Sum(f => f.Adag)} adag");
}
