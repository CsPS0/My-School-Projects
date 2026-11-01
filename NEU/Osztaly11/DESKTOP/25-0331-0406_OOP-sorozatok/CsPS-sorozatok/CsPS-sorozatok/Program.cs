using SorozatokLib;

Sorozatok sorozatok = new(File.ReadAllLines("sorozatok.txt").Skip(1));

//3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat:");
Console.ResetColor();
Console.WriteLine($"Összesen {sorozatok.SorozatokSzama} sorozat adatait olvastam be.");

//5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat:");
Console.ResetColor();
Console.WriteLine(sorozatok.EgyVagyKetto());

//6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat:");
Console.ResetColor();
Console.WriteLine($"A XX. században indult sorozatok átlagosan {sorozatok.HuszadikElott.ToString("F1")} évadot értek meg.");

//7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat:");
Console.ResetColor();
Console.WriteLine($"A(z) {sorozatok.LegKevesebbKat} kategóriából van a legkevesebb.");

//8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat:");
Console.ResetColor();
Console.Write("Melyik országból származó filmekre kíváncsi?: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string melyikOrszag = Console.ReadLine();
Console.ResetColor();
sorozatok.FajlIr(melyikOrszag);