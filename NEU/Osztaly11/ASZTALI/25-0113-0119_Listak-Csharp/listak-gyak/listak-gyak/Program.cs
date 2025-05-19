#region Files
string[] file = File.ReadAllLines("hegyekMO.txt");

if (File.Exists("hegyekMO.txt"))
{
    Console.WriteLine("The file exists.");
}
else
{
    Console.WriteLine("The file does not exist.");
    return;
}

List<Mountains> MNTs = new List<Mountains>();

for (int i = 0; i < file.Length; i++)
{
    string[] parts = file[i].Split(';');
    string mountainName = parts[0];
    int mountainHeight = int.Parse(parts[1]);
    MNTs.Add(new Mountains(mountainName, mountainHeight));
}

Console.Write("A ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("hegyekMO.txt");
Console.ResetColor();
Console.WriteLine(" has been successfully loaded.");
#endregion

Pause();

#region 1.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();

for (int i = 0; i < MNTs.Count; i++)
{
    Console.WriteLine(MNTs[i].Name + " (" + MNTs[i].Height + " m)");
}
#endregion

Pause();

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();

double sumHeight = 0;
for (int i = 0; i < MNTs.Count; i++)
{
    sumHeight += MNTs[i].Height;
}
double averageHeight = sumHeight / MNTs.Count;
Console.WriteLine("Az átlagos magasság: " + Math.Round(averageHeight, 1) + " m");
#endregion

Pause();

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();

Mountains highestMountain = MNTs[0];
for (int i = 1; i < MNTs.Count; i++)
{
    if (MNTs[i].Height > highestMountain.Height)
    {
        highestMountain = MNTs[i];
    }
}
Console.WriteLine("A legmagasabb hegycsúcs: " + highestMountain.Name + " (" + highestMountain.Height + " m)");
#endregion

Pause();

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();

int countAbove900 = 0;
for (int i = 0; i < MNTs.Count; i++)
{
    if (MNTs[i].Height >= 900)
    {
        countAbove900++;
    }
}
Console.WriteLine("Legalább 900 méteres hegycsúcsok száma: " + countAbove900);
#endregion

Pause();

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();

Console.Write("Add meg egy hegycsúcs nevét: ");
string inputName = Console.ReadLine();

bool found = false;
for (int i = 0; i < MNTs.Count; i++)
{
    if (MNTs[i].Name.ToLower() == inputName.ToLower())
    {
        Console.WriteLine("A(z) " + MNTs[i].Name + " magassága: " + MNTs[i].Height + " m");
        found = true;
        break;
    }
}

if (!found)
{
    Console.WriteLine("Nincs ilyen nevű hegycsúcs az adatok között.");
}
#endregion

Pause();

void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
        // skibidi hiba bypass
    }
    Console.WriteLine("1 másodperc...");
    Thread.Sleep(1000);
    Console.ResetColor();
}

public record Mountains(string Name, int Height);