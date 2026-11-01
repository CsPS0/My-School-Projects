using ultrabalatonLib;

Balatonok balatonok = new(File.ReadAllLines("ub2017egyeni.txt").Skip(1));

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("3. feladat: ");
Console.ResetColor();
Console.WriteLine($"Egyéni indulók: {balatonok.Ossz} fő");

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("4. feladat: ");
Console.ResetColor();
Console.WriteLine($"Célba érkező női sportolók: {balatonok.Noi} fő");

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("5. feladat: ");
Console.ResetColor();
Console.Write("Kérem a sportoló nevét: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekertNev = Console.ReadLine();
Console.ResetColor();

if (balatonok.Indult(bekertNev))
{
    Console.WriteLine("\tIndult a versenyen.");
    Console.WriteLine(balatonok.Teljesitette(bekertNev) ? "\tTeljesítette a teljes távot? Igen" : "\tTeljesítette a teljes távot? Nem");
}
else
{
    Console.WriteLine("\tNem indult a versenyen.");
}

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("7. feladat: ");
Console.ResetColor();
Console.WriteLine($"Átlagos idő férfiaknak: {balatonok.FerfiAtlagido():0.00} óra");

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("8. feladat: ");
Console.ResetColor();
var noiGyoztes = balatonok.NoiGyoztes();
Console.WriteLine($"Verseny győztesei\n\tNők: {noiGyoztes.nev} ({noiGyoztes.rajtszam}) - {noiGyoztes.ido}");
var ferfiGyoztes = balatonok.FerfiGyoztes();
Console.WriteLine($"\tFérfiak: {ferfiGyoztes.nev} ({ferfiGyoztes.rajtszam}) - {ferfiGyoztes.ido}");