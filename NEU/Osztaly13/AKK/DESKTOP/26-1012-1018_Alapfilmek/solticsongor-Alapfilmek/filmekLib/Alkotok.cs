namespace filmekLib
{
    public class Alkotok
    {
        public int AlkotoAzon {  get; set; }
        public string Nev {  get; set; }
        public DateOnly Szuletett { get; set; }
        public DateOnly Elhunyt { get; set; }

        public Alkotok(string data)
        {
            string[] datas = data.Split(";");
            AlkotoAzon = int.Parse(datas[0]);
            Nev = datas[1];
            Szuletett = DateOnly.Parse(datas[2]);
            Elhunyt = DateOnly.Parse(datas[3]);
        }
    }
}
