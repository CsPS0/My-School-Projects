namespace LovaglasLibrary
{
    public class Orarend
    {
        public int Ora_ID { get; set; }
        public DateOnly Datum { get; set; }
        public TimeOnly Kezdesi_Ido { get; set; }
        public int Hossz { get; set; }
        public int Kepzes_ID { get; set; }
        public int Szint_ID { get; set; }
        public int Oktato_ID { get; set; }
        public string Helyszin { get; set; }
        public int Letszam { get; set; }

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
