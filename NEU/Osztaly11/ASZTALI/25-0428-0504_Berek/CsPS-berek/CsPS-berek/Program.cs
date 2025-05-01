using BerekLib;

Berek berek = new(File.ReadAllLines("berek2020.txt").Skip(1));

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("3. feladat: ");
Console.ResetColor();
Console.WriteLine($"Dolgozók száma: {berek.Osszes} fő");

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("4. feladat: ");
Console.ResetColor();
Console.WriteLine($"Bérek átlaga: {berek.Atlagber} eFt");

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("5. feladat: ");
Console.ResetColor();
Console.WriteLine($"Részlegek: {berek.Reszlegek}");

// 6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("6. feladat: ");
Console.ResetColor();
Console.Write("Kérem egy részleg nevét: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekeres = Console.ReadLine() ?? "";
Console.ResetColor();

var legtobbetKereso = berek.LegtobbetKereso(bekeres);
if (legtobbetKereso == null)
{
    Console.WriteLine($"A megadott részleg ({bekeres}) nem létezik a cégnél!");
}
else
{
    Console.WriteLine($"\tA legtöbbet kereső dolgozó a megadott részlegen ({bekeres}):" +
        $"\n\tNév: {legtobbetKereso.Nev}" +
        $"\n\tNeme: {legtobbetKereso.Neme}" +
        $"\n\tBelépés: {legtobbetKereso.Belepesev}" +
        $"\n\tBér: {legtobbetKereso.BerFt:N0} Ft");
}

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("7. feladat: ");
Console.ResetColor();
Console.WriteLine("Részlegenként dolgozók száma:");
foreach (var adat in berek.ReszlegenkentDolgozokSzama())
{
    Console.WriteLine($"\t{adat}");
}

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("8. feladat: ");
Console.ResetColor();
Console.WriteLine("Részlegenként a legkisebb bérű dolgozó:");
foreach (var adat in berek.LegkisebbBeruek())
{
    Console.WriteLine($"\t{adat}");
}

// 9. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("9. feladat: ");
Console.ResetColor();
Console.WriteLine($"A női és férfi dolgozók keresztnevei:" +
    $"\n\tNők: {berek.KeresztnevekNemenként("nő")}" +
    $"\n\tFérfiak: {berek.KeresztnevekNemenként("férfi")}");

// 10. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("10. feladat: ");
Console.ResetColor();
Console.WriteLine("Részlegenként dolgozók nemek szerint:");
foreach (var adat in berek.ReszlegNemStatisztika())
{
    Console.WriteLine($"\t{adat}");
}

// 11. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("11. feladat: ");
Console.ResetColor();
Console.WriteLine("Részlegenként az átlag fizetés:");
foreach (var adat in berek.AtlagBerReszlegenkent())
{
    Console.WriteLine($"\t{adat}");
}

// 12. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("12. feladat: ");
Console.ResetColor();
Console.WriteLine("Jubileumi dolgozók száma évforduló szerint:");
foreach (var evfordulo in berek.Jubileum())
{
    Console.WriteLine($"\t{evfordulo.Key} éves: {evfordulo.Value} fő");
}