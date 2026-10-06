namespace szalonLib
{
    public class Szakmak
    {
        public int SzakmaID { get; init; }
        public string Nev { get; init; }

        public Szakmak(string data)
        {
            string[] datas = data.Split(";");
            SzakmaID = int.Parse(datas[0]);
            Nev = datas[1];
        }
    }
}
