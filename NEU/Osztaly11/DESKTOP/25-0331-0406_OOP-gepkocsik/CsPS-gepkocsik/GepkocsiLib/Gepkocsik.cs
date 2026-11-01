namespace GepkocsiLib
{
    public class Gepkocsik
    {
        public List<Gepkocsi> gepkocsik = new List<Gepkocsi>();

        public Gepkocsik(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                gepkocsik.Add(new Gepkocsi(i));
            }
        }

        public int Gepkocsikszama => gepkocsik.Count;

        public int Budapesti => gepkocsik.Count(x => x.Kerulet() > 0);

        public int Videki => gepkocsik.Count(x => x.Kerulet() == -1);

        public string Tobb()
        {
            if (Budapesti > Videki)
            {
                return "A budapesti autók száma több mint a vidéki autóké.";
            }
            else if (Budapesti < Videki)
            {
                return "A vidéki autók száma kevesebb mint a budapesti autóké.";
            }
            else
            {
                return "A budapesti és vidéki autók száma egyenlő.";
            }
        }

        public double Atlag
        {
            get
            {
                var budapestiAutok = gepkocsik.Where(x => x.Kerulet() != -1).ToList();
                if (budapestiAutok.Count > 0)
                {
                    double ossz = budapestiAutok.Average(x => x.Urtartalom);
                    return ossz / 1000;
                }
                return 0;
            }
        }


        public string Legkevesebb
        {
            get
            {
                var keves = gepkocsik.GroupBy(x => x.Gyartmany).OrderBy(group => group.Count()).First();
                return keves.Key;
            }
        }

        public void FajlIr(string gyartmany)
        {
            string fajl = "gyartmany.txt";
            using (StreamWriter asd = new StreamWriter(fajl))
            {
                var valogatot = gepkocsik.Where(x => x.Gyartmany.Equals(gyartmany, StringComparison.OrdinalIgnoreCase));
                foreach (var kocsi in valogatot)
                {
                    asd.WriteLine($"{kocsi.Rendszam};{kocsi.Nev};{kocsi.Cim};{kocsi.Gyartmany};{kocsi.Tipus};{kocsi.Urtartalom}");
                }
            }
            Console.WriteLine($"A {gyartmany} gyártmányú autók adatai a {fajl} fájlba lettek mentve.");
        }
    }
}