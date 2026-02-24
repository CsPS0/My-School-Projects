namespace solticsongor_JatekNyilvantarto
{
    public class Jatek
    {
        public string Cim { get; set; } = string.Empty;
        public string Mufaj { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Cim} - {Mufaj}";
        }
    }
}
