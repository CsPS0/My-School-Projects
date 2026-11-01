#region Files
string[] meres = File.ReadAllLines("meres.txt");

int sor = 50;
int oszlop = 50;

int[,] adat = new int[sor, oszlop];

for (int i = 0; i < sor; i++)
{
    string[] adatok = meres[i].Split(" ");
    for (int j = 0; j < oszlop; j++)
    {
        if ()
        {

        }

        else
        {

        }
    }
}
Console.WriteLine("");
#endregion

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

Console.Write("Adj meg egy számot 1 és 50 között: ");
int szam = int.Parse(Console.ReadLine()!);

bool keres_szam = false;
for (int i = 0;i < sor; i++)
{
    for (int j = 0;j < oszlop; j++)
    {
        if ()
        {
            
        }
    }
}
#endregion

Pause();

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

int osszeg = 0;
for (int i = 0; i < sor ; i++)
{
    for (int j=0; j < oszlop;j++)
    {
        if (adat[i, j] != 0)
        {
            osszeg++;
        }
    }
}
Console.WriteLine($"A baktériumok összege: {osszeg} lett.");
#endregion

Pause();

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();
#endregion

Pause();

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();
#endregion

Pause();

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();
#endregion

Pause();

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();
#endregion

Pause();

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();
#endregion

Pause();

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();
#endregion

void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
        //asd
    }
    Console.WriteLine("1 másodperc...");
    Thread.Sleep(1000);
    Console.ResetColor();
}