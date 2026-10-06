namespace csomagokLib;

public class HibasProgramException : Exception
{
    public HibasProgramException(string message) : base(message)
    {
        Console.WriteLine($"A megadott programazonosító nem létezik.");
    }
}
