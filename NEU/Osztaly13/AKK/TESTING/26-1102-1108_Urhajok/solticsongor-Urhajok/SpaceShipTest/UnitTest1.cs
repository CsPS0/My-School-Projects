namespace SpaceShipTest;

using ConsoleApp;

public class UrhajoTesztek
{
    [Test]
    public void UrhajoOsztaly_ErvenyesNev_SikeresPeldanyositas()
    {
        var osztaly = new UrhajoOsztaly("Felfedező");

        Assert.That(osztaly.Nev, Is.EqualTo("Felfedező"), "Nem érvényes felfedező!");
    }

    [Test]
    public void UrhajoOsztaly_ErvenytelenNev_KiveteltDob()
    {
        Assert.That(() => new UrhajoOsztaly(""), Throws.TypeOf<ArgumentException>(), "Üres névnél nem dobott kivételt!");
    }

    [Test]
    public void UrhajoTipus_ErvenyesAdatok_SikeresPeldanyositas()
    {
        var elvart = new Dictionary<string, int> { { "navigáció", 2 } };
        var tipus = new UrhajoTipus("Felderítő", "Mélyűri felderítő űrhajó", elvart);

        Assert.That(tipus.Nev, Is.EqualTo("Felderítő"), "A típus neve nem megfelelő!");
        Assert.That(tipus.Leiras, Is.EqualTo("Mélyűri felderítő űrhajó"), "A típus leírása nem megfelelő!");
        Assert.That(tipus.ElvartSzakertelmek["navigáció"], Is.EqualTo(2), "A navigáció elvárt szintje nem megfelelő!");
    }

    [Test]
    public void UrhajoTipus_UresElvartSzakertem_NemDobKivetelt()
    {
        var tipus = new UrhajoTipus("Teherszállító", "Nehéz teherszállító");

        Assert.That(tipus.ElvartSzakertelmek, Is.Empty, "Nem üres az elvárt szakértelmek listája!");
    }

    [Test]
    public void UrhajoTipus_ErvenytelenNev_KiveteltDob()
    {
        Assert.That(() => new UrhajoTipus("", "Leírás"), Throws.TypeOf<ArgumentException>(), "Üres típusnévnél nem dobott kivételt!");
    }

    [Test]
    public void LegenysegiTag_ErvenyesAdatok_HelyesErtekek()
    {
        var szakertelmek = new Dictionary<string, int>
        {
            { "diplomácia", 1 },
            { "navigáció", 2 }
        };

        var tag = new LegenysegiTag("John Smith", "amerikai", new DateTime(2001, 11, 2), "férfi", szakertelmek);

        Assert.That(tag.Nev, Is.EqualTo("John Smith"), "A tag neve nem megfelelő!");
        Assert.That(tag.Nemzetiseg, Is.EqualTo("amerikai"), "A nemzetiség nem megfelelő!");
        Assert.That(tag.SzuletesiDatum, Is.EqualTo(new DateTime(2001, 11, 2)), "A születési dátum nem megfelelő!");
        Assert.That(tag.Nem, Is.EqualTo("férfi"), "A nem nem megfelelő!");
        Assert.That(tag.GetSzakertelemSzint("navigáció"), Is.EqualTo(2), "A navigáció szintje nem megfelelő!");
        Assert.That(tag.GetSzakertelemSzint("diplomácia"), Is.EqualTo(1), "A diplomácia szintje nem megfelelő!");
        Assert.That(tag.GetSzakertelemSzint("harcászat"), Is.EqualTo(0), "Az ismeretlen szakértelem szintje nem nulla!");
        Assert.That(tag.HozzaRendeltHajo, Is.Null, "Kezdetben nincs hozzárendelt hajó!");
    }

    [Test]
    public void LegenysegiTag_ErvenytelenNev_KiveteltDob()
    {
        Assert.That(() => new LegenysegiTag("", "magyar", DateTime.Now, "férfi"), Throws.TypeOf<ArgumentException>(), "Üres tagnévnél nem dobott kivételt!");
    }

    [Test]
    public void Urhajo_ErvenytelenFerohely_KiveteltDob()
    {
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");

        Assert.That(() => new Urhajo("Kutató 11", tipus, osztaly, 2026, 0), Throws.TypeOf<ArgumentException>(), "0 férőhelynél nem dobott kivételt!");
    }

    [Test]
    public void Urhajo_ElvartSzakertelemNelkul_MindigIndithato()
    {
        var osztaly = new UrhajoOsztaly("Teher");
        var tipus = new UrhajoTipus("Bárka", "Egyszerű szállító");
        var hajo = new Urhajo("Cargo 1", tipus, osztaly, 2024, 5);

        Assert.That(hajo.Indithato, Is.True, "Elvárt szakértelem nélkül a hajónak indíthatónak kell lennie!");
    }

    [Test]
    public void Urhajo_NemElegendoszakertelmekkel_NemIndithato()
    {
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás", new Dictionary<string, int> { { "navigáció", 2 } });
        var hajo = new Urhajo("Kutató 11", tipus, osztaly, 2025, 3);

        var tag = new LegenysegiTag("Kovács Péter", "magyar", new DateTime(1995, 5, 10), "férfi",
            new Dictionary<string, int> { { "navigáció", 2 } });

        hajo.Legenyseg.Add(tag);

        Assert.That(hajo.Indithato, Is.False, "Nem elegendő szakértelemnél a hajó nem indítható!");
    }

    [Test]
    public void Urhajo_Elegendoszakertelmekkel_Indithato()
    {
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás", new Dictionary<string, int> { { "navigáció", 2 } });
        var hajo = new Urhajo("Kutató 11", tipus, osztaly, 2025, 3);

        var tag1 = new LegenysegiTag("John Smith", "amerikai", new DateTime(2001, 11, 2), "férfi",
            new Dictionary<string, int> { { "navigáció", 2 } });
        var tag2 = new LegenysegiTag("Nagy Anna", "magyar", new DateTime(1998, 3, 14), "nő",
            new Dictionary<string, int> { { "navigáció", 1 } });

        hajo.Legenyseg.Add(tag1);
        hajo.Legenyseg.Add(tag2);

        Assert.That(hajo.Indithato, Is.True, "Elegendő szakértelemnél a hajónak indíthatónak kell lennie!");
    }

    [Test]
    public void Urhajo_TobbElvartSzakertelemKozulEgyikHianyzik_NemIndithato()
    {
        var osztaly = new UrhajoOsztaly("Hadi");
        var tipus = new UrhajoTipus("Romboló", "Csatahajó", new Dictionary<string, int>
        {
            { "navigáció", 2 },
            { "tüzérség", 3 }
        });
        var hajo = new Urhajo("Titan", tipus, osztaly, 2026, 4);

        var tag = new LegenysegiTag("Kovács Béla", "magyar", new DateTime(1990, 1, 1), "férfi",
            new Dictionary<string, int> { { "navigáció", 5 } });

        hajo.Legenyseg.Add(tag);

        Assert.That(hajo.Indithato, Is.False, "Hiányzó szakértelemnél a hajó nem indítható!");
    }

    [Test]
    public void Urparancsnoksag_LegenysegHozzarendeles_Sikeres()
    {
        var parancsnoksag = new Urparancsnoksag();
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");
        var hajo = new Urhajo("Kutató 11", tipus, osztaly, 2026, 2);
        var tag = new LegenysegiTag("John Smith", "amerikai", new DateTime(2001, 11, 2), "férfi");

        parancsnoksag.HajoRegisztracio(hajo);
        parancsnoksag.LegenysegiTagRegisztracio(tag);

        Assert.That(parancsnoksag.SzabadLegenyseg, Contains.Item(tag), "A regisztrált tag nem került a szabadok közé!");

        parancsnoksag.Hozzarendel(hajo, tag);

        Assert.That(hajo.Legenyseg, Contains.Item(tag), "A tag nem került a hajó legénységébe!");
        Assert.That(parancsnoksag.SzabadLegenyseg, Does.Not.Contain(tag), "A tag nem került ki a szabadok közül!");
        Assert.That(tag.HozzaRendeltHajo, Is.EqualTo(hajo), "A hozzárendelt hajó értéke nem egyezik!");
    }

    [Test]
    public void Urparancsnoksag_BeteltFerohely_KiveteltDob()
    {
        var parancsnoksag = new Urparancsnoksag();
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");
        var hajo = new Urhajo("Kutató 11", tipus, osztaly, 2026, 1);

        var tag1 = new LegenysegiTag("Tag 1", "magyar", DateTime.Now, "férfi");
        var tag2 = new LegenysegiTag("Tag 2", "magyar", DateTime.Now, "férfi");

        parancsnoksag.HajoRegisztracio(hajo);
        parancsnoksag.LegenysegiTagRegisztracio(tag1);
        parancsnoksag.LegenysegiTagRegisztracio(tag2);

        parancsnoksag.Hozzarendel(hajo, tag1);

        Assert.That(() => parancsnoksag.Hozzarendel(hajo, tag2), Throws.TypeOf<InvalidOperationException>(), "Megtelt hajónál nem dobott kivételt!");
    }

    [Test]
    public void Urparancsnoksag_FoglaltTagUjbuliHozzarendelese_KiveteltDob()
    {
        var parancsnoksag = new Urparancsnoksag();
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");
        var hajo1 = new Urhajo("Hajó 1", tipus, osztaly, 2026, 2);
        var hajo2 = new Urhajo("Hajó 2", tipus, osztaly, 2026, 2);
        var tag = new LegenysegiTag("Tag 1", "magyar", DateTime.Now, "férfi");

        parancsnoksag.HajoRegisztracio(hajo1);
        parancsnoksag.HajoRegisztracio(hajo2);
        parancsnoksag.LegenysegiTagRegisztracio(tag);

        parancsnoksag.Hozzarendel(hajo1, tag);

        Assert.That(() => parancsnoksag.Hozzarendel(hajo2, tag), Throws.TypeOf<InvalidOperationException>(), "Már hozzárendelt tagnál nem dobott kivételt!");
    }

    [Test]
    public void Urparancsnoksag_LegenysegVisszahivasa_VisszakerulASzabadokKoze()
    {
        var parancsnoksag = new Urparancsnoksag();
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");
        var hajo = new Urhajo("Hajó 1", tipus, osztaly, 2026, 2);
        var tag = new LegenysegiTag("Tag 1", "magyar", DateTime.Now, "férfi");

        parancsnoksag.HajoRegisztracio(hajo);
        parancsnoksag.LegenysegiTagRegisztracio(tag);
        parancsnoksag.Hozzarendel(hajo, tag);

        parancsnoksag.Visszavon(hajo, tag);

        Assert.That(hajo.Legenyseg, Does.Not.Contain(tag), "A tag nem került ki a hajó legénységéből!");
        Assert.That(parancsnoksag.SzabadLegenyseg, Contains.Item(tag), "A tag nem került vissza a szabadok közé!");
        Assert.That(tag.HozzaRendeltHajo, Is.Null, "A hozzárendelt hajó értéke nem null!");
    }

    [Test]
    public void Urparancsnoksag_LegenysegAthelyezese_Sikeres()
    {
        var parancsnoksag = new Urparancsnoksag();
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");
        var hajo1 = new Urhajo("Hajó 1", tipus, osztaly, 2026, 2);
        var hajo2 = new Urhajo("Hajó 2", tipus, osztaly, 2026, 2);
        var tag = new LegenysegiTag("Tag 1", "magyar", DateTime.Now, "férfi");

        parancsnoksag.HajoRegisztracio(hajo1);
        parancsnoksag.HajoRegisztracio(hajo2);
        parancsnoksag.LegenysegiTagRegisztracio(tag);

        parancsnoksag.Hozzarendel(hajo1, tag);
        parancsnoksag.Athelyez(hajo1, hajo2, tag);

        Assert.That(hajo1.Legenyseg, Does.Not.Contain(tag), "A tag nem került le az első hajóról!");
        Assert.That(hajo2.Legenyseg, Contains.Item(tag), "A tag nem került át a második hajóra!");
        Assert.That(tag.HozzaRendeltHajo, Is.EqualTo(hajo2), "A hozzárendelt hajó nem a második hajó!");
    }
}
