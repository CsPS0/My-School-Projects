namespace LovaglasLibrary
{
    public class Orarend
    {
        public int Ora_ID { get; init; }
        public DateOnly Datum { get; init; }
        public TimeOnly Kezdesi_Ido { get; init; }
        public int Hossz { get; init; }
        public int Kepzes_ID { get; init; }
        public int Szint_ID { get; init; }
        public int Oktato_ID { get; init; }
        public string Helyszin { get; init; }
        public int Letszam { get; init; }

        public int Keresett
        {
            get
            {
                var oktato = DataStore.Instance?.Oktatok.FirstOrDefault(x => x.Oktato_ID == Oktato_ID);
                if (oktato is null) return 0;
                return (int)Math.Round((double)oktato.Oradij / 60.0 * Hossz * Letszam);
            }
        }

        public int KeresettSzamitas(int oradij)
        {
            return (int)Math.Round((double)oradij / 60.0 * Hossz * Letszam);
        }

        public Orarend(string row)
        {
            string[] col = row.Split(';');
            Ora_ID = int.Parse(col[0]);
            Datum = DateOnly.Parse(col[1]);
            Kezdesi_Ido = TimeOnly.Parse(col[2]);
            Hossz = int.Parse(col[3]);
            Kepzes_ID = int.Parse(col[4]);
            Szint_ID = int.Parse(col[5]);
            Oktato_ID = int.Parse(col[6]);
            Helyszin = col[7];
            Letszam = int.Parse(col[8]);
        }
    }
}
