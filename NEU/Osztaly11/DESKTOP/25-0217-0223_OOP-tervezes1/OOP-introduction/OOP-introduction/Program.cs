using OOP_introduction;

#region 1.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

Teglalap teglalap = new Teglalap();
teglalap.A = 11;
teglalap.B = 12;
Console.WriteLine(teglalap.Info());
#endregion

Pause();

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

Termek termek = new Termek("Laptop", 300000);
Console.WriteLine(termek.Informacio());
#endregion

void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nNyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
        // bypass warning
    }
    Console.WriteLine("1 másodperc...\n");
    Thread.Sleep(1000);
    Console.ResetColor();
}
