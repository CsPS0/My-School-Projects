using GepkocsiLib;

Gepkocsik gepkocsik = new(File.ReadAllLines("autok.txt").Skip(1));

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat:");
Console.ResetColor();
Console.WriteLine($"Összesen {gepkocsik.Gepkocsikszama} db gépkocsi adatait olvastam be.");

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat:");
Console.ResetColor();
Console.WriteLine(gepkocsik.Tobb());

// 6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat:");
Console.ResetColor();
Console.WriteLine($"A budapesti gépkocsik átlagos hengerűrtartalma {gepkocsik.Atlag:F2} liter.");

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat:");
Console.ResetColor();
Console.WriteLine($"A legkevesebb gyártmányú autó: {gepkocsik.Legkevesebb}");

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat:");
Console.ResetColor();
Console.Write("Melyik gyártmányú autókat írjam fájlba?: ");
string gyartmanyBe = Console.ReadLine();
gepkocsik.FajlIr(gyartmanyBe);