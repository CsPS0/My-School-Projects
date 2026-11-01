namespace solticsongor_Lift
{
    internal class Liftek
    {
        private readonly List<Lift> liftek = new();

        public Liftek(IEnumerable<Lift> liftekk)
        {
            this.liftek = new List<Lift>(liftekk);
        }

        public Lift this[int index]
        {
            get
            {
                if (index < 1 || index > liftek.Count)
                {
                    throw new ArgumentOutOfRangeException("Nincs ilyen sorszámú lift...");
                }
                return liftek[index - 1];
            }
        }
    }
}