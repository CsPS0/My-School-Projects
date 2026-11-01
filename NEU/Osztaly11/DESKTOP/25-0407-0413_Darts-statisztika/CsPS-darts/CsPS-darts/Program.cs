using dartsLib;

Darts darts = new(File.ReadAllLines("dobasok.txt").Skip(0));

// 2. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();
Console.WriteLine($"Körök száma: {darts.Ossz}");
Console.WriteLine();
// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();
Console.WriteLine($"3. dobásra Bullseye: {darts.Bullseye}");
Console.WriteLine();
// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();
Console.Write("Adja meg a szektor értékét! Szektor= ");
Console.ForegroundColor = ConsoleColor.Yellow;
string bekertSzektor = Console.ReadLine();
Console.ResetColor();
Console.WriteLine(darts.Szektor2(bekertSzektor));
// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();
Console.WriteLine(darts.Legmagasabb());