namespace szeleromuvekLib
{
    public class Szeleromu
    {
        public string Regio { get; set; }
        public string Megye { get; set; }
        public string Telepules { get; set; }
        public int Darab { get; set; }
        public double Teljesitmeny { get; set; }
        public int Ev { get; set; }

        public Szeleromu(string sor)
        {
            string[] s = sor.Split(";");
            Regio = s[0];
            Megye = s[1];
            Telepules = s[2];
            Darab = int.Parse(s[3]);
            Teljesitmeny = double.Parse(s[4]);
            Ev = int.Parse(s[5]);
        }
    }
}