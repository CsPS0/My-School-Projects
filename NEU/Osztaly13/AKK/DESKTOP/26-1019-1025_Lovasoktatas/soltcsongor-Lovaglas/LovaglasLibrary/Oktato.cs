namespace LovaglasLibrary
{
    public class Oktato
    {
        public int Oktato_ID { get; set; }
        public string Nev { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int Oradij { get; set; }

        public Oktato(string row)
        {
            string[] col = row.Split(';');
            Oktato_ID = int.Parse(col[0]);
            Nev = col[1];
            Email = col[2];
            Telefon = col[3];
            Oradij = int.Parse(col[4]);
        }
    }
}
