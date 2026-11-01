namespace dobozokLib
{
    public class EgyszeruDoboz : TeasDoboz
    {
        private Filter filter;

        public EgyszeruDoboz(string id, int darabSzam, Filterek filterek) : base(darabSzam)
        {
            var f = filterek[id];
            if (f == null)
            {
                throw new HibasAzonositoException();
            }
            filter = f;
        }

        public override int Ar => (filter.Ar * DarabSzam) + 100;

        public override string Nev => $"{filter.Tipus} tea";
    }
}