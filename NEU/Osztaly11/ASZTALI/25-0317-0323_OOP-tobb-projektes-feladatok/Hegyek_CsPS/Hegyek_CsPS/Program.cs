using Hegyek_lib;

Hegyek hegyMo = new Hegyek(File.ReadLines("hegyekMo.txt").Skip(1));
// 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();
Console.WriteLine("Hegyek száma: " + hegyMo.HegyAdatMennyiseg);
// 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();
var alacsony = hegyMo.AlacsonyHegy;
Console.WriteLine($"Legalacsonyabb hegy: {alacsony.Nev} ({alacsony.Hegyseg}), {alacsony.Magassag} m");
// 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();
Console.WriteLine("Átlagos magasság: " + Math.Round(hegyMo.AtlagMagassag, 1) + " m");
// 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();
Console.WriteLine("Mátrában található hegycsúcsok száma: " + hegyMo.MatraCsucsok);
// 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();
Console.WriteLine("\"bérc\" nevű hegycsúcsok száma: " + hegyMo.BercCsucsok);
// 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();
Console.WriteLine("Hegységek nevei ABC sorrendben:");
Console.WriteLine(string.Join("; ", hegyMo.HegysegekABC));
// 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat");
Console.ResetColor();
Console.WriteLine("3000 lábnál magasabb hegycsúcsok száma: " + hegyMo.HaromezerLabnalMagasabb);
// 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat");
Console.ResetColor();
Console.WriteLine("Legmagasabb hegyek kiírása fájlba...");
using (StreamWriter sw = new StreamWriter("haromlegmag.txt"))
{
    foreach (var hegy in hegyMo.HaromLegmagasabb)
    {
        sw.WriteLine($"{hegy.Magassag} - {hegy.Hegyseg}: {hegy.Nev}");
    }
}