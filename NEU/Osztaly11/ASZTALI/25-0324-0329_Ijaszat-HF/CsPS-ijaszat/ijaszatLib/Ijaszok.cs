namespace ijaszatLib
{
    public class Ijaszok
    {
        readonly List<Ijasz> ijaszLista;

        public Ijaszok()
        {
            ijaszLista = new List<Ijasz>();
        }

        public void Hozzaad(Ijasz ijasz)
        {
            ijaszLista.Add(ijasz);
        }

        public void LoMind()
        {
            foreach (var ijasz in ijaszLista)
            {
                bool siker = ijasz.Lo();
                Console.WriteLine(siker
                    ? $"Lövés sikeres: {ijasz.Info()}"
                    : $"Túl fáradt: {ijasz.Info()}");
            }
        }

        public void PihentetMind()
        {
            foreach (var ijasz in ijaszLista)
            {
                ijasz.Pihen();
                Console.WriteLine($"Pihent: {ijasz.Info()}");
            }
        }

        public void Listaz()
        {
            Console.WriteLine("Íjász lista:");
            foreach (var ijasz in ijaszLista)
            {
                Console.WriteLine(ijasz.Info());
            }
        }

        public Ijasz Legugyesebb()
        {
            return ijaszLista.OrderByDescending(i => i.Ugyesseg).FirstOrDefault();
        }

        public Ijasz LegmagasabbSzintu()
        {
            return ijaszLista.OrderByDescending(i => i.Szint).FirstOrDefault();
        }

        public int Letszam => ijaszLista.Count;
    }
}