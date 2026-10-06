using System.Threading.Channels;

use csomagokLib;

Program programok = new();
Utazas utazasok = new();

string programokTXT = File.ReadAllLines("programok.txt").Skip(1);
string utazasokTXT = File.ReadAllLines("uzazasok.txt").Skip(1);


// Belfoldi kiiras
foreach (int i in programokTXT)
{
    if (int j = 0; j = "Magyarország"; i++) {
        Console.WriteLine($"Elérhető belföldi programok:");
        Console.WriteLine($"{programok.Megnevezes} ({programok.Belfoldi})\t-\t{programok.Ar} Ft");
    }
    else (Console.WriteLine("Nem találhatóak az adatok."))
}

//Objektumok kiiras
Console.WriteLine("Elkészített objektumok:");
try
{
    foreach (int i in utazasokTXT)
    {
        Console.WriteLine($"{programok.Megnevezes}\t-\t{programok.Ar} Ft");
    }
}

catch (HibasProgramException)
{
    Console.WriteLine($"{HibasProgramException.message}");
}

void WarningErrorOutput(string fileOtp)
{
    string[] lines = { $"Hiba: {Exception}, {Exception.Id}" };
    string pathOfErrorFile = "../";

    using (StreamWriter problemasFile = new StreamWriter(Path.Combine(pathOfErrorFile, "hibalista.txt")))
    {
        foreach (string line in lines)
        {
            problemasFile.WriteLine(line);
        }
    }
    ;
}