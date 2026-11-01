namespace teremLib
{
    public class Foglalas : IFoglalas
    {
        public int HelyszinAzonosito { get; set; }
        public DateTime Kezdete { get; set; }
        public DateTime Vege { get; set; }
        public string TanarAzonosito { get; set; }

        public Foglalas(DateTime kezd, int idotartam, int helyAzon, string tanAzon)
        {
            if (idotartam <= 0 || idotartam % 15 != 0)
            {
                throw new IdotartamException();
            }

            this.Kezdete = kezd;
            this.Vege = kezd.AddMinutes(idotartam);
            this.HelyszinAzonosito = helyAzon;
            this.TanarAzonosito = tanAzon;
        }

        public override string ToString()
        {
            return $"{Kezdete:yyyy.MM.dd. HH:mm:ss} - {Vege:HH:mm:ss} {TanarAzonosito}";
        }
    }
}