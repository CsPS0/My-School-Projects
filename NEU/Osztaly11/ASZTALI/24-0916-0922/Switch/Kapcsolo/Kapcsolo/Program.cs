#region Switch Feladatok

//1. Feladat:

Console.Write("Adj meg egy érdemjegyet (1-5): ");
int jegy = int.Parse(Console.ReadLine()!);

switch(jegy)
{
    case 1: Console.WriteLine("Gratulálok megfogsz bukni!"); break;
    case 2: Console.WriteLine("Hát browski vigyázzá!"); break;
    case 3: Console.WriteLine("Skibidi 3-mas..."); break;
    case 4: Console.WriteLine("Majdnem tökéletes 🤬"); break;
    case 5: Console.WriteLine("Jó, de kilették tagadva az oláh cigányokból!"); break;
    default: Console.WriteLine("Szerintem ez az eredmény nem létezik...-,-.,-.,-.,-.,-.");break;
}

//2. Feladat:

Console.Write("Adj meg egy hónapot: ");
string honap = Console.ReadLine()!;

switch(honap)
{
    case "December" or "Január" or "Február": Console.WriteLine("Jelenleg 1/4 van.");break;
    case "Március" or "Április" or "Május": Console.WriteLine("Jelenleg 2/4 van.");break;
    case "Június" or "Július" or "Augusztus": Console.WriteLine("Jelenleg 3/4 van.");break;
    case "Szeptember" or "Október" or "November": Console.WriteLine("Jelenleg 4/4 van.");break;
    default: Console.WriteLine("Szerintem valamit elrontottál, vagy csak nem lett jó a nagybetű.");break;
}

//3. Feladat:

Console.Write("Add meg valamelyik hónapnak a sorzsámát(1-12): ");
int hsz = int.Parse(Console.ReadLine()!);

switch(hsz)
{
    case 12 or 1 or 2: Console.WriteLine("Tél van, de gondolom hó az nincs.");break;
    case 3 or 4 or 5: Console.WriteLine("Tavasz van, ami azt jelenti, hogy 2024. március 31.-én jön a nyuszi!");break;
    case 6 or 7 or 8: Console.WriteLine("Itt a nyár pörög a minecárt!");break;
    case 9 or 10 or 11: Console.WriteLine("\r\nItt van az ősz, itt van ujra,\r\nS szép, mint mindig, énnekem.\r\nTudja isten, hogy mi okból\r\nSzeretem? de szeretem.\r\n \r\nKiülök a dombtetőre,\r\nInnen nézek szerteszét,\r\nS hallgatom a fák lehulló\r\nLevelének lágy neszét.");break;
    default: Console.WriteLine("Nincs ilyen számsorral rendelkező évszak! (vagy csak még nem találták ki.)");break;
}

//4. Feladat:

Console.Write("Hány órát alszol egy nap?: ");
int orak = int.Parse(Console.ReadLine()!);

switch(orak)
{
    case 1 or 2 or 3 or 4 or 5 or 6: Console.WriteLine("Egy kicsit kevés nem gondolod? Pörög a LoL ha nem tévedek...");break;
    case 7 or 8 or 9: Console.WriteLine("Átlagos.");break;
    case 10 or 11 or 12: Console.WriteLine("Kicsit sok az, nem-e?");break;
    case 13 or 14 or 15 or 16 or 17 or 18 or 19 or 20 or 21 or 22 or 23 or 24: Console.WriteLine("Hát az inkább már túl sok...");break;
    default: Console.WriteLine("Ezt nem tudom értelmezni.");break;
}

//5. Feladat:

Console.Write("Add meg a szeptemberi nap sorszámát (1-30): ");
int nap = int.Parse(Console.ReadLine()!);
switch(nap % 7)
{
    case 0: Console.WriteLine("Ez a nap Péntekre esik."); break;
    case 1: Console.WriteLine("Ez a nap Szombatra esik."); break;
    case 2: Console.WriteLine("Ez a nap Vasárnapra esik."); break;
    case 3: Console.WriteLine("Ez a nap Hétfőre esik."); break;
    case 4: Console.WriteLine("Ez a nap Keddre esik."); break;
    case 5: Console.WriteLine("Ez a nap Szerdára esik."); break;
    case 6: Console.WriteLine("Ez a nap Csütörtökre esik."); break;
}

Console.WriteLine("Az óráid ezen a napon: Informatika, Matematika, Nyelvtan.");

//6. Feladat:

Console.Write("Add meg az osztály jelet: ");
string osztaly = Console.ReadLine()!.ToLower();

switch(osztaly)
{
    case "a": Console.WriteLine("Az osztályfőnök: Kovács Péter, Terem: 101"); break;
    case "b": Console.WriteLine("Az osztályfőnök: Nagy Anna, Terem: 102"); break;
    case "c": Console.WriteLine("Az osztályfőnök: Tóth Sándor, Terem: 103"); break;
    default: Console.WriteLine("Hibás osztályjel, nincs ilyen osztály!"); break;
}

//7. Feladat:

Console.Write("Adj meg egy egész számot: ");
int szam1 = int.Parse(Console.ReadLine()!);
Console.Write("Adj meg egy másik egész számot: ");
int szam2 = int.Parse(Console.ReadLine()!);
Console.Write("Adj meg egy műveleti jelet(+, -, x, :) ");
string muvelet = Console.ReadLine()!;

switch(muvelet)
{
    case "+": Console.WriteLine(szam1 + szam2);break;
    case "-": Console.WriteLine(szam1 - szam2);break;
    case "*": Console.WriteLine(szam1 * szam2);break;
    case "/": Console.WriteLine(szam1 / szam2);break;
    default: Console.WriteLine("Ezt nem tudom értelmezni.");break;
}


//8. Feladat:

Console.Write("Adj meg egy összeget: ");
int osszeg = int.Parse(Console.ReadLine()!);
int kerekitett = osszeg % 10;

switch(kerekitett)
{
    case 1:
    case 2: osszeg -= kerekitett; break;
    case 3:
    case 4: osszeg += (5 - kerekitett); break;
    case 6:
    case 7: osszeg -= (kerekitett - 5); break;
    case 8:
    case 9: osszeg += (10 - kerekitett); break;
}

Console.WriteLine("Készpénzben ennyit kell fizetned: " + osszeg);

//9. Feladat:

Console.Write("Adj meg egy évszámot: ");
int ev = int.Parse(Console.ReadLine()!);
Console.Write("Adj meg egy hónap nevét: ");
//string honap = Console.ReadLine()!.ToLower(); //TODO!: fix hónap string malfunction

bool szokoev = (ev % 4 == 0 && ev % 100 != 0) || (ev % 400 == 0); //TODO!: nem megy

switch(honap)
{
    case "január":
    case "március":
    case "május":
    case "július":
    case "augusztus":
    case "október":
    case "december": Console.WriteLine("31 napos."); break;
    case "április":
    case "június":
    case "szeptember":
    case "november": Console.WriteLine("30 napos."); break;
    case "február": 
        if (szokoev) Console.WriteLine("29 napos.");
        else Console.WriteLine("28 napos.");
        break;
    default: Console.WriteLine("Hibás hónapnév!"); break;
}

#endregion

#region Többirányú elágazás gyakorló feladatok

//10. Feladat:

Console.Write("Add meg a vásárlás végösszegét: ");
int osszegVasarlas = int.Parse(Console.ReadLine()!);
int szelvenyek = osszegVasarlas / 500;

switch(osszegVasarlas)
{
    case >= 10000: Console.WriteLine("Ajándék: Zsákbamacska (pohárkrém, kandírozott gyümölcs, vagy marcipán figura)."); break;
    case >= 6000: Console.WriteLine("Ajándék: Tábla csoki."); break;
    case >= 3000: Console.WriteLine("Ajándék: Zacskó gumicukor."); break;
    default: Console.WriteLine("Nincs ajándék."); break;
}

Console.WriteLine($"Nyereményszelvények száma: {szelvenyek}");

//11. Feladat:

Console.Write("Add meg az időt (óra): ");
int ora = int.Parse(Console.ReadLine()!);
Console.Write("Add meg az időt (perc): ");
int perc = int.Parse(Console.ReadLine()!);

string fenyVisszajelzes = "Hibás válasz.";

if (ora >= 6 && ora < 21)
{
    Console.Write("Elég fény szűrődik be az ablakokon? (igen/nem): ");
    string elegFeny = Console.ReadLine()!.ToLower();

    switch (elegFeny)
    {
        case "igen":
            fenyVisszajelzes = "Világítás: Nincs szükség világításra.";
            break;
        case "nem":
            fenyVisszajelzes = "Világítás: Nappali fény.";
            break;
        default:
            fenyVisszajelzes = "Hibás válasz.";
            break;
    }
}
else
{
    fenyVisszajelzes = "Világítás: Éjszakai fény.";
}

Console.WriteLine(fenyVisszajelzes);

#endregion

#region Asztali alkalmazások fejlesztése

//12. Feladat:

Random random = new Random();

int elsoSzamjegy = random.Next(1, 10);
int masodikSzamjegy = random.Next(0, 10);
int harmadikSzamjegy = random.Next(0, 10);
int negyedikSzamjegy = random.Next(0, 10);

Console.WriteLine($"Generált kód: {elsoSzamjegy}{masodikSzamjegy}{harmadikSzamjegy}{negyedikSzamjegy}");

switch (elsoSzamjegy)
{
    case > 4:
        Console.WriteLine("Beléphetsz az 1. helyiségbe.");
        break;
}

switch (masodikSzamjegy)
{
    case > 4:
        Console.WriteLine("Beléphetsz a 2. helyiségbe.");
        break;
}

switch (harmadikSzamjegy)
{
    case > 4:
        Console.WriteLine("Beléphetsz a 3. helyiségbe.");
        break;
}

switch (negyedikSzamjegy)
{
    case > 4:
        Console.WriteLine("Beléphetsz a 4. helyiségbe.");
        break;
}

bool elsoEsHarmadikParos = (elsoSzamjegy % 2 == 0) && (harmadikSzamjegy % 2 == 0);
switch (elsoEsHarmadikParos)
{
    case true:
        Console.WriteLine("Beléphetsz az 5. helyiségbe.");
        break;
}

bool masodikEsNegyedikParos = (masodikSzamjegy % 2 == 0) && (negyedikSzamjegy % 2 == 0);
switch (masodikEsNegyedikParos)
{
    case true:
        Console.WriteLine("Beléphetsz a 6. helyiségbe.");
        break;
}

bool mindenParosEsNagyobbMintNegy = (elsoSzamjegy % 2 == 0 && elsoSzamjegy > 4) &&
                                        (masodikSzamjegy % 2 == 0 && masodikSzamjegy > 4) &&
                                        (harmadikSzamjegy % 2 == 0 && harmadikSzamjegy > 4) &&
                                        (negyedikSzamjegy % 2 == 0 && negyedikSzamjegy > 4);
bool vanKettoKulonbozoSzamjegy = (elsoSzamjegy != masodikSzamjegy) || 
                                    (elsoSzamjegy != harmadikSzamjegy) || 
                                    (elsoSzamjegy != negyedikSzamjegy) || 
                                    (masodikSzamjegy != harmadikSzamjegy) || 
                                    (masodikSzamjegy != negyedikSzamjegy) || 
                                    (harmadikSzamjegy != negyedikSzamjegy);

switch (mindenParosEsNagyobbMintNegy && vanKettoKulonbozoSzamjegy)
{
    case true:
        Console.WriteLine("Beléphetsz a 7. helyiségbe.");
        break;
}

if (!(elsoSzamjegy > 4 || masodikSzamjegy > 4 || harmadikSzamjegy > 4 || negyedikSzamjegy > 4 ||
        elsoEsHarmadikParos || masodikEsNegyedikParos || (mindenParosEsNagyobbMintNegy && vanKettoKulonbozoSzamjegy)))
{
    Console.WriteLine("A kártya érvénytelen, nem léphetsz be egy helyiségbe sem.");
}

#endregion