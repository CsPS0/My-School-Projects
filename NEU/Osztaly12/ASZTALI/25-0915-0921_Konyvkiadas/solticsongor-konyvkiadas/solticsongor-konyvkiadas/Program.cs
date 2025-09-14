using konyvkiadasLib;

Konyvek konyvek = new(File.ReadAllLines("kiadas.txt"));

// 2. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat: ");
Console.ResetColor();
Console.Write("Szerző: ");
Console.ForegroundColor= ConsoleColor.Yellow;
string szerzoBekeres = Console.ReadLine() ?? "";
Console.ResetColor();
int szerzoAlkalom = konyvek.BekeresSzerzoAlkalom(szerzoBekeres);
if (szerzoAlkalom == 0)
{
    Console.WriteLine("Nem adtak ki");
}
else
{
    Console.WriteLine($"{szerzoAlkalom} könyvkiadás");
}

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n3. feladat: ");
Console.ResetColor();
Console.WriteLine(konyvek.LegnagyobbPeldanySzam());

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n4. feladat: ");
Console.ResetColor();
Console.WriteLine(konyvek.Legalabb40KPeldany());

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n5. feladat: ");
Console.ResetColor();
Console.WriteLine($"Legalább kétszer, nagyobb példányszámban újra kiadott könyvek:\n{konyvek.LegalabbKetszerNagyobbPeldany()}");