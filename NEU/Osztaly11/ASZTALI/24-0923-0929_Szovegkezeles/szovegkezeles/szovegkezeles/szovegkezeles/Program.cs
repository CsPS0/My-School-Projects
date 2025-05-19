#region 1. Feladat
Console.WriteLine("1. Feladat:\n");
Console.Write("Adj meg egy szöveget: ");
string szoveg = Console.ReadLine()!;

Console.Write("Add meg melyik feladatott szeretnéd végrehajtanni(a/b/c/d/e/f): ");
string feladat1 = Console.ReadLine()!;

switch (feladat1)
{
    case "a":
        Console.WriteLine(szoveg.Length);
        break;
    case "b":
        for (int i = 0; i < szoveg.Length; i++)
        {
            Console.WriteLine(szoveg[i]);
        }
        break;
    case "c":
        for (int i = szoveg.Length - 1; i >= 0; i--)
        {
            Console.WriteLine(szoveg[i]);
        }
        break;
    case "d":
        Console.WriteLine(szoveg.ToUpper());
        break;
    case "e":
        Console.WriteLine(szoveg.ToLower());
        break;
    case "f":
        char[] betuk = szoveg.ToCharArray();
        char temp = betuk[1];
        betuk[1] = betuk[2];
        betuk[2] = temp;
        Console.WriteLine(new string(betuk));
        break;
}
#endregion

#region 2. Feladat
Console.WriteLine("\n2. Feladat\n");
Console.Write("Adj meg egy szót: ");
string szo1 = Console.ReadLine()!;
Console.Write("Adj meg egy másik szót:");
string szo2 = Console.ReadLine()!;

//a
Console.WriteLine($"\nA két szó összehúzva: {szo1 + szo2}");

//b
if (szo1.Length > szo2.Length)
{
    Console.WriteLine("\nAz első szó hosszabb, mint a második");
}
else if (szo1.Length < szo2.Length)
{
    Console.WriteLine("\nA második szó hosszabb, mint az első");
}
else
{
    Console.WriteLine("\nA két szó egyforma hosszú");
}

//c
Console.WriteLine(szo1 + ";" + szo2);

//d
string forditottSzo2 = "";
for (int i = szo2.Length - 1; i >= 0; i--)
{
    forditottSzo2 += szo2[i];
}
Console.WriteLine(szo1 + forditottSzo2);

//e
string valtakozoSzo = "";
int maxHossz = szo1.Length > szo2.Length ? szo1.Length : szo2.Length;

for (int i = 0; i < maxHossz; i++)
{
    if (i % 2 == 0)
    {
        valtakozoSzo += (i < szo1.Length) ? szo1[i] : '*';
    }
    else
    {
        valtakozoSzo += (i < szo2.Length) ? szo2[i] : '*';
    }
}
Console.WriteLine(valtakozoSzo);
#endregion

#region 3. Feladat
Console.WriteLine("\n3.Feladat\n");
Console.Write("Írj be egy szöveget: ");
string szoveg3 = Console.ReadLine()!;

//a
string forditottSzoveg = "";
for (int i = szoveg3.Length - 1; i >= 0; i--)
{
    forditottSzoveg += szoveg3[i];
}
Console.WriteLine(forditottSzoveg);

//b
string[] szavak = szoveg3.Split(' ');
string ujSzoveg = "";
for (int i = 0; i < szavak.Length; i++)
{
    if (szavak[i] != "")
    {
        ujSzoveg += szavak[i] + " ";
    }
}
Console.WriteLine(ujSzoveg.Trim());

//c
Console.WriteLine(szoveg3.Trim());

//d
string palindromSzoveg = szoveg3.Replace(" ", "").ToLower();
string forditottPalindrom = "";
for (int i = palindromSzoveg.Length - 1; i >= 0; i--)
{
    forditottPalindrom += palindromSzoveg[i];
}
Console.WriteLine(palindromSzoveg == forditottPalindrom ? "A szöveg palindrom" : "A szöveg nem palindrom");
#endregion

#region 4. Feladat
Console.WriteLine("\n4. Feladat\n");
Console.Write("Add meg a családneved: ");
string csaladnev = Console.ReadLine()!;
Console.Write("Add meg a kereszneved: ");
string keresztnev = Console.ReadLine()!;
Console.Write("Van esetleg 2. keresztneved (igen/nem)?: ");
string knevkerd = Console.ReadLine()!;

if (knevkerd == "igen")
{
    Console.Write("Add meg a második keresznevedet: ");
    string msdkkeresztnev = Console.ReadLine()!;

    //a
    string azonosito = csaladnev.ToLower() + "." + keresztnev.ToLower() + "." + msdkkeresztnev.ToLower();
    Console.WriteLine($"Azonosító: {azonosito}");

    //b
    Random rnd = new Random();
    string jelszo = csaladnev.Substring(0, 2).ToLower() +
                    rnd.Next(100, 999) +
                    keresztnev[1] + keresztnev[3] +
                    "!?#"[rnd.Next(3)] +
                    msdkkeresztnev.Substring(msdkkeresztnev.Length - 2).ToUpper();
    Console.WriteLine($"Jelszó: {jelszo}");
}
else if (knevkerd == "nem")
{
    string azonosito = csaladnev.ToLower() + "." + keresztnev.ToLower();
    Console.WriteLine($"Azonosító: {azonosito}");

    Random rnd = new Random();
    string jelszo = csaladnev.Substring(0, 2).ToLower() +
                    rnd.Next(100, 999) +
                    keresztnev[1] + keresztnev[3] +
                    "!?#"[rnd.Next(3)] +
                    csaladnev.Substring(csaladnev.Length - 2).ToUpper();
    Console.WriteLine($"Jelszó: {jelszo}");
}
else
{
    Console.WriteLine("Sajnos ezt nem értem...");
}
#endregion

#region 5. Feladat
Console.WriteLine("\n5. Feladat\n");
Console.Write("Írj be egy szöveget: ");
string madarnyelv = Console.ReadLine()!;

string madarnyelvesito = "";
string maganhangzok = "aáeéiíoóöőuúüűAÁEÉIÍOÓÖŐUÚÜŰ";

for (int i = 0; i < madarnyelv.Length; i++)
{
    madarnyelvesito += madarnyelv[i];
    if (maganhangzok.Contains(madarnyelv[i]))
    {
        madarnyelvesito += "v" + madarnyelv[i].ToString().ToLower();
    }
}
Console.WriteLine(madarnyelvesito);
#endregion

#region 6. Feladat
Console.WriteLine("\n6. Feladat\n");
Console.Write("Adj meg egy email címet: ");
string email = Console.ReadLine()!;

if (!email.Contains('@'))
{
    Console.WriteLine("Az email nem tartalmaz @ karaktert!");
}
else if (!email.Contains('.'))
{
    Console.WriteLine("Az email nem tartalmaz pontot!");
}
else if (email[0] == '@')
{
    Console.WriteLine("Az email nem kezdődhet @-al!");
}
else if (email.LastIndexOf('.') < email.IndexOf('@'))
{
    Console.WriteLine("A pontnak a @ után kell lennie!");
}
else if (email.Contains(" "))
{
    Console.WriteLine("Az emailben nem lehet szóköz!");
}
else
{
    Console.WriteLine("Az email formátuma megfelelő!");
}
#endregion

#region 7. Feladat
Console.WriteLine("\n7. Feladat\n");
Console.Write("Adj meg egy jelszót: ");
string jelszo1 = Console.ReadLine()!;
Console.Write("Add meg újra a jelszót: ");
string jelszo2 = Console.ReadLine()!;

if (jelszo1 != jelszo2)
{
    Console.WriteLine("A két jelszó nem egyezik!");
}
else if (jelszo1.Length < 8)
{
    Console.WriteLine("A jelszó túl rövid!");
}
else
{
    bool vanNagybetu = jelszo1.Any(c => c >= 'A' && c <= 'Z');
    bool vanKisbetu = jelszo1.Any(c => c >= 'a' && c <= 'z');
    bool vanSzam = jelszo1.Any(c => c >= '0' && c <= '9');
    bool vanSpecialisKarakter = jelszo1.Any(c => "!?.@#$".Contains(c));

    if (!vanNagybetu)
    {
        Console.WriteLine("Nincs benne nagybetű!");
    }
    else if (!vanKisbetu)
    {
        Console.WriteLine("Nincs benne kisbetű!");
    }
    else if (!vanSzam)
    {
        Console.WriteLine("Nincs benne szám!");
    }
    else if (!vanSpecialisKarakter)
    {
        Console.WriteLine("Nincs benne speciális karakter!");
    }
    else
    {
        Console.WriteLine("A jelszó megfelelő!");
    }
}
#endregion

#region 8. Feladat
Console.WriteLine("\n8. Feladat\n");
Console.Write("Adj meg egy max 15 karakteres szót: ");
string titkositas = Console.ReadLine()!;

if (titkositas.Length > 15)
{
    Console.WriteLine("A szó túl hosszú!");
}
else
{
    string paratlan = "";
    string paros = "";

    for (int i = 0; i < titkositas.Length; i++)
    {
        if (i % 2 == 0)
            paros += titkositas[i];
        else
            paratlan += titkositas[i];
    }

    string forditottParos = "";
    for (int i = paros.Length - 1; i >= 0; i--)
    {
        forditottParos += paros[i];
    }

    Console.WriteLine($"Titkosított szöveg: {paratlan + forditottParos}");
}
#endregion