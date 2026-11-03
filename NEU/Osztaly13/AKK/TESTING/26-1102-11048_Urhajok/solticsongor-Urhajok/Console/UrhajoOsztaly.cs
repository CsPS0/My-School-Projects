namespace ConsoleApp;

public class UrhajoOsztaly
{
    public string Nev { get; }

    public UrhajoOsztaly(string nev)
    {
        if (string.IsNullOrWhiteSpace(nev))
        {
            throw new ArgumentException("Az űrhajó osztály neve nem lehet üres.", nameof(nev));
        }

        Nev = nev;
    }
}
