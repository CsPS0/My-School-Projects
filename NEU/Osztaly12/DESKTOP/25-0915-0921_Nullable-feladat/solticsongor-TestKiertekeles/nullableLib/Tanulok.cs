namespace nullableLib
{
    public class Tanulok
    {
        public List<Tanulo> Lista = new List<Tanulo>();

        public Tanulok(string filenev)
        {
            if (File.Exists(filenev))
            {
                string[] sorok = File.ReadAllLines(filenev);
                for (int i = 1; i < sorok.Length; i++)
                {
                    string[] adatok = sorok[i].Split(';');
                    if (adatok.Length > 0 && !string.IsNullOrWhiteSpace(adatok[0]))
                    {
                        Lista.Add(new Tanulo(sorok[i]));
                    }
                }
            }

            else
            {
                Console.WriteLine($"A '{filenev}' nem létezik, vagy nem olvasható!");
            }
        }

        public List<Tanulo> KeresesNevvel(string nevReszlet)
        {
            List<Tanulo> talalatok = new List<Tanulo>();
            foreach (Tanulo tanulo in Lista)
            {
                if (tanulo.Nev.IndexOf(nevReszlet, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    talalatok.Add(tanulo);
                }
            }
            return talalatok;
        }

        public int HanyanMindkettoCsoportban(Tanulok masikCsoport)
        {
            int darab = 0;
            foreach (Tanulo sajatTanulo in this.Lista)
            {
                foreach (Tanulo masikTanulo in masikCsoport.Lista)
                {
                    if (sajatTanulo.Nev == masikTanulo.Nev)
                    {
                        darab++;
                    }
                }
            }
            return darab;
        }
    }
}