using System.Text.Json.Serialization;

namespace appleLib
{
    public class Alma : ISzimulacio
    {
        const int KEZDO_MERET = 5;
        const int NOVEKEDES_LEPES_IDO = 2;
        readonly (int, int) NOVEKEDES_MERET = (1, 3);

        const int ERESHATAR_MERET = 80;
        const int ERES_LEPES_IDO = 5;
        readonly (int, int) ERES_SZAZALEK = (5, 10);

        const int PERC = 60;
        readonly (int, int) ROHADAS_IDO = (2 * PERC, 5 * PERC);
        const int HALAL_IDO = 5 * PERC;

        public int Meret { get; private set; }
        public int EresSzazalek { get; private set; }
        public int KorokSzama { get; private set; }
        public int RohadasKorSzama { get; private set; }

        [JsonConstructor]
        public Alma(int meret, int eresSzazalek, int korokSzama, int rohadasKorSzama)
        {
            Meret = meret;
            EresSzazalek = eresSzazalek;
            KorokSzama = korokSzama;
            RohadasKorSzama = rohadasKorSzama;
        }

        public Alma()
        {
            Meret = KEZDO_MERET;
            EresSzazalek = 0;
            KorokSzama = 0;
            RohadasKorSzama = -1;
        }

        bool Megnott => Meret >= ERESHATAR_MERET;
        bool Rohad => KorokSzama >= RohadasKorSzama;

        [JsonIgnore]
        public bool EletbenVan => RohadasKorSzama < 0 || KorokSzama < RohadasKorSzama + HALAL_IDO;

        static int RandomIntervallum((int, int) intervallum) => Random.Shared.Next(intervallum.Item1, intervallum.Item2 + 1);

        void Novekedes()
        {
            if (Megnott) return;
            if (KorokSzama % NOVEKEDES_LEPES_IDO == 0)
            {
                Meret += RandomIntervallum(NOVEKEDES_MERET);
            }
        }

        void Eres()
        {
            if (!Megnott) return;
            if (KorokSzama % ERES_LEPES_IDO == 0 && EresSzazalek < 100)
            {
                EresSzazalek += RandomIntervallum(ERES_SZAZALEK);
                if (EresSzazalek >= 100)
                {
                    EresSzazalek = 100;
                    RohadasKorSzama = KorokSzama + RandomIntervallum(ROHADAS_IDO);
                }
            }
        }

        public void Kor()
        {
            ++KorokSzama;
            Novekedes();
            Eres();
        }

        public override string ToString()
        {
            return EresSzazalek switch
            {
                _ when !Megnott => $"Gyümölcskezdemény: {Meret} mm",
                < 100 => $"Gyümölcs: {EresSzazalek}% érett",
                _ when !Rohad => "Érett gyümölcs",
                _ when EletbenVan => "Ez az alma megrohadt",
                _ => "Ez az alma meghalt"
            };
        }
    }
}