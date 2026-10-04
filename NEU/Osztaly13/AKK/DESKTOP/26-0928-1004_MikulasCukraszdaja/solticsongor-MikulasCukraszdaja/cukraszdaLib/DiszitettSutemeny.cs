namespace cukraszdaLib
{
    public sealed class DiszitettSutemeny : Sutemeny
    {
        public IEnumerable<string> Osszetevok { get; }

        public DiszitettSutemeny(string azonosito, string tipus, string megnevezes, KeszitesAdatok keszitesAdatok, IEnumerable<string> osszetevok)
            : base(azonosito, tipus, megnevezes, keszitesAdatok)
        {
            Osszetevok = osszetevok.ToList();
        }

        public string TipusMeghatarozas(string osszetevoAzonosito)
        {
            if (string.IsNullOrEmpty(osszetevoAzonosito))
            {
                return string.Empty;
            }

            if (osszetevoAzonosito[0] == 'd')
            {
                return osszetevoAzonosito;
            }

            return osszetevoAzonosito[0].ToString();
        }

        public override int ElkeszitesiIdo =>
            Osszetevok.Sum(o => KeszitesAdatok[TipusMeghatarozas(o)].ElkeszitesiIdo);
    }
}
