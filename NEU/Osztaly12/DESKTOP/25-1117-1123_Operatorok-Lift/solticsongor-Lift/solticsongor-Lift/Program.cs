using solticsongor_Lift;

try
{
    string[] sorok = File.ReadAllLines("input.txt");
    int sorIndex = 0;

    int n = 0;
    if (sorIndex < sorok.Length)
    {
        string? elsoSor = sorok[sorIndex++];
        try
        {
            n = int.Parse(elsoSor);
        }
        catch (FormatException)
        {
            n = 0;
        }
    }

    List<Lift> liftDolog = new List<Lift>();
    for (int i = 0; i < n; i++)
    {
        if (sorIndex < sorok.Length)
        {
            string? elsoSor = sorok[sorIndex++];
            try
            {
                int emeletSzam = int.Parse(elsoSor);
                liftDolog.Add(new Lift(emeletSzam));
            }
            catch
            {
                liftDolog.Add(new Lift(10));
            }
        }
        else
        {
            liftDolog.Add(new Lift(10)); // ha elfogyna
        }
    }

    Liftek liftek = new Liftek(liftDolog);
    while (sorIndex < sorok.Length)
    {
        string? elsoSor = sorok[sorIndex++];
        if (elsoSor == null) continue; //bár 0%-os az esély, azért itt van
        try
        {
            string[] taglalas = elsoSor.Split(';');
            if (taglalas.Length != 2)
            {
                Console.WriteLine("Hibás sor");
                continue;
            }
            int liftIndex = int.Parse(taglalas[0]);
            string irany = taglalas[1];

            Lift jelenlegiLift = liftek[liftIndex];

            if (irany == "fel")
            {
                jelenlegiLift.Felfele();
            }

            else if (irany == "le")
            {
                jelenlegiLift.Lefele();
            }
            else
            {
                Console.WriteLine("Hibás sor");
                continue;
            }
            Console.WriteLine(jelenlegiLift);
        }
        catch (FormatException)
        {
            Console.WriteLine("Hibás sor");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

catch (Exception ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}