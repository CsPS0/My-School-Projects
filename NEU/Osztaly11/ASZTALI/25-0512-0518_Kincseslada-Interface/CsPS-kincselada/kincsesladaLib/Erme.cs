namespace kincsesladaLib
{
    public class Erme : IComparable<Erme>, IKincs
    {
        private string tipus { get; set; }
        public string Nev => "érme";
        public string Leiras => $"Egy csillogó {tipus}érme";
        public string Tipus => tipus;
        public Erme(int tipus)
        {
            string[] ermek = ["arany", "ezüst", "réz"];
            this.tipus = ermek[tipus % 3];
        }
        public int Ertek => tipus switch
        {
            "arany" => 100,
            "ezüst" => 10,
            "réz" => 1,
            _ => 0
        };
        public int CompareTo(Erme? other)
        {
            if (other == null) return 1;
            return this.Ertek.CompareTo(other.Ertek);
        }
    }
}