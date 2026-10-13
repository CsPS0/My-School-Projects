namespace filmekLib
{
    public class Munkakor
    {
        public int MunkakorAzon { get; set; }
        public int Munkakorr { get; set; }

        public Munkakor(string data)
        {
            string[] datas = data.Split(";");
            MunkakorAzon = int.Parse(datas[0]);
            Munkakorr = int.Parse(datas[1]);
        }
    }
}
