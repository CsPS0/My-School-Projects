using System.Text;

namespace teremLib
{
    public class Orarend
    {
        private List<Foglalas> foglalasok;

        public Orarend()
        {
            foglalasok = new List<Foglalas>();
        }

        public bool FoglaltE(DateTime kezdet, int idotartam)
        {
            DateTime vege = kezdet.AddMinutes(idotartam);
            return foglalasok.Any(f => kezdet < f.Vege && vege > f.Kezdete);
        }

        public static Orarend operator +(Orarend orarend, Foglalas ujFoglalas)
        {
            if (orarend.FoglaltE(ujFoglalas.Kezdete, (int)(ujFoglalas.Vege - ujFoglalas.Kezdete).TotalMinutes))
            {
                throw new FoglalasException();
            }
            orarend.foglalasok.Add(ujFoglalas);
            return orarend;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var rendezettFoglalasok = foglalasok.OrderBy(f => f.Kezdete);
            foreach (var foglalas in rendezettFoglalasok)
            {
                sb.AppendLine(foglalas.ToString());
            }
            return sb.ToString();
        }
    }
}