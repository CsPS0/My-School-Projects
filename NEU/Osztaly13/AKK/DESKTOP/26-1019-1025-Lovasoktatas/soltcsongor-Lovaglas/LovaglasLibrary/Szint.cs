namespace LovaglasLibrary
{
    public class Szint
    {
        public int Szint_ID { get; set; }
        public string Megnevezes { get; set; }
        public string Kategoria { get; set; }
        public string Leiras { get; set; }

        public Szint(string row)
        {
            string[] col = row.Split(';');
            Szint_ID = int.Parse(col[0]);
            Megnevezes = col[1];
            Kategoria = col[2];
            Leiras = col[3];
        }
    }
}
