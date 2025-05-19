using System.ComponentModel.Design;

// 1. Feladat: Téglalap kerülete és területe
Console.WriteLine("1. Feladat:");
Console.Write("Add meg az 'a' oldalt: ");
double a = double.Parse(Console.ReadLine());
Console.Write("Add meg a 'b' oldalt: ");
double b = double.Parse(Console.ReadLine());
if (a > 0 && b > 0)
{
    Console.WriteLine("Téglalap kerülete: " + (2 * (a + b)));
    Console.WriteLine("Téglalap területe: " + (a * b));
}
else
{
    Console.WriteLine("A számok nem megfelelőek!");
}

// 2. Feladat: Számolások két számmal
Console.WriteLine("\n2. Feladat:");
Console.Write("Első szám: ");
int num1 = int.Parse(Console.ReadLine());
Console.Write("Második szám: ");
int num2 = int.Parse(Console.ReadLine());
Console.WriteLine("Összeg: " + (num1 + num2));
Console.WriteLine("Különbség: " + (num1 - num2));
Console.WriteLine("Szorzat: " + (num1 * num2));
if (num2 != 0)
{
    Console.WriteLine("Hányados: " + ((double)num1 / num2));
}
else
{
    Console.WriteLine("Nullával nem lehet osztani!");
}

// 3. Feladat: Üdvözlés születési év alapján
Console.WriteLine("\n3. Feladat:");
Console.Write("Neved: ");
string name = Console.ReadLine();
Console.Write("Születési év: ");
int birthYear = int.Parse(Console.ReadLine());
if (birthYear < 2000)
{
    Console.WriteLine("Jó napot " + name + "!");
}
else
{
    Console.WriteLine("Szia " + name + "!");
}

// 4. Feladat: Ajándékvásárlás
Console.WriteLine("\n4. Feladat:");
Console.Write("Első ajándék ára: ");
int gift1 = int.Parse(Console.ReadLine());
Console.Write("Második ajándék ára: ");
int gift2 = int.Parse(Console.ReadLine());
Console.Write("Harmadik ajándék ára: ");
int gift3 = int.Parse(Console.ReadLine());
int total = gift1 + gift2 + gift3;
if (total <= 20000)
{
    Console.WriteLine("Még maradt " + (20000 - total) + " Ft.");
}
else
{
    Console.WriteLine("Túllépted a keretet " + (total - 20000) + " Ft-tal.");
}

// 5. Feladat: Pénzfeldobás
Console.WriteLine("\n5. Feladat:");
Random rand = new Random();
if (rand.Next(2) == 0)
{
    Console.WriteLine("Fej");
}
else
{
    Console.WriteLine("Írás");
}

// 6. Feladat: Páros vagy páratlan
Console.WriteLine("\n6. Feladat:");
Console.Write("Adj meg egy számot: ");
int szam = int.Parse(Console.ReadLine());
if (szam % 2 == 0)
{
    Console.WriteLine("A szám páros.");
}
else
{
    Console.WriteLine("A szám páratlan.");
}

// 7. Feladat: Ötöst szeretnél?
Console.WriteLine("\n7. Feladat:");
Console.Write("Szeretnél ötöst programozásból (igen/nem)? ");
string valasz = Console.ReadLine();
if (valasz == "igen")
{
    Console.WriteLine("Akkor gyakorolj!");
}
else
{
    Console.WriteLine("Helytelen hozzáállás!");
}

// 8. Feladat: Szám előjele
Console.WriteLine("\n8. Feladat:");
Console.Write("Adj meg egy számot: ");
int elojelSzam = int.Parse(Console.ReadLine());
if (elojelSzam > 0)
{
    Console.WriteLine("Pozitív");
}
else if (elojelSzam < 0)
{
    Console.WriteLine("Negatív");
}
else
{
    Console.WriteLine("Nulla");
}

// 9. Feladat: Hónap és évszak
Console.WriteLine("\n9. Feladat:");
Console.Write("Add meg a hónap sorszámát (1-12): ");
int honap = int.Parse(Console.ReadLine());
if (honap == 12 || honap == 1 || honap == 2)
{
    Console.WriteLine("Tél");
}
else if (honap >= 3 && honap <= 5)
{
    Console.WriteLine("Tavasz");
}
else if (honap >= 6 && honap <= 8)
{
    Console.WriteLine("Nyár");
}
else if (honap >= 9 && honap <= 11)
{
    Console.WriteLine("Ősz");
}
else
{
    Console.WriteLine("Érvénytelen hónap.");
}

// 10. Feladat: Szökőév
Console.WriteLine("\n10. Feladat:");
Console.Write("Adj meg egy évet: ");
int evszam = int.Parse(Console.ReadLine());
if ((evszam % 4 == 0 && evszam % 100 != 0) || evszam % 400 == 0)
{
    Console.WriteLine("Szökőév");
}
else
{
    Console.WriteLine("Nem szökőév");
}

// 11. Feladat: Legnagyobb szám
Console.WriteLine("\n11. Feladat:");
Console.Write("Adj meg 3 számot: ");
int n1 = int.Parse(Console.ReadLine());
int n2 = int.Parse(Console.ReadLine());
int n3 = int.Parse(Console.ReadLine());
int legnagyobb = n1;
if (n2 > legnagyobb)
{
    legnagyobb = n2;
}
if (n3 > legnagyobb)
{
    legnagyobb = n3;
}
Console.WriteLine("A legnagyobb szám: " + legnagyobb);

// 12. Feladat: Leárazás
Console.WriteLine("\n12. Feladat:");
Console.Write("Add meg az eredeti árat: ");
double ar = double.Parse(Console.ReadLine());
Console.Write("Add meg a leárazás %-át: ");
double learazas = double.Parse(Console.ReadLine());
double akciosAr = ar * (1 - learazas / 100);
Console.WriteLine("Akciós ár: " + akciosAr);
if (learazas > 50)
{
    Console.WriteLine("Megéri megvenni!");
}
else
{
    Console.WriteLine("Nem éri meg megvenni!");
}

// 13. Feladat: Víz hőmérséklete
Console.WriteLine("\n13. Feladat:");
Console.Write("Add meg a víz hőmérsékletét: ");
double temp = double.Parse(Console.ReadLine());
if (temp <= 0)
{
    Console.WriteLine("Jég");
}
else if (temp >= 100)
{
    Console.WriteLine("Gőz");
}
else
{
    Console.WriteLine("Folyékony");
}

// 14. Feladat: Számok rendezése
Console.WriteLine("\n14. Feladat:");
Console.Write("Adj meg 3 számot: ");
int sz1 = int.Parse(Console.ReadLine());
int sz2 = int.Parse(Console.ReadLine());
int sz3 = int.Parse(Console.ReadLine());
if (sz1 > sz2) { int tempSzam = sz1; sz1 = sz2; sz2 = tempSzam; }
if (sz2 > sz3) { int tempSzam = sz2; sz2 = sz3; sz3 = tempSzam; }
if (sz1 > sz2) { int tempSzam = sz1; sz1 = sz2; sz2 = tempSzam; }
Console.WriteLine("Növekvő sorrend: " + sz1 + ", " + sz2 + ", " + sz3);

// 15. Feladat: Vércsoport
Console.WriteLine("\n15. Feladat:");
Console.Write("Add meg a vércsoportodat (A, B, AB, O): ");
string vercsoport = Console.ReadLine().ToUpper();
if (vercsoport == "A" || vercsoport == "B" || vercsoport == "AB" || vercsoport == "O")
{
    Console.WriteLine(vercsoport + " vércsoportod van.");
}
else
{
    Console.WriteLine("Ismeretlen vércsoport.");
}