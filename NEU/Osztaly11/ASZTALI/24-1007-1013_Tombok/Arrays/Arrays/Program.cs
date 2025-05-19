#region Basics
Random random = new Random();
#endregion

//|----------------------------------------|

#region 1.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat:");
Console.ResetColor();

int[] osztalyzatok = new int[30];

for (int i = 0; i < osztalyzatok.Length; i++)
{
    osztalyzatok[i] = random.Next(2, 6);
}

Console.WriteLine("Diákok osztályzatai:");
foreach (var osztalyzat in osztalyzatok)
{
    Console.WriteLine(osztalyzat);
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 2.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat:");
Console.ResetColor();
double[] arak = { 1000, 1500, 2000, 2500, 3500 };
double kedvezmeny = 0.15;

Console.WriteLine("Régi és új DVD árak:");
for (int i = 0; i < arak.Length; i++)
{
    double ujAra = arak[i] * (1 - kedvezmeny);
    Console.WriteLine($"Kategória {i + 1}: Régi ár: {arak[i]} Ft, Új ár: {ujAra:F2} Ft");
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 3.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat:");
Console.ResetColor();
int[] dobások = new int[50];

Console.WriteLine("Dobások:");
for (int i = 0; i < dobások.Length; i++)
{
    dobások[i] = random.Next(1, 7);
    Console.Write(dobások[i] + " ");
    if (dobások[i] == 6)
    {
        Console.WriteLine(" - újra dobhat!");
    }
    else
    {
        Console.WriteLine();
    }
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 4.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat:");
Console.ResetColor();
string[] diakok = { "Anna", "Béla", "Cecil", "Dóra", "Elek" };
int sorsoltIndex = random.Next(diakok.Length);

Console.WriteLine($"A mai napon {diakok[sorsoltIndex]} órai munkája lesz osztályozva!");
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 5.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat:");
Console.ResetColor();
string[] valaszok = {
            "Dolgozunk a probléma megoldásán, türelmüket kérjük.", //0
            "Ez a funkció csak a következő verzióban lesz elérhető.", //1
            "Kipróbáltuk, nekünk működik. Kérjük, olvassa el figyelmesebben a dokumentációt!", //2
            "Ez a funkció technikai okok miatt nem megvalósítható.", //3
            "Kérjük, pontosítsa a hibabejelentését, a hibajelenségről küldjön egy képernyő képet is!" //4
        };

int randomIndex = random.Next(valaszok.Length);

Console.WriteLine("Véletlenszerű válasz: " + valaszok[randomIndex]);
randomIndex = random.Next(valaszok.Length);
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 6.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat:");
Console.ResetColor();
Console.Write("Kérem, adja meg az n értékét: ");
Console.ForegroundColor = ConsoleColor.Yellow;
int n = int.Parse(Console.ReadLine()!);
Console.ResetColor();
int[] fibonacci = new int[n];

fibonacci[0] = 0;
if (n > 1) fibonacci[1] = 1;

for (int i = 2; i < n; i++)
{
    fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
}

Console.WriteLine("Az első n Fibonacci szám:");
foreach (var szam in fibonacci)
{
    Console.WriteLine(szam);
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 7.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat:");
Console.ResetColor();
int[] binaris = new int[10];
int[] hatvanyok = new int[10];
int[] szorzatok = new int[10];
int osszeg = 0;

for (int i = 0; i < binaris.Length; i++)
{
    binaris[i] = random.Next(0, 2);
}

for (int i = 0; i < hatvanyok.Length; i++)
{
    hatvanyok[i] = (int)Math.Pow(2, i);
}

for (int i = 0; i < szorzatok.Length; i++)
{
    szorzatok[i] = binaris[i] * hatvanyok[i];
    osszeg += szorzatok[i];
}

Console.WriteLine("A bináris szám: " + string.Join("", binaris));
Console.WriteLine("A decimális érték: " + osszeg);
Console.WriteLine("A hatványok: " + string.Join(", ", hatvanyok));
Console.WriteLine("A szorzatok: " + string.Join(", ", szorzatok));
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 8.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat:");
Console.ResetColor();
double[] sebessegek = { 80, 70, 90, 75, 85, 95 };
double[] ujSebessegek = new double[sebessegek.Length];

Console.WriteLine("Új sebességek:");
for (int i = 0; i < sebessegek.Length; i++)
{
    ujSebessegek[i] = sebessegek[i] * 0.9;
    Console.Write(ujSebessegek[i]);

    if (ujSebessegek[i] > 65)
    {
        Console.WriteLine(" - büntetés");
    }
    else
    {
        Console.WriteLine();
    }
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 9.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat:");
Console.ResetColor();
bool[] rabok = new bool[100];

for (int szolga = 1; szolga <= 100; szolga++)
{
    for (int rab = szolga - 1; rab < 100; rab += szolga)
    {
        rabok[rab] = !rabok[rab];
    }
}

Console.WriteLine("A kiszabadult rabok sorszámai:");
for (int i = 0; i < rabok.Length; i++)
{
    if (rabok[i])
    {
        Console.WriteLine((i + 1) + " ");
    }
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);

#region 10.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat:");
Console.ResetColor();
int[] ePin = new int[6];

for (int i = 0; i < ePin.Length; i++)
{
    ePin[i] = random.Next(0, 10);
}

Console.WriteLine("e-PIN kód:");
for (int i = 0; i < ePin.Length; i++)
{
    Console.WriteLine($"{i + 1}. számjegy: {ePin[i]}");
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n|----------------------------------------|\n");
Console.ResetColor();
Thread.Sleep(2000);