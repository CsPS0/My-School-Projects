namespace joalanyLib
{
    public class Szemely
    {
        public string Nev { get; set; }
        public DateTime SzuletesiDatum { get; set; }

        public int Kor
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - SzuletesiDatum.Year;
                if (SzuletesiDatum.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

    public Szemely(string nev, DateTime szuletesiDatum)
        {
            Nev = nev;
            SzuletesiDatum = szuletesiDatum;

            if (Kor < 14)
            {
                throw new HibasEletkorException();
            }
        }

    public override string ToString()
        {
            return $"{Nev}, {SzuletesiDatum.ToShortDateString()}, {Kor} éves";
        }
    }
}