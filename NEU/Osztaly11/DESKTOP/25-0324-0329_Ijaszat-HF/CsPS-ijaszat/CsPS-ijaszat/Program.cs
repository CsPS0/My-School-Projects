using ijaszatLib;

class Program
{
    static void Main()
    {
        Ijaszok csapat = new Ijaszok();

        Random rnd = new Random();
        for (int i = 0; i < 3; i++)
        {
            int ugy = rnd.Next(3, 6);
            csapat.Hozzaad(new Ijasz(ugy));
        }

        csapat.Listaz();

        Console.WriteLine("\nLövetés mindenkivel:");
        csapat.LoMind();

        Console.WriteLine("\nPihentetés mindenkivel:");
        csapat.PihentetMind();

        Console.WriteLine("\nLegügyesebb:");
        Console.WriteLine(csapat.Legugyesebb()?.Info());

        Console.WriteLine("\nLegmagasabb szintű:");
        Console.WriteLine(csapat.LegmagasabbSzintu()?.Info());
    }
}