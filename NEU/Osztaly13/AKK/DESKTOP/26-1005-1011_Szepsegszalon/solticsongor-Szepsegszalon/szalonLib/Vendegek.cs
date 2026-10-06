namespace szalonLib
{
    public class Vendegek
    {
        public int VendegID {  get; init; }
        public string Nev {  get; init; }
        public string Cim {  get; init; }
        public string Telefon { get; init; }

        public Vendegek(string data)
        {
            string[] datas = data.Split(";");
            VendegID = int.Parse(datas[0]);
            Nev = datas[1];
            Cim = datas[2];
            Telefon = datas[3];
        }
    }
}
