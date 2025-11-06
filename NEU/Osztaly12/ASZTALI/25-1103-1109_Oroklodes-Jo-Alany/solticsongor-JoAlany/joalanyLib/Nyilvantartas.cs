namespace joalanyLib
{
    public class Nyilvantartas
    {
        private List<Szemely> szemelyek = new List<Szemely>();

        public Szemely this[int index]
        {
            get { return szemelyek[index]; }
        }

        public void Hozzaad(Szemely szemely)
        {
            szemelyek.Add(szemely);
        }

        public IEnumerable<Diak> Diakok
        {
            get
            {
                return szemelyek.OfType<Diak>();
            }
        }

        public IEnumerable<Tanar> Tanarok
        {
            get
            {
                return szemelyek.OfType<Tanar>();
            }
        }
    }
}