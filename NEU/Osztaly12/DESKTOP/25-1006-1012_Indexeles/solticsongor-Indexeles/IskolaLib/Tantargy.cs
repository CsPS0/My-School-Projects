namespace IskolaLib
{
    public class Tantargy
    {
        public string Nev { get; set; }
        public string Kod { get; set; }

        public Tantargy(string nev, string kod)
        {
            Nev = nev;
            Kod = kod;
        }
    }
}