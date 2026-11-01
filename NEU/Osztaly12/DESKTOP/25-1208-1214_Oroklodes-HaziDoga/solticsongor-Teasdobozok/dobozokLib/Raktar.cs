namespace dobozokLib
{
    public class Raktar
    {
        private List<TeasDoboz> dobozok;

        public Raktar()
        {
            dobozok = new List<TeasDoboz>();
        }

        public void Hozzaad(TeasDoboz doboz)
        {
            dobozok.Add(doboz);
        }

        public List<TeasDoboz> Dobozok => dobozok;
    }
}