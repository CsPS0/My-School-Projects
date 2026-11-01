namespace koteszetLib
{
    public class Alapanyag
    {
        public string Azonosito { get; set; }
        public string Nev {  get; set; }
        public int Ar {  get; set; }
        public int ElkeszetesiIdo { get; set; }

        public Alapanyag(string adat)
        {
            string[] sorok = adat.Split(";");
            Azonosito = sorok[0];
            Nev = sorok[1];
            Ar = int.Parse(sorok[2]);
            ElkeszetesiIdo = int.Parse(sorok[3]);
        }
    }
}