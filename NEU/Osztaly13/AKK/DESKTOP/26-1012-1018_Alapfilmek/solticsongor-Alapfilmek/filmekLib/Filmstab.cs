namespace filmekLib
{
    public class Filmstab
    {
        public int FilmAzon {  get; set; }
        public int MunkakorAzon { get; set; }
        public int AlkotoAzon { get; set; }

        public Filmstab(string data)
        {
            string[] datas = data.Split(";");
            FilmAzon = int.Parse(datas[0]);
            MunkakorAzon = int.Parse(datas[1]);
            AlkotoAzon = int.Parse(datas[2]);
        }
    }
}
