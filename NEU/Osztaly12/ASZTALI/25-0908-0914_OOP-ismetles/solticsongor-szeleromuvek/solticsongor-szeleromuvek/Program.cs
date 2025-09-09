using szeleromuvekLib;

Szeleromuvek szeleromuvek = new(File.ReadAllLines("szeleromuvek.csv").Skip(1));

// 1. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat: ");
Console.ResetColor();
Console.WriteLine($"A beolvasott adatok száma: {szeleromuvek.Osszes}");

// 2. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n2. feladat: ");
Console.ResetColor();
Console.WriteLine("Régiónként a szélerőmű telepítések száma:");
foreach (var i in szeleromuvek.TelepitesekSzamaRegonkent())
{
    Console.WriteLine(i);
}

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n3. feladat: ");
Console.ResetColor();
Console.WriteLine(szeleromuvek.LegtobbTelepitesRegio());

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n4. feladat: ");
Console.ResetColor();
Console.WriteLine("Régiónként a szélerőművek száma:");
foreach (var i in szeleromuvek.SzeleromuvekSzamaRegonkent())
{
    Console.WriteLine(i);
}

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n5. feladat: ");
Console.ResetColor();
Console.WriteLine("Az egyes régiókban megyénként telepített szélerőművek száma:");
foreach (var i in szeleromuvek.SzeleromuvekSzamaMegyeenkent())
{
    Console.WriteLine(i);
}

// 6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n6. feladat: ");
Console.ResetColor();
Console.WriteLine("Településenként a szélerőművek száma:");
foreach (var i in szeleromuvek.SzeleromuvekSzamaTelepulesenkent())
{
    Console.WriteLine(i);
}

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n7. feladat: ");
Console.ResetColor();
Console.WriteLine("A 3 legtöbb szélerőművet tartalmazó település:");
foreach (var i in szeleromuvek.LegtobbSzeleromuTelepules(3))
{
    Console.WriteLine(i);
}

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n8. feladat: ");
Console.ResetColor();
Console.WriteLine("A szélerőművek átlagos teljesítménye településenként:");
foreach (var i in szeleromuvek.AtlagosTeljesitmenyTelepulesenkent())
{
    Console.WriteLine(i);
}

// 9. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n9. feladat: ");
Console.ResetColor();
Console.WriteLine("Az 5 legnagyobb összteljesítményű szélerőművekkel rendelkező település:");
foreach (var i in szeleromuvek.LegnagyobbOsszteljesitmenyuTelepulesek(5))
{
    Console.WriteLine(i);
}