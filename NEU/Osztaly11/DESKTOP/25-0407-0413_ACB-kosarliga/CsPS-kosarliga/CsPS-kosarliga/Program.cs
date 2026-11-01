using kosarligaLib;

Ligak ligak = new(File.ReadAllLines("eredmenyek.csv").Skip(1));

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("4. feladat: ");
Console.ResetColor();
Console.Write("Csapat neve: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekertCsapat = Console.ReadLine();
Console.ResetColor();
Console.WriteLine($"\tHazai mérkőzések száma: {ligak.HazaiMecsek(bekertCsapat)}\n\tIdegen mérkőzések száma: {ligak.IdegenMecsek(bekertCsapat)}");

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("5. feladat: ");
Console.ResetColor();
Console.WriteLine(ligak.Dontetlen());

// 6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("6. feladat: ");
Console.ResetColor();
Console.Write("A város neve: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekertVaros = Console.ReadLine();
Console.ResetColor();
Console.WriteLine($"A csapat neve: {ligak.Varos(bekertVaros)}");

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("7. feladat: ");
Console.ResetColor();
Console.WriteLine($"100 pont felett dobtak hazai meccsen:\n\t{string.Join("\n\t", ligak.TobbMintSzaz())}");

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("8. feladat: ");
Console.ResetColor();
Console.Write("Az időpont (pl.: 2007.11.27): ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekertIdopont = Console.ReadLine();
Console.ResetColor();
Console.WriteLine(ligak.ByDate(bekertIdopont));

// 9. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("9. feladat: ");
Console.ResetColor();
Console.WriteLine($"A legnagyobb pontkülönbségű mérkőzés adatai:\n\t{ligak.LegnagyobbKulonbseg()}");

// 10. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("10. feladat: ");
Console.ResetColor();
Console.WriteLine($"Stadionok, ahol 20-nál több mérkőzést játszottak:\n\t{ligak.Stadionok()}");