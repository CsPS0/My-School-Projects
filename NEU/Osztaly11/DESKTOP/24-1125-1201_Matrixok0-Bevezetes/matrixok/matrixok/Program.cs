// mivel tanultok már a metódusokat, ezért a pause is metóduként van használva (lásd kód alján)

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

Console.Write("Adja meg a mátrix sorainak számát: ");
Console.ForegroundColor= ConsoleColor.Yellow;
int sorokSzama = int.Parse(Console.ReadLine());
Console.ResetColor();
Console.Write("Adja meg a mátrix oszlopainak számát: ");
Console.ForegroundColor = ConsoleColor.Yellow;
int oszlopokSzama = int.Parse(Console.ReadLine());
Console.ResetColor();
int[,] matrixTomb = new int[sorokSzama, oszlopokSzama];
Random veletlen = new Random();

for (int sorIndex = 0; sorIndex < sorokSzama; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < oszlopokSzama; oszlopIndex++)
    {
        matrixTomb[sorIndex, oszlopIndex] = veletlen.Next(0, 101);
        Console.Write(matrixTomb[sorIndex, oszlopIndex] + "\t");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

string[,] nevekMatrix =
{
            { "Anna", "Béla", "Cecil" },
            { "Dávid", "Emma", "Ferenc" },
            { "Géza", "Hanna", "Irma" }
        };

for (int sorIndex = 0; sorIndex < 3; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < 3; oszlopIndex++)
    {
        Console.WriteLine($"{sorIndex + 1}. sor {oszlopIndex + 1}. elem: {nevekMatrix[sorIndex, oszlopIndex]}");
    }
}

Console.WriteLine("\nMátrix formában:");
for (int sorIndex = 0; sorIndex < 3; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < 3; oszlopIndex++)
    {
        Console.Write(nevekMatrix[sorIndex, oszlopIndex] + "\t");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

int[][] csonkaTomb = new int[6][];
for (int sorIndex = 0; sorIndex < 6; sorIndex++)
{
    int veletlenHossz = veletlen.Next(3, 10);
    csonkaTomb[sorIndex] = new int[veletlenHossz];
    for (int oszlopIndex = 0; oszlopIndex < veletlenHossz; oszlopIndex++)
    {
        csonkaTomb[sorIndex][oszlopIndex] = veletlen.Next(0, 10);
        Console.Write(csonkaTomb[sorIndex][oszlopIndex] + " ");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

int[][] inicializaltCsonkaTomb =
{
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6, 7 },
            new int[] { 8, 9 },
            new int[] { 10, 11, 12, 13, 14 },
            new int[] { 15 },
            new int[] { 16, 17, 18, 19 }
        };

for (int sorIndex = 0; sorIndex < inicializaltCsonkaTomb.Length; sorIndex++)
{
    Console.Write($"Sor {sorIndex + 1}: ");
    for (int oszlopIndex = 0; oszlopIndex < inicializaltCsonkaTomb[sorIndex].Length; oszlopIndex++)
    {
        Console.Write(inicializaltCsonkaTomb[sorIndex][oszlopIndex] + " ");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

Console.Write("Adja meg az egységmátrix méretét: ");
Console.ForegroundColor = ConsoleColor.Yellow;
int meret = int.Parse(Console.ReadLine());
Console.ResetColor();
int[,] egysegesMatrix = new int[meret, meret];

for (int sorIndex = 0; sorIndex < meret; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < meret; oszlopIndex++)
    {
        egysegesMatrix[sorIndex, oszlopIndex] = (sorIndex == oszlopIndex) ? 1 : 0;
        Console.Write(egysegesMatrix[sorIndex, oszlopIndex] + "\t");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

Console.Write("Adja meg a mátrix sorainak számát: ");
Console.ForegroundColor = ConsoleColor.Yellow;
int sorok = int.Parse(Console.ReadLine());
Console.ResetColor();
Console.Write("Adja meg a mátrix oszlopainak számát: ");
Console.ForegroundColor = ConsoleColor.Yellow;
int oszlopok = int.Parse(Console.ReadLine());
Console.ResetColor();
int[,] sorfolytonosMatrix = new int[sorok, oszlopok];
int szamlalo = 0;

for (int sorIndex = 0; sorIndex < sorok; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < oszlopok; oszlopIndex++)
    {
        sorfolytonosMatrix[sorIndex, oszlopIndex] = szamlalo++;
        Console.Write(sorfolytonosMatrix[sorIndex, oszlopIndex] + "\t");
    }
    Console.WriteLine();
}
#endregion

Pause();

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

Console.Write("Adja meg az n értékét (a mátrix mérete n*n): ");
Console.ForegroundColor = ConsoleColor.Yellow;
int n = int.Parse(Console.ReadLine());
Console.ResetColor();
int[,] veletlenMatrix = new int[n, n];

Console.WriteLine("\nA generált mátrix:");
for (int sorIndex = 0; sorIndex < n; sorIndex++)
{
    for (int oszlopIndex = 0; oszlopIndex < n; oszlopIndex++)
    {
        veletlenMatrix[sorIndex, oszlopIndex] = veletlen.Next(0, 101);
        Console.Write(veletlenMatrix[sorIndex, oszlopIndex] + "\t");
    }
    Console.WriteLine();
}

// a) Főátló elemei
Console.WriteLine("\nFőátló elemei:");
for (int i = 0; i < n; i++)
{
    Console.Write(veletlenMatrix[i, i] + " ");
}
Console.WriteLine();

// b) Mellékátló elemei
Console.WriteLine("\nMellékátló elemei:");
for (int i = 0; i < n; i++)
{
    Console.Write(veletlenMatrix[i, n - i - 1] + " ");
}
Console.WriteLine();

// c) Főátló feletti háromszög
Console.WriteLine("\nFőátló feletti háromszög elemei:");
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (j > i)
        {
            Console.Write(veletlenMatrix[i, j] + "\t");
        }
        else
        {
            Console.Write("\t");
        }
    }
    Console.WriteLine();
}

// d) Főátló alatti háromszög
Console.WriteLine("\nFőátló alatti háromszög elemei:");
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (j < i)
        {
            Console.Write(veletlenMatrix[i, j] + "\t");
        }
        else
        {
            Console.Write("\t");
        }
    }
    Console.WriteLine();
}

// e) Mellékátló feletti háromszög
Console.WriteLine("\nMellékátló feletti háromszög elemei:");
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (j < n - i - 1)
        {
            Console.Write(veletlenMatrix[i, j] + "\t");
        }
        else
        {
            Console.Write("\t");
        }
    }
    Console.WriteLine();
}

// f) Mellékátló alatti háromszög
Console.WriteLine("\nMellékátló alatti háromszög elemei:");
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (j > n - i - 1)
        {
            Console.Write(veletlenMatrix[i, j] + "\t");
        }
        else
        {
            Console.Write("\t");
        }
    }
    Console.WriteLine();
}
#endregion

Pause();

void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
    }
    Console.WriteLine("1 másodperc...");
    Thread.Sleep(1000);
    Console.ResetColor();
}
