namespace bukkMaratonLib
{
    public class Versenyzo
    {
        public string Rajtszam { get; set; }
        public string Kategoria { get; set; }
        public string Nev { get; set; }
        public string Egyesulet { get; set; }
        public string Ido { get; set; }
        public Versenytav Versenytav { get; set; }

        public TimeSpan Idotartam => TimeSpan.Parse(Ido);

        public Versenyzo(string sor)
        {
            var adatok = sor.Split(';');
            Rajtszam = adatok[0];
            Kategoria = adatok[1];
            Nev = adatok[2];
            Egyesulet = adatok[3];
            Ido = adatok[4];
            Versenytav = new Versenytav(Rajtszam);
        }
    }
}
