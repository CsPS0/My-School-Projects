using System.Text;

namespace teremLib
{
    public sealed class SpecialisTerem : Terem
    {
        public int TakaritasIdo { get; set; }

        public SpecialisTerem(int azon, int feroh, int takaritasIdo) : base(azon, feroh)
        {
            TakaritasIdo = takaritasIdo;
        }

        public override void IdopontFoglalas(Foglalas foglalas)
        {
            int foglalasIdo = (int)(foglalas.Vege - foglalas.Kezdete).TotalMinutes;
            DateTime takaritasKezd = foglalas.Vege;
            Foglalas takaritasFoglalas = new Foglalas(takaritasKezd, TakaritasIdo, foglalas.HelyszinAzonosito, "takaritas");

            bool foglalasLehetseges = !Orarend.FoglaltE(foglalas.Kezdete, foglalasIdo);
            bool takaritasLehetseges = !Orarend.FoglaltE(takaritasKezd, TakaritasIdo);

            if (foglalasLehetseges && takaritasLehetseges)
            {
                Orarend = Orarend + foglalas;
                Orarend = Orarend + takaritasFoglalas;
            }
            else
            {
                throw new FoglalasException();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"t{Azonosito} (takarítási idő: {TakaritasIdo} perc):");
            sb.AppendLine("Foglalt időpontok:");
            sb.Append(Orarend.ToString());
            return sb.ToString();
        }
    }
}