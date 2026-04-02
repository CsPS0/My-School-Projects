using solticsongor_SzokoEvConsole;
using System.Diagnostics;

var converter = new LeapYearConverter();

Console.Write("Kérlek, adj meg egy évszámot: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string? input = Console.ReadLine();
Console.ResetColor();

if (input != null && int.TryParse(input, out int year))
{
    bool isLeap = converter.IsLeapYear(year);
    string status = isLeap ? "szökőév" : "nem szökőév";
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write(year);
    Console.ResetColor();
    Console.WriteLine($" egy {status}.");
}
else
{
    Console.WriteLine("Érvénytelen évszám lett megadva.");
}

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("\nTeszt futtatása 2 másodpercen belül...\n");
Console.ResetColor();
Thread.Sleep(2000);

RunTests();

static void RunTests()
{
    var startInfo = new ProcessStartInfo
    {
        FileName = "dotnet",
        Arguments = "test ../solticsongor-SzokoEvTest/solticsongor-SzokoEvTest.csproj",
        RedirectStandardOutput = false,
        UseShellExecute = false,
        CreateNoWindow = false
    };

    using var process = Process.Start(startInfo);
    process?.WaitForExit();
}