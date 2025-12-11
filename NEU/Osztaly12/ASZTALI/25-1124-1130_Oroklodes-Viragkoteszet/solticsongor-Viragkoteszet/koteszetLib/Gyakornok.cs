namespace koteszetLib
{
    public class Gyakornok : Dolgozo
    {
        private readonly List<int> _kepesitesek;
        private static readonly Random Rnd = new Random();

        public override double Gyakorlottsag { get; }

        public override int MunkaraForditottIdo =>
            (int)Feladatok.Feladatok.Sum(f => f.ElkeszetesiIdo * (1 + (100 - Gyakorlottsag) / 100.0));

        public Gyakornok(int id, string nev, IEnumerable<string> kepesitesek) : base(id, nev)
        {
            _kepesitesek = kepesitesek.Select(int.Parse).ToList();
            Gyakorlottsag = Rnd.Next(7, 10) * 10;
        }

        public override void UjFeladatHozzaadasa(Termek termek)
        {
            if (_kepesitesek.Contains(termek.Id))
            {
                base.UjFeladatHozzaadasa(termek);
            }
            else
            {
                throw new HibasFeladatException(Id, termek.Id);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} (gyakornok)";
        }
    }
}