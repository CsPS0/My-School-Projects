//1. Feladat:
Console.WriteLine("\nElső Feladat\n");
Console.WriteLine("Add meg az N értékét: ");
int N  = int.Parse(Console.ReadLine()!);
for (int i = 1; i <= N; i++)
{
    Console.WriteLine(i);
}

//2. Feladat:
Console.WriteLine("\nMásodik Feladat\n");
Console.Write("Add meg az N értékét: ");
N = int.Parse(Console.ReadLine()!); //újrahasznált változó
for (int i = 1; i <= N; i++)
{
    Console.WriteLine(i * i);
}

//3. Feladat:
Console.WriteLine("\nHarmadik Feladat\n");
Console.Write("Add meg az N értékét: ");
N = int.Parse(Console.ReadLine()!);
for (int i = N; i >= 1; i--)
{
    Console.WriteLine(i);
}

//4. Feladat:
Console.WriteLine("\nNegyedok Feladat\n");
Console.Write("Add meg az N értékét: ");
N = int.Parse(Console.ReadLine()!);
for (int i = 1; i <= N; i++)
{
    Console.WriteLine(i * 2);
}

//5. Feladat:
Console.WriteLine("\nÖtödik Feladat\n");
Console.Write("Add meg az 'a' értékét: ");
int a = int.Parse(Console.ReadLine()!);
Console.Write("Add meg a 'b' értékét: ");
int b = int.Parse(Console.ReadLine()!);
for (int i = a; i <= b; i++)
{
    Console.WriteLine(i);
}

//6. Feladat:
Console.WriteLine("\nHatodik Feladat\n");
Console.Write("Add meg egy számot: ");
int szam = int.Parse(Console.ReadLine()!);
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(szam * i);
}

//7. Feladat:
Console.WriteLine("\nHetedik Feladat\n");
Console.Write("Add meg egy számot: ");
szam = int.Parse(Console.ReadLine()!);
for (int i = 1; szam * i < 100; i++)
{
    Console.WriteLine(szam * i);
}

//8. Feladat:
Console.WriteLine("\nNyolcadik Feladat\n");
Console.Write("Add meg egy pozitív számot: ");
szam = int.Parse(Console.ReadLine()!);
for (int i = 1; i <= szam; i++)
{
    if (szam % i == 0)
    {
        Console.WriteLine(i);
    }
}

//9. Feladat:
Console.WriteLine("\nKilencedik Feladat\n");
Console.Write("Add meg a hatvány alapját: ");
int alap = int.Parse(Console.ReadLine()!);
Console.Write("Add meg a hatvány kitevőjét: ");
int kitevo = int.Parse(Console.ReadLine()!);
int eredmeny = 1;
for (int i = 1; i <= kitevo; i++)
{
    eredmeny *= alap;
}
Console.WriteLine("Az eredmény: " + eredmeny);

//10. Feladat:
Console.WriteLine("\nTizedik Feladat\n");
Console.Write("Add meg az intervallum alsó határát: ");
a = int.Parse(Console.ReadLine()!);
Console.Write("Add meg az intervallum felső határát: ");
b = int.Parse(Console.ReadLine()!);
Console.Write("Add meg az osztó számot: ");
szam = int.Parse(Console.ReadLine()!);
for (int i = a; i <= b; i++)
{
    if (i % szam == 0)
    {
        Console.WriteLine(i);
    }
}

//11. Feladat:
Console.WriteLine("\nTizenegyedik Feladat\n");
Console.Write("Add meg az N értékét: ");
N = int.Parse(Console.ReadLine()!);
Random rnd = new Random();
for (int i = 0; i < N; i++)
{
    Console.WriteLine(rnd.Next(0, 101));
}

//12. Feladat:
Console.WriteLine("\nTizenkettedik Feladat\n");
Console.Write("Add meg az N értékét: ");
N = int.Parse(Console.ReadLine()!);
Console.Write("Add meg az 'a' értékét: ");
a = int.Parse(Console.ReadLine()!);
Console.Write("Add meg a 'b' értékét: ");
b = int.Parse(Console.ReadLine()!);
for (int i = 0; i < N; i++)
{
    Console.WriteLine(rnd.NextDouble() * (b - a) + a);
}

//13. Feladat:
Console.WriteLine("\nTizenharmadik Feladat\n");
Console.Write("Add meg a K értékét: ");
int K = int.Parse(Console.ReadLine()!);
for (int i = 0; i < K; i++)
{
    if (rnd.Next(0, 2) == 0)
    {
        Console.WriteLine("Fej");
    }
    else
    {
        Console.WriteLine("Írás");
    }
}

//14. Feladat:
Console.WriteLine("\nTizennegyedik Feladat\n");
Console.WriteLine("Ez az összeg lesz számolva: 1*2 + 2*3 + 3*4 + ... + K*(K+1)");
Console.Write("Add meg a K értékét: ");
K = int.Parse(Console.ReadLine()!);
int osszeg = 0;
for (int i = 1; i <= K; i++)
{
    osszeg += i * (i + 1);
}
Console.WriteLine("Az összeg: " + osszeg);

//15. Feladat:
Console.WriteLine("\nTizenötödik Feladat\n");
Console.Write("Add meg a négyzet méretét: ");
K = int.Parse(Console.ReadLine()!);
for (int i = 0; i < K; i++)
{
    for (int j = 0; j < K; j++)
    {
        if (i == 0 || i == K - 1 || j == 0 || j == K - 1)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
}

//16. Feladat:
Console.WriteLine("\nTizenhatodik Feladat\n");
Console.Write("Add meg a sorok számát: ");
int sorok = int.Parse(Console.ReadLine()!);
Console.Write("Add meg az oszlopok számát: ");
int oszlopok = int.Parse(Console.ReadLine()!);
for (int i = 0; i < sorok; i++)
{
    for (int j = 0; j < oszlopok; j++)
    {
        if ((i + j) % 2 == 0)
        {
            Console.Write("X");
        }
        else
        {
            Console.Write("O");
        }
    }
    Console.WriteLine();
}

//17. Feladat: (ha nem megy, hát nem megy :'[  )
Console.WriteLine("\nTizenhetedik Feladat\n");
Console.Write("Add meg a háromszög sorainak számát: ");
int sorok17 = int.Parse(Console.ReadLine()!);
for (int i = 1; i <= sorok17; i++)
{
    for (int j = 0; j < sorok17 - i; j++)
    {
        Console.Write(" ");
    }
    for (int j = 0; j < 2 * i - 1; j++)
    {
        Console.Write("*");
    }
    Console.WriteLine();
}

//18. Feladat:
Console.WriteLine("\nTizennyolcadik Feladat\n");
for (int i = -99; i <= 99; i++)
{
    if (i % 3 == 0 && i % 2 != 0 && Math.Abs(i) >= 10 && Math.Abs(i) < 100)
    {
        Console.WriteLine(i);
    }
}

//19. Feladat:
Console.WriteLine("\nTizenkilencedik Feladat\n");
for (int i = 1; i <= 14; i++)
{
    int gyakorlasIdo = rnd.Next(30, 121); // 30 perctől 120 percig [ugyebbár ez nem SQL :D]
    Console.WriteLine($"{i}. nap: {gyakorlasIdo} perc");
}

//20. Feladat:
Console.WriteLine("\nHuszadik Feladat\n");
for (int i = 100; i < 1000; i++)
{
    int szaz = i / 100;
    int tiz = (i / 10) % 10;
    int egyes = i % 10;

    if (Math.Pow(szaz, 3) + Math.Pow(tiz, 3) + Math.Pow(egyes, 3) == i) //Math.Pow() = hatványra emelés; első szám: alap , második szám: kitevő
    {
        Console.WriteLine(i);
    }
}