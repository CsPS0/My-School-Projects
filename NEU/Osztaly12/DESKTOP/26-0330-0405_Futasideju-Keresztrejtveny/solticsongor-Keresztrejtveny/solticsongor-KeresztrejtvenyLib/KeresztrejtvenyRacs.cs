namespace solticsongor_KeresztrejtvenyLib
{
    public class KeresztrejtvenyRacs
    {
        private List<string> Adatsorok = new List<string>();
        public char[,] Racs;
        public int[,] Sorszamok;

        public int OszlopokDb => Adatsorok.Count > 0 ? Adatsorok[0].Length : 0;
        public int SorokDb => Adatsorok.Count;

        public KeresztrejtvenyRacs(string forras)
        {
            BeolvasAdatsorok(forras);
            Racs = new char[SorokDb + 2, OszlopokDb + 2];
            Sorszamok = new int[SorokDb + 2, OszlopokDb + 2];
            FeltoltRacs();
        }

        private void BeolvasAdatsorok(string forras)
        {
            Adatsorok = File.ReadAllLines(forras).ToList();
        }

        private void FeltoltRacs()
        {
            for (int i = 0; i < SorokDb + 2; i++)
            {
                for (int j = 0; j < OszlopokDb + 2; j++)
                {
                    Racs[i, j] = '#';
                }
            }

            for (int i = 0; i < SorokDb; i++)
            {
                for (int j = 0; j < OszlopokDb; j++)
                {
                    Racs[i + 1, j + 1] = Adatsorok[i][j];
                }
            }
        }

        public void MegjelenitRacs()
        {
            for (int i = 1; i <= SorokDb; i++)
            {
                for (int j = 1; j <= OszlopokDb; j++)
                {
                    Console.Write(Racs[i, j] == '#' ? "##" : "[]");
                }
                Console.WriteLine();
            }
        }

        public int LeghosszabbFuggoleges()
        {
            int maxHossz = 0;
            for (int o = 1; o <= OszlopokDb; o++)
            {
                int aktualisHossz = 0;
                for (int s = 1; s <= SorokDb; s++)
                {
                    if (Racs[s, o] != '#')
                    {
                        aktualisHossz++;
                    }
                    else
                    {
                        if (aktualisHossz > 1)
                        {
                            if (aktualisHossz > maxHossz) maxHossz = aktualisHossz;
                        }
                        aktualisHossz = 0;
                    }
                }
                if (aktualisHossz > 1)
                {
                    if (aktualisHossz > maxHossz) maxHossz = aktualisHossz;
                }
            }
            return maxHossz;
        }

        public SortedDictionary<int, int> VizszintesStatisztika()
        {
            SortedDictionary<int, int> stat = new SortedDictionary<int, int>();
            for (int i = 1; i <= SorokDb; i++)
            {
                int hossz = 0;
                for (int j = 1; j <= OszlopokDb; j++)
                {
                    if (Racs[i, j] != '#')
                    {
                        hossz++;
                    }
                    else
                    {
                        if (hossz > 1)
                        {
                            if (stat.ContainsKey(hossz)) stat[hossz]++;
                            else stat[hossz] = 1;
                        }
                        hossz = 0;
                    }
                }
                if (hossz > 1)
                {
                    if (stat.ContainsKey(hossz)) stat[hossz]++;
                    else stat[hossz] = 1;
                }
            }
            return stat;
        }

        public void Sorszamoz()
        {
            int szamlalo = 1;
            for (int i = 1; i <= SorokDb; i++)
            {
                for (int j = 1; j <= OszlopokDb; j++)
                {
                    if (Racs[i, j] == '#') continue;

                    bool startV = (Racs[i, j - 1] == '#') && (Racs[i, j + 1] != '#');
                    bool startF = (Racs[i - 1, j] == '#') && (Racs[i + 1, j] != '#');

                    if (startV || startF)
                    {
                        Sorszamok[i, j] = szamlalo++;
                    }
                }
            }
        }

        public void MegjelenitSorszamokkal()
        {
            for (int i = 1; i <= SorokDb; i++)
            {
                for (int j = 1; j <= OszlopokDb; j++)
                {
                    if (Racs[i, j] == '#')
                    {
                        Console.Write("##");
                    }
                    else
                    {
                        if (Sorszamok[i, j] > 0)
                        {
                            Console.Write(Sorszamok[i, j].ToString("00"));
                        }
                        else
                        {
                            Console.Write("[]");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
