namespace dobozokLib
{
    public class ValogatasDoboz : TeasDoboz
    {
        private Filterek osszesFilter;
        private List<Filter> tartalom;

        public ValogatasDoboz(Filterek filterek) : base(0)
        {
            osszesFilter = filterek;
            tartalom = new List<Filter>();
        }

        private ValogatasDoboz(Filterek filterek, List<Filter> tartalomInput, int darabSzam) : base(darabSzam)
        {
            osszesFilter = filterek;
            tartalom = tartalomInput;
        }

        public override int Ar => tartalom.Sum(f => f.Ar) + 100;

        public string FilterTipusokString => string.Join(", ", tartalom.Select(f => f.Tipus).Distinct());

        public override string Nev => $"Válogatás tea ({FilterTipusokString})";

        public static ValogatasDoboz operator +(ValogatasDoboz doboz, string id)
        {
            var filter = doboz.osszesFilter[id];
            if (filter == null)
            {
                throw new HibasAzonositoException();
            }

            var ujTartalom = new List<Filter>(doboz.tartalom);
            ujTartalom.Add(filter);

            return new ValogatasDoboz(doboz.osszesFilter, ujTartalom, doboz.DarabSzam + 1);
        }
    }
}