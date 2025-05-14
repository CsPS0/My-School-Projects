namespace kincsesladaLib
{
    public class Dragako : IComparable<Dragako>, IKincs
    {
        private string tipus;
        public int Nagysag { get; set; }

        public string Nev => tipus;
        public string Leiras => $"Egy gyönyörű {MeretToString()} {tipus}";
        public string Tipus => tipus;
        public int Ertek => SzamolErtek();

        private string MeretToString()
        {
            return Nagysag switch
            {
                1 => "kisméretű",
                2 => "közepes méretű",
                3 => "nagyméretű",
                _ => "ismeretlen méretű"
            };
        }

        private int SzamolErtek()
        {
            int alapErtek = tipus switch
            {
                "zafír" => 500,
                "smaragd" => 400,
                "gyémánt" => 1000,
                _ => 0
            };

            return Nagysag switch
            {
                1 => alapErtek,
                2 => alapErtek * 4,
                3 => alapErtek * 9,
                _ => 0
            };
        }

        public Dragako(int tipus, int nagysag)
        {
            string[] dragakovek = ["zafír", "smaragd", "gyémánt"];
            this.tipus = dragakovek[tipus % 3];
            this.Nagysag = Math.Clamp(nagysag, 1, 3);
        }

        public override string ToString()
        {
            return Leiras + ".";
        }

        public int CompareTo(Dragako? other)
        {
            if (other == null) return 1;
            return this.Ertek.CompareTo(other.Ertek);
        }
    }
}