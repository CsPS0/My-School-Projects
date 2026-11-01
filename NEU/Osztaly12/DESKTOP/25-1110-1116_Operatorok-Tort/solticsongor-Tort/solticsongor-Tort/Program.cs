using solticsongor_Tort;

// Példányosítás
Tort t1 = new Tort(1, 2);
Console.WriteLine($"t1 = {t1}");

Tort t2 = new Tort(2, 4);
Console.WriteLine($"t2 (egyszerűsítve) = {t2}");

Tort t3 = new Tort(3, 1);
Console.WriteLine($"t3 = {t3}");

// Alap konstruktor
Tort t4 = new Tort();
Console.WriteLine($"t4 (alap) = {t4}");

// Negatív számok kezelése
Tort t5 = new Tort(-1, 2);
Console.WriteLine($"t5 = {t5}");

Tort t6 = new Tort(1, -2);
Console.WriteLine($"t6 (nevező negatív) = {t6}");

Tort t7 = new Tort(-1, -2);
Console.WriteLine($"t7 (mindkettő negatív) = {t7}");

// Tizedes tört tulajdonság
Console.WriteLine($"t1 tizedes törtben: {t1.TizedesTort}");

// Műveletek törtekkel
Console.WriteLine($"t1 + t2 = {t1 + t2}");
Console.WriteLine($"t1 - t2 = {t1 - t2}");
Console.WriteLine($"t1 * t2 = {t1 * t2}");
Console.WriteLine($"t1 / t2 = {t1 / t2}");

// Műveletek egész számmal
Console.WriteLine($"t1 + 1 = {t1 + 1}");
Console.WriteLine($"t1 - 1 = {t1 - 1}");
Console.WriteLine($"t1 * 2 = {t1 * 2}");
Console.WriteLine($"t1 / 2 = {t1 / 2}");

// Összehasonlítás
Console.WriteLine($"t1 == t2: {t1 == t2}");
Console.WriteLine($"t1 != t3: {t1 != t3}");
Console.WriteLine($"t1 < t3: {t1 < t3}");
Console.WriteLine($"t1 > t3: {t1 > t3}");

// Konverziók
Tort t8 = 5;
Console.WriteLine($"Implicit konverzió int-ből: t8 = {t8}");

Tort t9 = 0.75;
Console.WriteLine($"Implicit konverzió double-ból: t9 = {t9}");

int egesz = (int)new Tort(7, 2);
Console.WriteLine($"Explicit konverzió int-be (7/2): {egesz}");

egesz = (int)new Tort(3, 4);
Console.WriteLine($"Explicit konverzió int-be (3/4): {egesz}");

try
{
    new Tort(1, 0);
}
catch (ArgumentException e)
{
    Console.WriteLine($"Hiba elfogva: {e.Message}");
}

try
{
    var eredmeny = t1 / 0;
}
catch (DivideByZeroException e)
{
    Console.WriteLine($"Hiba elfogva: {e.Message}");
}