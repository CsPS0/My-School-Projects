namespace koteszetLib
{
    public class Viragkoto : Dolgozo
    {
        public override double Gyakorlottsag => 100;

        public override int MunkaraForditottIdo => Feladatok.Feladatok.Sum(f => f.ElkeszetesiIdo);

        public Viragkoto(int id, string nev) : base(id, nev) {}
    }
}