namespace ConsoleApp;

public class UrhajoOsztaly
{
    public string Nev { get; set; }

    public UrhajoOsztaly(string nev)
    {
        if (string.IsNullOrEmpty(nev))
        {
            throw new ArgumentException("Az osztály neve kötelező!");
        }

        Nev = nev;
    }
}
