namespace cukraszdaLib
{
    public sealed class AlapSutemeny : Sutemeny
    {
        public AlapSutemeny(string azonosito, string tipus, string megnevezes, KeszitesAdatok keszitesAdatok)
            : base(azonosito, tipus, megnevezes, keszitesAdatok)
        {
        }

        public override int ElkeszitesiIdo => KeszitesAdatok[Tipus].ElkeszitesiIdo;
    }
}
