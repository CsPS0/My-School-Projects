namespace szalonLib
{
    public class Alkalmazottak
    {
        public int AlkalmazottID { get; init; }
        public string Nev { get; init; }
        public int SzakmaID { get; init; }
        public string Telefon { get; init; }

        public Alkalmazottak(string data)
        {
            string[] datas = data.Split(";");
            AlkalmazottID = int.Parse(datas[0]);
            Nev = datas[1];
            SzakmaID = int.Parse(datas[2]);
            Telefon = datas[3];
        }
    }
}
