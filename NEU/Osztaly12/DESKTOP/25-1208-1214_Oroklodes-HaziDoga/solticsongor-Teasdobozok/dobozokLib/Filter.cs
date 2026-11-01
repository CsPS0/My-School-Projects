namespace dobozokLib
{
    public class Filter
    {
        public string Azonosito { get; private set; }
        public string Tipus { get; private set; }
        public int Ar { get; private set; }

        public bool Gyogytea => Azonosito.StartsWith("z");

        public Filter(string azonosito, string tipus, int ar)
        {
            Azonosito = azonosito;
            Tipus = tipus;
            Ar = ar;
        }

        public override string ToString()
        {
            return $"{Tipus} ({Ar} Ft)";
        }
    }
}