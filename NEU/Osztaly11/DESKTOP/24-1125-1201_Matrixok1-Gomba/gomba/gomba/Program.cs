#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

string file = "erdo.txt";
string[] sorok = File.ReadAllLines(file);
string[] elsoSor = sorok[0].Split(' ');
int N = int.Parse(elsoSor[0]);
int M = int.Parse(elsoSor[1]);

int[,] gombak = new int[N, M];

for (int i = 0; i < N; i++)
{
    string[] gombaSor = sorok[i + 1].Split(' ');
    for (int j = 0; j < M; j++)
    {
        gombak[i, j] = int.Parse(gombaSor[j]);
    }
}

Console.WriteLine($"A {file} beolvasva és eltárolva");
#endregion

Pause();

#region 2.eladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

int osszesGomba = 0;
for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
    {
        osszesGomba += gombak[i, j];
    }
}
Console.WriteLine($"Összes gomba: {osszesGomba}");
#endregion

Pause();

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

int teruletKevesebbMint5 = 0;
for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
    {
        if (gombak[i, j] < 5)
        {
            teruletKevesebbMint5++;
        }
    }
}
Console.WriteLine($"Területek, ahol 5-nél kevesebb gomba volt: {teruletKevesebbMint5}");
#endregion

Pause();

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

double avg = (double)osszesGomba / (N * M);
double atlag = Math.Round(avg, 3);
Console.WriteLine($"A mezők átlaga: {atlag} lesz.");

for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
    {
        if (gombak[i, j] > atlag)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(gombak[i, j]);
            Console.ResetColor();
        }

        else
        {
            Console.Write(gombak[i, j]);
        }
    }
    Console.WriteLine();
}

#endregion

Pause();

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

int minGombaErtek = int.MaxValue;
string irany = "";

int minGombaÉszak = 0, minGombaKelet = 0, minGombaDél = 0, minGombaNyugat = 0;

for (int j = 0; j < M; j++)
{
    minGombaÉszak += gombak[0, j];
}

for (int i = 0; i < N; i++)
{
    minGombaKelet += gombak[i, M - 1];
}

for (int j = 0; j < M; j++)
{
    minGombaDél += gombak[N - 1, j];
}

for (int i = 0; i < N; i++)
{
    minGombaNyugat += gombak[i, 0];
}

minGombaErtek = Math.Min(Math.Min(minGombaÉszak, minGombaKelet), Math.Min(minGombaDél, minGombaNyugat));

if (minGombaErtek == minGombaÉszak)
    irany = "észak";
else if (minGombaErtek == minGombaKelet)
    irany = "kelet";
else if (minGombaErtek == minGombaDél)
    irany = "dél";
else
    irany = "nyugat";

Console.WriteLine($"A legkevesebb gomba az {irany} irányban volt: {minGombaErtek} gomba.");
#endregion

Pause();

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

int oszlopokTobbMint100 = 0;

for (int j = 0; j < M; j++)
{
    int oszlopOsszeg = 0;
    for (int i = 0; i < N; i++)
    {
        oszlopOsszeg += gombak[i, j];
    }
    if (oszlopOsszeg > 100)
    {
        oszlopokTobbMint100++;
    }
}

Console.WriteLine($"Oszlopok, ahol több mint 100 gombát találtak: {oszlopokTobbMint100}");
#endregion

Pause();

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

bool voltGomba = false;
for (int i = 0; i < N; i++)
{
    bool nincsGomba = true;
    for (int j = 0; j < M; j++)
    {
        if (gombak[i, j] > 0)
        {
            nincsGomba = false;
            break;
        }
    }
    if (nincsGomba)
    {
        Console.WriteLine($"Nem volt gomba a(z) {i + 1}. sorban.");
        voltGomba = true;
    }
}

if (!voltGomba)
{
    Console.WriteLine("Mindegyikben volt gomba.");
}
#endregion

Pause();

#region 8.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

Console.Write("Add meg a gombák számát: ");
int keresettGomba = int.Parse(Console.ReadLine()!);

bool talalt = false;

for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
    {
        if (gombak[i, j] == keresettGomba)
        {
            Console.WriteLine($"Találat a(z) ({i + 1},{j + 1}) koordinátán.");
            talalt = true;
        }
    }
}

if (!talalt)
{
    Console.WriteLine("Nem találtunk ilyen számú gombát.");
}
#endregion

Pause();

#region 9.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

int maxGomba = 0;
List<string> maxGombaTeruletek = new List<string>();

for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
    {
        if (gombak[i, j] > maxGomba)
        {
            maxGomba = gombak[i, j];
            maxGombaTeruletek.Clear();
            maxGombaTeruletek.Add($"({i + 1},{j + 1})");
        }
        else if (gombak[i, j] == maxGomba)
        {
            maxGombaTeruletek.Add($"({i + 1},{j + 1})");
        }
    }
}

Console.WriteLine("A legtöbb gomba ezen a területen volt:");
foreach (var terulet in maxGombaTeruletek)
{
    Console.Write(terulet + " ");
}

File.WriteAllText("max.txt", string.Join(" ", maxGombaTeruletek));
Console.WriteLine("\nA max.txt fájlba kiírásra került.");
#endregion

Pause();

#region 10.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat");
Console.ResetColor();

int minGomba3x3 = int.MaxValue;

for (int i = 0; i < N - 2; i++)
{
    for (int j = 0; j < M - 2; j++)
    {
        int teruletOsszeg = 0;
        for (int di = 0; di < 3; di++)
        {
            for (int dj = 0; dj < 3; dj++)
            {
                teruletOsszeg += gombak[i + di, j + dj];
            }
        }

        minGomba3x3 = Math.Min(minGomba3x3, teruletOsszeg);
    }
}

Console.WriteLine($"A legkevesebb gomba egy 3x3-as területen: {minGomba3x3}");
#endregion

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