namespace novenygyujtesLib
{
    public class NovenyFactory
    {
        readonly Random random = new();

        private readonly List<Virag> viragok = new()
        {
            new Virag("Pipacs", "Piros pipacs", "Mezei virág", 10, 'P'),
            new Virag("Margaréta", "Fehér margaréta", "Mezei virág", 8, 'M'),
            new Virag("Búzavirág", "Kék búzavirág", "Mezei virág", 12, 'B'),
            new Virag("Ibolya", "Lila ibolya", "Erdei virág", 15, 'I'),
            new Virag("Nárcisz", "Sárga nárcisz", "Kerti virág", 20, 'N')
        };

        private readonly List<Gyogynoveny> gyogynovenyek = new()
        {
            new Gyogynoveny("Kamilla", "Fehér kamilla", "Mezei gyógynövény", 25, 'K', "Gyulladáscsökkentő hatású"),
            new Gyogynoveny("Csalán", "Zöld csalán", "Mezei gyógynövény", 18, 'C', "Vértisztító hatású"),
            new Gyogynoveny("Citromfű", "Illatos citromfű", "Kerti gyógynövény", 30, 'F', "Nyugtató hatású"),
            new Gyogynoveny("Levendula", "Lila levendula", "Kerti gyógynövény", 35, 'L', "Alvásjavító hatású"),
            new Gyogynoveny("Kakukkfű", "Apró kakukkfű", "Hegyi gyógynövény", 22, 'T', "Köhögéscsillapító hatású")
        };

        private readonly List<Gomba> gombak = new()
        {
            new Gomba("Vargánya", "Barna vargánya", "Ehető gomba", 40, 'V', false),
            new Gomba("Szegfűgomba", "Mezei szegfűgomba", "Ehető gomba", 30, 'S', false),
            new Gomba("Gyilkos galóca", "Fehér gyilkos galóca", "Mérgező gomba", 0, 'G', true),
            new Gomba("Légyölő galóca", "Piros pettyes légyölő galóca", "Mérgező gomba", 0, 'X', true),
            new Gomba("Rókagomba", "Sárga rókagomba", "Ehető gomba", 35, 'R', false)
        };

        public INoveny Create()
        {
            int tipus = random.Next(3);

            switch (tipus)
            {
                case 0: return viragok[random.Next(viragok.Count)];
                case 1: return gyogynovenyek[random.Next(gyogynovenyek.Count)];
                default: return gombak[random.Next(gombak.Count)];
            }
        }
    }
}