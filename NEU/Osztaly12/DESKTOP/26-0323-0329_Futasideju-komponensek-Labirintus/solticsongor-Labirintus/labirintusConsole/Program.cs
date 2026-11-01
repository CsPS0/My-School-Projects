using labirintusConsole;

string file1 = "Lab1.txt";
if (!File.Exists(file1))
{
    file1 = Path.Combine("..", "Lab1.txt");
}

Console.WriteLine("5. feladat: Labirintus adatai");
LabSim lab1 = new LabSim(file1);
Console.WriteLine($"\tSorok száma: {lab1.SorokSzama}");
Console.WriteLine($"\tOszlopok száma: {lab1.OszlopokSzama}");
Console.WriteLine($"\tKijárat indexe: sor:{lab1.KijaratSorindex} oszlop:{lab1.KijaratOszlopindex}");

Console.WriteLine("6. feladat: A labirintus");
lab1.KiirLab();

lab1.Utkereses();

Console.WriteLine("\nAz útkeresés eredménye:");
lab1.KiirLab();
if (lab1.KeresesKesz)
{
    Console.WriteLine("Útvonal megtalálva!");
}
else
{
    Console.WriteLine("Nincs megoldás!");
}

string file2 = "Lab2.txt";
if (!File.Exists(file2)) file2 = Path.Combine("..", "Lab2.txt");
if (File.Exists(file2))
{
    Console.WriteLine("\nLab2.txt tesztelése:");
    LabSim lab2 = new LabSim(file2);
    lab2.Utkereses();
    lab2.KiirLab();
    if (lab2.KeresesKesz) Console.WriteLine("Útvonal megtalálva!");
    else Console.WriteLine("Nincs megoldás!");
}

Console.WriteLine("\nNyomjon meg egy billentyűt a kilépéshez...");
Console.ReadKey();