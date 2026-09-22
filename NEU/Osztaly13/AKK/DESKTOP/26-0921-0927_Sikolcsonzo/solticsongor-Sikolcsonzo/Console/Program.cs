using System.Globalization;
using kolcsonzoLib;

string eszkozFajl = "sporteszkozok.txt";
string foglalasFajl = "foglalasok.txt";
string hibaFajl = "hibalista.txt";

File.WriteAllText(hibaFajl, "");
List<string> hibak = new List<string>();

List<Sporteszkoz> eszkozok = new List<Sporteszkoz>();
string[] eszkozSorok = File.ReadAllLines(eszkozFajl);

for (int i = 1; i < eszkozSorok.Length; i++)
{
    string sor = eszkozSorok[i];
    if (sor.Trim() != "")
    {
        eszkozok.Add(SporteszkozFactory.Factory(sor));
    }
}

Kolcsonzesek kolcsonzo = new Kolcsonzesek(eszkozok);

List<Berles> ervenyesIgenyek = new List<Berles>();
string[] foglalasSorok = File.ReadAllLines(foglalasFajl);

for (int i = 1; i < foglalasSorok.Length; i++)
{
    string sor = foglalasSorok[i];
    if (sor.Trim() == "")
    {
        continue;
    }

    string[] darabok = sor.Split(';');
    DateOnly kezdet = DateOnly.ParseExact(darabok[0].Trim(), ["yyyy.MM.dd", "yyyy.MM.dd.", "yyyy-MM-dd"], CultureInfo.InvariantCulture);
    int napok = int.Parse(darabok[1].Trim());
    string id = darabok[2].Trim();
    string nev = darabok[3].Trim();

    try
    {
        Berles ujBerles = new Berles(id, kezdet, napok, nev);
        ervenyesIgenyek.Add(ujBerles);
    }
    catch (HibasDatumException ex)
    {
        hibak.Add($"{ex.Message}\t{sor.Trim()}");
    }
}

List<string> utkozesiHibak = kolcsonzo.SporteszkozBerles(ervenyesIgenyek);
hibak.AddRange(utkozesiHibak);
File.WriteAllLines(hibaFajl, hibak);

Console.WriteLine("A kölcsönözhető sporteszközök:");
foreach (string leiras in kolcsonzo.KolcsonozhetoEszkozokLeirasai)
{
    Console.WriteLine(leiras);
}

Console.WriteLine();
Console.WriteLine("A sporteszközök sikeres foglalásai:");
foreach (Sporteszkoz eszkoz in kolcsonzo.Eszkozok)
{
    if (eszkoz.Foglalasok.FoglaltIdoszakok.Count > 0)
    {
        Console.WriteLine(eszkoz);
        Console.WriteLine(eszkoz.Foglalasok);
    }
}

Console.WriteLine();

string maiDatumPelda;
try
{
    maiDatumPelda = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy.MM.dd");
}
catch
{
    maiDatumPelda = "2026.09.22";
}

Console.Write($"Add meg a kölcsönzés első napját! (pl: {maiDatumPelda}) ");
string? bemenetDatum = Console.ReadLine();
DateOnly keresettKezdet = DateOnly.ParseExact(bemenetDatum!.Trim(), ["yyyy.MM.dd", "yyyy.MM.dd.", "yyyy-MM-dd"], CultureInfo.InvariantCulture);

DateOnly ma;
try
{
    ma = DateOnly.FromDateTime(DateTime.Now);
}
catch
{
    ma = new DateOnly(2026, 9, 22);
}

if (keresettKezdet < ma)
{
    Console.WriteLine("Időutazó vagy?");
}

Console.Write("Add meg, hány napig kölcsönöznél sílécet vagy snowboardot! ");
int keresettNapok = int.Parse(Console.ReadLine()!.Trim());

Console.WriteLine("A megadott időszakban kölcsönözhető sporteszközök:");
List<Sporteszkoz> szabadok = kolcsonzo.SzabadSporteszkozok(keresettKezdet, keresettNapok);
foreach (Sporteszkoz eszkoz in szabadok)
{
    Console.WriteLine($"{eszkoz.Azonosito} {eszkoz.Leiras}, {eszkoz.Meret} cm");
}