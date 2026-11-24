namespace LovaglasLibrary
{
    public class Kepzes
    {
        public int Kepzes_ID { get; set; }
        public string Kepzes_Tipus { get; set; }
        public int Ajanlott_Szint { get; set; }

        public Kepzes(string row)
        {
            string[] col = row.Split(';');
            Kepzes_ID = int.Parse(col[0]);
            Kepzes_Tipus = col[1];
            Ajanlott_Szint = int.Parse(col[2]);
        }
    }
}
