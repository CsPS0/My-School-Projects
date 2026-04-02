class KeresztrejtvenyRacs
{
    private List<string> Adatsorok = new List<string>();
    public char[,] Racs;
    public int[,] Sorszamok;

    public int SorokDb => Adatsorok.Count;
    public int OszlopokDb => Adatsorok.Count > 0 ? Adatsorok[0].Length : 0;

    public KeresztrejtvenyRacs(string forras)
    {
        BeolvasAdatsorok(forras);
        Racs = new char[SorokDb, OszlopokDb];
        Sorszamok = new int[SorokDb, OszlopokDb];
        FeltoltRacs();
    }

    private void BeolvasAdatsorok(string forras)
    {
        Adatsorok = File.ReadAllLines(forras).ToList();
    }

    private void FeltoltRacs()
    {
        for (int i = 0; i < SorokDb; i++)
        {
            for (int j = 0; j < OszlopokDb; j++)
            {
                Racs[i, j] = Adatsorok[i][j];
            }
        }
    }

    public void MegjelenitRacs()
    {
        for (int i = 0; i < SorokDb; i++)
        {
            for (int j = 0; j < OszlopokDb; j++)
            {
                Console.Write(Racs[i, j] == '#' ? "##" : "[]");
            }
            Console.WriteLine();
        }
    }

    public int LeghosszabbFuggoleges()
    {
        int maxHossz = 0;
        for (int o = 0; o < OszlopokDb; o++)
        {
            int aktualisHossz = 0;
            for (int s = 0; s < SorokDb; s++)
            {
                if (Racs[s, o] != '#')
                {
                    aktualisHossz++;
                }
                else
                {
                    if (aktualisHossz > 1) maxHossz = Math.Max(maxHossz, aktualisHossz);
                    aktualisHossz = 0;
                }
            }
            if (aktualisHossz > 1) maxHossz = Math.Max(maxHossz, aktualisHossz);
        }
        return maxHossz;
    }

    public void VizszintesStatisztika()
    {
        Dictionary<int, int> stat = new Dictionary<int, int>();
        for (int i = 0; i < SorokDb; i++)
        {
            int hossz = 0;
            for (int j = 0; j < OszlopokDb; j++)
            {
                if (Racs[i, j] != '#')
                {
                    hossz++;
                }
                else
                {
                    if (hossz > 1) FrissitStat(stat, hossz);
                    hossz = 0;
                }
            }
            if (hossz > 1) FrissitStat(stat, hossz);
        }

        foreach (var elem in stat.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{elem.Key} betűs: {elem.Value} darab");
        }
    }

    private void FrissitStat(Dictionary<int, int> stat, int hossz)
    {
        if (stat.ContainsKey(hossz)) stat[hossz]++;
        else stat[hossz] = 1;
    }

    public void Sorszamoz()
    {
        int szamlalo = 1;
        for (int i = 0; i < SorokDb; i++)
        {
            for (int j = 0; j < OszlopokDb; j++)
            {
                if (Racs[i, j] == '#') continue;

                bool startV = (j == 0 || Racs[i, j - 1] == '#') && (j + 1 < OszlopokDb && Racs[i, j + 1] != '#');
                bool startF = (i == 0 || Racs[i - 1, j] == '#') && (i + 1 < SorokDb && Racs[i + 1, j] != '#');

                if (startV || startF)
                {
                    Sorszamok[i, j] = szamlalo++;
                }
            }
        }
    }

    public void MegjelenitSorszamokkal()
    {
        for (int i = 0; i < SorokDb; i++)
        {
            for (int j = 0; j < OszlopokDb; j++)
            {
                if (Racs[i, j] == '#') Console.Write("##");
                else if (Sorszamok[i, j] > 0) Console.Write(Sorszamok[i, j].ToString("00"));
                else Console.Write("[]");
            }
            Console.WriteLine();
        }
    }
}