using fleatsLib;

namespace FleatTests;

public class BuszTests
{
    [Test]
    public void Konstruktor_ErvenyesAdatokkal_HelyesenInicializal()
    {
        var b = new Busz("ABC-123", 50);

        Assert.That(b.Rendszam, Is.EqualTo("ABC-123"));
        Assert.That(b.Ferohely, Is.EqualTo(50));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(0));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(0));
        Assert.That(b.SzabadHelyek, Is.EqualTo(50));
    }

    [Test]
    public void Konstruktor_ErvenytelenRendszam_ArgumentExceptiontDob()
    {
        Assert.That(() => new Busz("", 50), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Konstruktor_ErvenytelenFerohely_ArgumentOutOfRangeExceptiontDob()
    {
        Assert.That(() => new Busz("ABC-123", 0), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void MegallobanKezeles_CsakFelszallas_HelyesenModositjaAzUtasszamot()
    {
        var b = new Busz("ABC-123", 50);

        int left = b.MegallobanKezeles(0, 15);

        Assert.That(left, Is.EqualTo(0));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(15));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(15));
        Assert.That(b.SzabadHelyek, Is.EqualTo(35));
    }

    [Test]
    public void MegallobanKezeles_CsakLeszallas_HelyesenCsokkentiAzUtasszamot()
    {
        var b = new Busz("ABC-123", 50);
        b.MegallobanKezeles(0, 20);

        int left = b.MegallobanKezeles(8, 0);

        Assert.That(left, Is.EqualTo(0));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(12));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(20));
        Assert.That(b.SzabadHelyek, Is.EqualTo(38));
    }

    [Test]
    public void MegallobanKezeles_EgyidejuFelszallasEsLeszallas()
    {
        var b = new Busz("ABC-123", 50);
        b.MegallobanKezeles(0, 20);

        int left = b.MegallobanKezeles(5, 10);

        Assert.That(left, Is.EqualTo(0));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(25));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(30));
        Assert.That(b.SzabadHelyek, Is.EqualTo(25));
    }

    [Test]
    public void MegallobanKezeles_TobbLeszalloMintAmiFennVan_InvalidOperationExceptiontDob()
    {
        var b = new Busz("ABC-123", 50);
        b.MegallobanKezeles(0, 5);

        Assert.That(() => b.MegallobanKezeles(6, 0), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void MegallobanKezeles_NegativLeszallo_ArgumentOutOfRangeExceptiontDob()
    {
        var b = new Busz("ABC-123", 50);

        Assert.That(() => b.MegallobanKezeles(-1, 5), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void MegallobanKezeles_NegativFelszallo_ArgumentOutOfRangeExceptiontDob()
    {
        var b = new Busz("ABC-123", 50);

        Assert.That(() => b.MegallobanKezeles(0, -1), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void MegallobanKezeles_TobbFelszalloMintSzabadHely_LentMaradnakUtasok()
    {
        var b = new Busz("ABC-123", 30);
        b.MegallobanKezeles(0, 20);

        int left = b.MegallobanKezeles(0, 15);

        Assert.That(left, Is.EqualTo(5));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(30));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(30));
        Assert.That(b.SzabadHelyek, Is.EqualTo(0));
    }

    [Test]
    public void MegallobanKezeles_PontosanKapacitasigTolt()
    {
        var b = new Busz("ABC-123", 40);

        int left = b.MegallobanKezeles(0, 40);

        Assert.That(left, Is.EqualTo(0));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(40));
        Assert.That(b.SzabadHelyek, Is.EqualTo(0));
    }

    [Test]
    public void MegallobanKezeles_TeljesenTeleBuszraNemTudFelszallniSenki()
    {
        var b = new Busz("ABC-123", 25);
        b.MegallobanKezeles(0, 25);

        int left = b.MegallobanKezeles(0, 10);

        Assert.That(left, Is.EqualTo(10));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(25));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(25));
    }

    [Test]
    public void MegallobanKezeles_LeszallasUtanSzabadulFelHelyFelszalloknak()
    {
        var b = new Busz("ABC-123", 20);
        b.MegallobanKezeles(0, 20);

        int left = b.MegallobanKezeles(5, 8);

        Assert.That(left, Is.EqualTo(3));
        Assert.That(b.AktualisUtasszam, Is.EqualTo(20));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(25));
    }

    [Test]
    public void ResetNapiAdatok_NullazzaAzUtasokatEsAzOsszesitest()
    {
        var b = new Busz("ABC-123", 50);
        b.MegallobanKezeles(0, 30);

        b.ResetNapiAdatok();

        Assert.That(b.AktualisUtasszam, Is.EqualTo(0));
        Assert.That(b.OsszesSzallitottUtas, Is.EqualTo(0));
        Assert.That(b.SzabadHelyek, Is.EqualTo(50));
    }
}

public class BuszflottaTests
{
    [Test]
    public void Konstruktor_UresFlotta_HelyesKezdoallapot()
    {
        var fleet = new Buszflotta();

        Assert.That(fleet.BuszokSzama, Is.EqualTo(0));
        Assert.That(fleet.Buszok, Is.Empty);
    }

    [Test]
    public void Konstruktor_KezdoBuszokkal_HelyesenInicializal()
    {
        var list = new List<Busz>
        {
            new Busz("AAA-111", 30),
            new Busz("BBB-222", 50)
        };

        var fleet = new Buszflotta(list);

        Assert.That(fleet.BuszokSzama, Is.EqualTo(2));
        Assert.That(fleet.Buszok.Count, Is.EqualTo(2));
    }

    [Test]
    public void Konstruktor_NullKezdoLista_ArgumentNullExceptiontDob()
    {
        Assert.That(() => new Buszflotta(null!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void BuszHozzaadas_SikeresHozzaadas()
    {
        var fleet = new Buszflotta();
        var b = new Busz("AAA-111", 40);

        fleet.BuszHozzaadas(b);

        Assert.That(fleet.BuszokSzama, Is.EqualTo(1));
        Assert.That(fleet.Buszok[0].Rendszam, Is.EqualTo("AAA-111"));
    }

    [Test]
    public void BuszHozzaadas_NullBusz_ArgumentNullExceptiontDob()
    {
        var fleet = new Buszflotta();

        Assert.That(() => fleet.BuszHozzaadas(null!), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void BuszHozzaadas_MarLetezoRendszam_ArgumentExceptiontDob()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));

        Assert.That(() => fleet.BuszHozzaadas(new Busz("aaa-111", 50)), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void KeresBusz_LetezoRendszam_VisszaadjaABuszt()
    {
        var fleet = new Buszflotta();
        var b = new Busz("AAA-111", 40);
        fleet.BuszHozzaadas(b);

        var res = fleet.KeresBusz("aaa-111");

        Assert.That(res, Is.Not.Null);
        Assert.That(res!.Rendszam, Is.EqualTo("AAA-111"));
    }

    [Test]
    public void KeresBusz_NemLetezoVagyErvenytelenRendszam_NulltAdVissza()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));

        Assert.That(fleet.KeresBusz("XYZ-999"), Is.Null);
    }

    [Test]
    public void MegalloEsemeny_NemLetezoBusz_KeyNotFoundExceptiontDob()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));

        Assert.That(() => fleet.MegalloEsemeny("ZZZ-999", "Deák tér", 0, 5), Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void MegalloEsemeny_ErvenytelenMegalloNev_ArgumentExceptiontDob()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));

        Assert.That(() => fleet.MegalloEsemeny("AAA-111", "", 0, 5), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void MegalloEsemeny_Sikeres_RogzitiAStatisztikakat()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));

        int left = fleet.MegalloEsemeny("AAA-111", "Deák tér", 0, 15);

        Assert.That(left, Is.EqualTo(0));
        Assert.That(fleet.GetFelszalltUtasok("Deák tér"), Is.EqualTo(15));
        Assert.That(fleet.GetLeszalltUtasok("Deák tér"), Is.EqualTo(0));
    }

    [Test]
    public void MegalloEsemeny_TobbMegalloEsTobbBusz_HelyesOsszesitesek()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));
        fleet.BuszHozzaadas(new Busz("BBB-222", 60));

        fleet.MegalloEsemeny("AAA-111", "Astoria", 0, 10);
        fleet.MegalloEsemeny("BBB-222", "Astoria", 0, 15);

        fleet.MegalloEsemeny("AAA-111", "Blaha", 4, 8);
        fleet.MegalloEsemeny("BBB-222", "Blaha", 5, 12);

        Assert.That(fleet.GetFelszalltUtasok("Astoria"), Is.EqualTo(25));
        Assert.That(fleet.GetLeszalltUtasok("Astoria"), Is.EqualTo(0));

        Assert.That(fleet.GetFelszalltUtasok("Blaha"), Is.EqualTo(20));
        Assert.That(fleet.GetLeszalltUtasok("Blaha"), Is.EqualTo(9));
    }

    [Test]
    public void MegalloEsemeny_Tultoltottseg_CsakAFelszalltUtasokatRogzitiAMegalloban()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 10));

        int left = fleet.MegalloEsemeny("AAA-111", "Oktogon", 0, 14);

        Assert.That(left, Is.EqualTo(4));
        Assert.That(fleet.GetFelszalltUtasok("Oktogon"), Is.EqualTo(10));
    }

    [Test]
    public void GetFelszalltUtasok_Es_GetLeszalltUtasok_IsmeretlenMegallo_NullatAd()
    {
        var fleet = new Buszflotta();

        Assert.That(fleet.GetFelszalltUtasok("Ismeretlen"), Is.EqualTo(0));
        Assert.That(fleet.GetLeszalltUtasok("Ismeretlen"), Is.EqualTo(0));
    }

    [Test]
    public void GetBuszOsszesSzallitottUtas_HelyesErteketAd()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 40));
        fleet.BuszHozzaadas(new Busz("BBB-222", 40));

        fleet.MegalloEsemeny("AAA-111", "Megálló 1", 0, 15);
        fleet.MegalloEsemeny("AAA-111", "Megálló 2", 5, 10);
        fleet.MegalloEsemeny("BBB-222", "Megálló 1", 0, 20);

        Assert.That(fleet.GetBuszOsszesSzallitottUtas("AAA-111"), Is.EqualTo(25));
        Assert.That(fleet.GetBuszOsszesSzallitottUtas("BBB-222"), Is.EqualTo(20));
    }

    [Test]
    public void GetBuszOsszesSzallitottUtas_NemLetezoBusz_KeyNotFoundExceptiontDob()
    {
        var fleet = new Buszflotta();

        Assert.That(() => fleet.GetBuszOsszesSzallitottUtas("ZZZ-999"), Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void GetFlottaOsszesSzallitottUtas_MindenBusztOsszegez()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 30));
        fleet.BuszHozzaadas(new Busz("BBB-222", 30));

        fleet.MegalloEsemeny("AAA-111", "Megálló 1", 0, 20);
        fleet.MegalloEsemeny("BBB-222", "Megálló 1", 0, 15);

        Assert.That(fleet.GetFlottaOsszesSzallitottUtas(), Is.EqualTo(35));
    }

    [Test]
    public void NapVegeReset_NullazzaAFlottaEsMegallokAdatait()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 30));
        fleet.MegalloEsemeny("AAA-111", "Deák tér", 0, 15);

        fleet.NapVegeReset();

        Assert.That(fleet.GetFelszalltUtasok("Deák tér"), Is.EqualTo(0));
        Assert.That(fleet.GetLeszalltUtasok("Deák tér"), Is.EqualTo(0));
        Assert.That(fleet.GetBuszOsszesSzallitottUtas("AAA-111"), Is.EqualTo(0));
        Assert.That(fleet.GetFlottaOsszesSzallitottUtas(), Is.EqualTo(0));
        Assert.That(fleet.Buszok[0].AktualisUtasszam, Is.EqualTo(0));
    }

    [Test]
    public void GetOsszesFelszallasMegallonkent_Es_GetOsszesLeszallasMegallonkent_HelyesSzotaratAd()
    {
        var fleet = new Buszflotta();
        fleet.BuszHozzaadas(new Busz("AAA-111", 50));
        fleet.MegalloEsemeny("AAA-111", "Megálló 1", 0, 10);
        fleet.MegalloEsemeny("AAA-111", "Megálló 2", 3, 5);

        var ons = fleet.GetOsszesFelszallasMegallonkent();
        var offs = fleet.GetOsszesLeszallasMegallonkent();

        Assert.That(ons.Count, Is.EqualTo(2));
        Assert.That(ons["Megálló 1"], Is.EqualTo(10));
        Assert.That(ons["Megálló 2"], Is.EqualTo(5));
        Assert.That(offs.Count, Is.EqualTo(2));
        Assert.That(offs["Megálló 1"], Is.EqualTo(0));
        Assert.That(offs["Megálló 2"], Is.EqualTo(3));
    }
}
