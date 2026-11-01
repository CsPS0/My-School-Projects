using brainrotLib;

Console.WriteLine("=== BRAINROT PROGRAM ===");
Console.WriteLine();

var factory = new BrainrotFactory();
var output = new BrainrotOutput(factory);

// 1./2.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n1./2. feladat");
Console.ResetColor();

Console.Write("Add meg a mátrix méretét (N): ");
Console.ForegroundColor = ConsoleColor.Yellow;
if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
{
    n = 5;
    Console.WriteLine($"Érvénytelen méret, alapértelmezett {n}x{n} mátrix használata.");
}
Console.ResetColor();

output.CreateMatrix(n);
output.DisplayMatrix();

Console.WriteLine("=== STATISZTIKÁK ===");

// 3.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();
output.DisplayTotalCount();
Console.WriteLine();

// 4.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();
output.DisplayGroupByName();

// 5.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();
output.DisplayGroupByType();

// 6.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();
output.DisplayMostAndLeastCommon();

// 7.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();
output.DisplayDetailedDescriptions();

// 8.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat");
Console.ResetColor();
output.DisplayChronologicalStats();

// 9.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat");
Console.ResetColor();
output.DisplaySpecialStats();