using System.Text;

namespace LovaglasLibrary
{
    public class DataStore
    {
        List<Szint> Szint = new();
        List<Kepzes> Kepzes = new();
        List<Oktato> Oktato = new();
        List<Orarend> Orarend = new();

        public DataStore(string path = "Forrasok")
        {
            Szint = Path.Combine(File.ReadAllLines($"{path}/szint.csv")
                .Skip(1)
                .Select(x => new Szint)
                .ToList());
            Kepzes = Path.Combine(File.ReadAllLines($"{path}/kepzes.csv")
                .Skip(1)
                .Select(x => new Kepzes)
                .ToList());
            Oktato = Path.Combine(File.ReadLines($"{path}/oktato.csv")
                .Skip(1)
                .Select(x => new Oktato)
                .ToList());
            Orarend = Path.Combine(File.ReadAllLines($"{path}/orarend.csv")
                .Skip(1)
                .Select(x => new Orarend)
                .ToList());
        }

        public void Oramegtartasa()
        {
            
        }

        public void Feladat05()
        {
            string legalabb45 = Szint.Where(x => x.Orarend.Hossz >= 45);
        }

        public void Feladat06()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i <= 3; i++)
            {
                stringBuilder.AppendLine($"{Nev}: {Oradij} Ft");
                i++;
            }
            Console.WriteLine(stringBuilder.ToString());
        }

        public void Feladat07()
        {
            string keresettPenz = Oktato.Select(x => x.Oradij).OrderByDescending(x => x).First(3);
        }
    }
}
