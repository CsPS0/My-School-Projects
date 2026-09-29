using System.Data;
using System.Globalization;

namespace starTrekLib
{
    public class Tablak
    {
        public DataSet Adatbazis { get; } = new DataSet("StarTrek");

        public DataTable Fajok { get; } = new DataTable("Fajok");
        public DataTable HajoOsztalyok { get; } = new DataTable("HajoOsztalyok");
        public DataTable HajoSzerepek { get; } = new DataTable("HajoSzerepek");
        public DataTable Urhajok { get; } = new DataTable("Urhajok");

        public Tablak()
        {
            Adatbazis.Locale = new CultureInfo("hu-HU");

            Fajok.Columns.Add("FajID", typeof(int));
            Fajok.Columns.Add("FajNev", typeof(string));
            Fajok.PrimaryKey = new[] { Fajok.Columns["FajID"]! };

            HajoOsztalyok.Columns.Add("HajoOsztalyID", typeof(int));
            HajoOsztalyok.Columns.Add("HajoOsztalyNev", typeof(string));
            HajoOsztalyok.Columns.Add("SzerepID", typeof(int));
            HajoOsztalyok.PrimaryKey = new[] { HajoOsztalyok.Columns["HajoOsztalyID"]! };

            HajoSzerepek.Columns.Add("SzerepID", typeof(int));
            HajoSzerepek.Columns.Add("SzerepNev", typeof(string));
            HajoSzerepek.PrimaryKey = new[] { HajoSzerepek.Columns["SzerepID"]! };

            Urhajok.Columns.Add("UrhajoID", typeof(int));
            Urhajok.Columns.Add("Azonosito", typeof(string));
            Urhajok.Columns.Add("UrhajoNev", typeof(string));
            Urhajok.Columns.Add("HajoOsztalyID", typeof(int));
            Urhajok.Columns.Add("FajID", typeof(int));
            Urhajok.PrimaryKey = new[] { Urhajok.Columns["UrhajoID"]! };

            Adatbazis.Tables.AddRange(new[] { Fajok, HajoOsztalyok, HajoSzerepek, Urhajok });

            // 1:N kapcsolatok (idegen kulcs kényszer nélkül)
            Adatbazis.Relations.Add("Faj_Urhajo",
                Fajok.Columns["FajID"]!, Urhajok.Columns["FajID"]!, false);
            Adatbazis.Relations.Add("Osztaly_Urhajo",
                HajoOsztalyok.Columns["HajoOsztalyID"]!, Urhajok.Columns["HajoOsztalyID"]!, false);
            Adatbazis.Relations.Add("Szerep_Osztaly",
                HajoSzerepek.Columns["SzerepID"]!, HajoOsztalyok.Columns["SzerepID"]!, false);
        }

        // 4. feladat
        public int EnterpriseDarab()
        {
            return Urhajok.AsEnumerable()
                .Count(r => r.Field<string>("UrhajoNev")!.Contains("Enterprise"));
        }

        // 5. feladat
        public int OsztalyokSzerepSzerint(string szerepNev)
        {
            DataRow? szerep = HajoSzerepek.AsEnumerable()
                .FirstOrDefault(r => r.Field<string>("SzerepNev") == szerepNev);

            if (szerep == null) return -1;

            return szerep.GetChildRows("Szerep_Osztaly").Length;
        }

        // 6. feladat
        public List<(string Nev, int Darab)> LegtobbHajosOsztalyok(int db)
        {
            return HajoOsztalyok.AsEnumerable()
                .Select(o => (Nev: o.Field<string>("HajoOsztalyNev")!,
                              Darab: o.GetChildRows("Osztaly_Urhajo").Length))
                .OrderByDescending(x => x.Darab)
                .Take(db)
                .ToList();
        }

        // 8. feladat
        public DataView SzerepekRendezve()
        {
            return new DataView(HajoSzerepek, "", "SzerepNev ASC", DataViewRowState.CurrentRows);
        }

        // 9. feladat
        public DataTable UrhajokSzerepSzerint(int szerepId)
        {
            DataTable eredmeny = Urhajok.Clone();
            eredmeny.Locale = Adatbazis.Locale;

            foreach (DataRow hajo in Urhajok.Rows)
            {
                DataRow? osztaly = hajo.GetParentRow("Osztaly_Urhajo");
                if (osztaly != null && osztaly.Field<int>("SzerepID") == szerepId)
                {
                    eredmeny.ImportRow(hajo);
                }
            }
            return eredmeny;
        }

        // 10. feladat
        public string OsztalyNev(int urhajoId)
        {
            DataRow? hajo = Urhajok.Rows.Find(urhajoId);
            return hajo?.GetParentRow("Osztaly_Urhajo")?.Field<string>("HajoOsztalyNev") ?? "";
        }

        public string FajNev(int urhajoId)
        {
            DataRow? hajo = Urhajok.Rows.Find(urhajoId);
            return hajo?.GetParentRow("Faj_Urhajo")?.Field<string>("FajNev") ?? "";
        }
    }
}