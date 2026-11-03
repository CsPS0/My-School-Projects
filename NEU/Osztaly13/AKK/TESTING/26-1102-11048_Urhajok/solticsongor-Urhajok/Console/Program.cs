using ConsoleApp;

var osztaly = new UrhajoOsztaly("felfedező");
var tipus = new UrhajoTipus("felderítő", "mélységi felderítő űrhajó", new Dictionary<string, int> { { "navigáció", 2 } });
var hajo = new Urhajo("Kutató 11", tipus, osztaly, 2024, 2);

var szakertelmek = new Dictionary<string, int> { { "diplomácia", 1 }, { "navigáció", 2 } };
var tag1 = new LegenysegiTag("John Smith", "amerikai", new DateTime(2001, 11, 2), "férfi", szakertelmek);

var parancsnoksag = new Urparancsnoksag();
parancsnoksag.HajoRegisztracio(hajo);
parancsnoksag.LegenysegiTagRegisztracio(tag1);
parancsnoksag.Hozzarendel(hajo, tag1);

Console.WriteLine("Hajó: " + hajo.Nev);
Console.WriteLine("Indítható (1 fővel): " + hajo.Indithato);

var tag2 = new LegenysegiTag("Kovács Béla", "magyar", new DateTime(1995, 5, 10), "férfi", new Dictionary<string, int> { { "navigáció", 1 } });
parancsnoksag.LegenysegiTagRegisztracio(tag2);
parancsnoksag.Hozzarendel(hajo, tag2);

Console.WriteLine("Indítható (2 fővel): " + hajo.Indithato);
