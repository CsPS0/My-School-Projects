using passLib;

Console.WriteLine("Jelszó Ellenőrző Program (Kilépés: 'exit')");

while (true)
{
    Console.Write("\nKérem a jelszót: ");
    string? pwd = Console.ReadLine();

    if (string.IsNullOrEmpty(pwd) || pwd.ToLower() == "exit")
        break;

    bool ok = Password.IsValid(pwd);

    if (ok)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("A jelszó MEGFELEL a követelményeknek.");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("A jelszó NEM felel meg (min 6 kar, kisbetű, nagybetű, szám).");
    }
    Console.ResetColor();
}