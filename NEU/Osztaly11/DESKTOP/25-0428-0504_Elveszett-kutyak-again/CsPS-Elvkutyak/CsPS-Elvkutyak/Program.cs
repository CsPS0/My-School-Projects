using KutyakLib;

Kutyak kutyak = new(File.ReadAllLines("kutyak.csv").Skip(1));

// 1. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("1. feladat: ");
Console.ResetColor();
Console.WriteLine($"Összesen {kutyak.Ossz} db kutya van bejelentve.");

// 2. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("2. feladat: ");
Console.ResetColor();
Console.WriteLine($"Lista azokról a kutyákról, melyek 2020.12.31-én elvesztek:" +
    $"\n\t" +
    $"{kutyak.Elveszettek}");

// 3. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("3. feladat: ");
Console.ResetColor();
Console.WriteLine($"2020-ban {kutyak.Ev2020} kutya eltűnését jelentették, míg 2021-ben {kutyak.Ev2021} kutyát.");

// 4. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("4. feladat: ");
Console.ResetColor();
Console.WriteLine($"A kutyák fajtái: {kutyak.FajtakNevsor}");

// 5. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("5. feladat: ");
Console.ResetColor();
Console.WriteLine($"Ezek a kutyanevek szerepelnek többször: " +
    $"\n\t" +
    $"{kutyak.TobbszorosNevek}");

// 6. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("6. feladat: ");
Console.ResetColor();
Console.WriteLine($"Szukák száma: {kutyak.SzukakSzama}, Kanok száma: {kutyak.KanokSzama}, Nem ismert: {kutyak.IsmeretlenNem}");

// 7. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("7. feladat: ");
Console.ResetColor();
Console.WriteLine($"Ezekből a Budapesti kerületekből történt bejelentések száma:" +
    $"\n\t" +
    $"{kutyak.KeruletiStatisztika}");

// 8. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("8. feladat: ");
Console.ResetColor();
Console.WriteLine($"Bejelentések száma melyből legalább 5 kutya fajtánként:" +
    $"\n\t" +
    $"{kutyak.FajtakLegalabb5Kutya}");

// 9. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("9. feladat: ");
Console.ResetColor();
Console.WriteLine($"Havonta {kutyak.HaviEltunesek} kutyát jelentenek be.");

// 10. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("10. feladat: ");
Console.ResetColor();
Console.WriteLine($"Helyenként ennyi kan, szuka, n.a. volt bejelentve:" +
    $"\n\t" +
    $"{kutyak.HelyNemStatisztika}");

// 11. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("11. feladat: ");
Console.ResetColor();
Console.WriteLine($"Fajonként ezekről a településekről tűntek el az adott fajtájú kutyák:" +
    $"\n\t" +
    $"{kutyak.FajtaHelyek}");

// 12. feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("12. feladat: ");
Console.ResetColor();
Console.WriteLine($"Ezeket a neveket adták, ezeknek a fajtáknak:" +
    $"\n\t" +
    $"{kutyak.NevFajtak}");