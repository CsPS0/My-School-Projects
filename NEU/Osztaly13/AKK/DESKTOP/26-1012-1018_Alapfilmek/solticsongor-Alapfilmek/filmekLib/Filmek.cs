namespace filmekLib
{
    public class Filmek
    {
        public int FilmAzon { get; set; }
        public string Cim { get; set; }
        public string Ev { get; set; }
        public string Szines { get; set; }
        public string Mufaj { get; set; }
        public int Hossz { get; set; }

        public Filmek(string data)
        {
            string[] datas = data.Split(";");
            FilmAzon = int.Parse(datas[0]);
            Cim = datas[1];
            Ev = datas[2];
            Szines = datas[3];
            Mufaj = datas[4];
            Hossz = int.Parse(datas[5]);
        }
    }
}
