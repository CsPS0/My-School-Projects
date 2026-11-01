namespace IskolaLib
{
    public class Iskola
    {
        private List<Diak> diakok = new List<Diak>();
        private List<Tantargy> tantargyak = new List<Tantargy>();

        public void UjDiak(Diak diak)
        {
            diakok.Add(diak);
        }

        public void UjTantargy(Tantargy tantargy)
        {
            tantargyak.Add(tantargy);
        }

        public Diak this[string azon]
        {
            get
            {
                var diak = diakok.FirstOrDefault(d => d.Azon == azon);
                if (diak == null)
                {
                    throw new KeyNotFoundException("Nincs ilyen azonosítójú diák.");
                }
                return diak;
            }
        }

        public Tantargy this[string nev, bool isTantargy]
        {
            get
            {
                var tantargy = tantargyak.FirstOrDefault(t => t.Nev == nev);
                if (tantargy == null)
                {
                    throw new KeyNotFoundException("Nincs ilyen nevű tantárgy.");
                }
                return tantargy;
            }
        }
    }
}