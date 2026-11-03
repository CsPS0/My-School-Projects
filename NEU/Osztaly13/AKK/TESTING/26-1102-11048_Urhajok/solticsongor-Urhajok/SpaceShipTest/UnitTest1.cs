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
        Assert.That(() => new UrhajoOsztaly(""), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void UrhajoTipus_ErvenyesAdatok_SikeresPeldanyositas()
    {
        var elvart = new Dictionary<string, int> { { "navigáció", 2 } };
        var tipus = new UrhajoTipus("Felderítő", "Mélyűri felderítő űrhajó", elvart);

        Assert.That(tipus.Nev, Is.EqualTo("Felderítő"));
        Assert.That(tipus.Leiras, Is.EqualTo("Mélyűri felderítő űrhajó"));
        Assert.That(tipus.ElvartSzakertelmek["navigáció"], Is.EqualTo(2));
    }

    [Test]
    public void UrhajoTipus_UresElvartSzakertem_NemDobKivetelt()
    {
        var tipus = new UrhajoTipus("Teherszállító", "Nehéz teherszállító");

        Assert.That(tipus.ElvartSzakertelmek, Is.Empty);
    }

    [Test]
    public void UrhajoTipus_ErvenytelenNev_KiveteltDob()
    {
        Assert.That(() => new UrhajoTipus("", "Leírás"), Throws.TypeOf<ArgumentException>());
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

        Assert.That(tag.Nev, Is.EqualTo("John Smith"));
        Assert.That(tag.Nemzetiseg, Is.EqualTo("amerikai"));
        Assert.That(tag.SzuletesiDatum, Is.EqualTo(new DateTime(2001, 11, 2)));
        Assert.That(tag.Nem, Is.EqualTo("férfi"));
        Assert.That(tag.GetSzakertelemSzint("navigáció"), Is.EqualTo(2));
        Assert.That(tag.GetSzakertelemSzint("diplomácia"), Is.EqualTo(1));
        Assert.That(tag.GetSzakertelemSzint("harcászat"), Is.EqualTo(0));
        Assert.That(tag.HozzaRendeltHajo, Is.Null);
    }

    [Test]
    public void LegenysegiTag_ErvenytelenNev_KiveteltDob()
    {
        Assert.That(() => new LegenysegiTag("", "magyar", DateTime.Now, "férfi"), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Urhajo_ErvenytelenFerohely_KiveteltDob()
    {
        var osztaly = new UrhajoOsztaly("Felfedező");
        var tipus = new UrhajoTipus("Felderítő", "Leírás");

        Assert.That(() => new Urhajo("Kutató 11", tipus, osztaly, 2026, 0), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Urhajo_ElvartSzakertelemNelkul_MindigIndithato()
    {
        var osztaly = new UrhajoOsztaly("Teher");
        var tipus = new UrhajoTipus("Bárka", "Egyszerű szállító");
        var hajo = new Urhajo("Cargo 1", tipus, osztaly, 2024, 5);

        Assert.That(hajo.Indithato, Is.True);
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

        Assert.That(hajo.Indithato, Is.False);
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

        Assert.That(hajo.Indithato, Is.True);
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

        Assert.That(hajo.Indithato, Is.False);
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

        Assert.That(parancsnoksag.SzabadLegenyseg, Contains.Item(tag));

        parancsnoksag.Hozzarendel(hajo, tag);

        Assert.That(hajo.Legenyseg, Contains.Item(tag));
        Assert.That(parancsnoksag.SzabadLegenyseg, Does.Not.Contain(tag));
        Assert.That(tag.HozzaRendeltHajo, Is.EqualTo(hajo));
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

        Assert.That(() => parancsnoksag.Hozzarendel(hajo, tag2), Throws.TypeOf<InvalidOperationException>());
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

        Assert.That(() => parancsnoksag.Hozzarendel(hajo2, tag), Throws.TypeOf<InvalidOperationException>());
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

        Assert.That(hajo.Legenyseg, Does.Not.Contain(tag));
        Assert.That(parancsnoksag.SzabadLegenyseg, Contains.Item(tag));
        Assert.That(tag.HozzaRendeltHajo, Is.Null);
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

        Assert.That(hajo1.Legenyseg, Does.Not.Contain(tag));
        Assert.That(hajo2.Legenyseg, Contains.Item(tag));
        Assert.That(tag.HozzaRendeltHajo, Is.EqualTo(hajo2));
    }
}
