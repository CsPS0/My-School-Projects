namespace GepkocsiLib
{
    public class Gepkocsi
    {
        public string Rendszam { get; set; }
        public string Nev { get; set; }
        public string Cim { get; set; }
        public string Gyartmany { get; set; }
        public string Tipus { get; set; }
        public double Urtartalom { get; set; }

        public Gepkocsi(string adat)
        {
            string[] sor = adat.Split(";");
            Rendszam = sor[0];
            Nev = sor[1];
            Cim = sor[2];
            Gyartmany = sor[3];
            Tipus = sor[4];
            Urtartalom = double.Parse(sor[5]);
        }
        public int Kerulet()
        {
            if (Cim[0] != '1')
            {
                return -1;
            }
            else
            {
                string ker = Cim[1..2]; //<-- 2. és 3. index között
                return int.Parse(ker);
            }
        }
    }
}