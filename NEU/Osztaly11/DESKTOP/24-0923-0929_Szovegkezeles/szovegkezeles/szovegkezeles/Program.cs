#region 1. Feladat
using System;

Console.WriteLine("1. Feladat:\n");
Console.Write("Adj meg egy szöveget: ");
string szoveg = Console.ReadLine()!;

Console.Write("Add meg melyik feladatott szeretnéd végrehajtanni(a/b/c/d/e/f): ");
string feladat1 = Console.ReadLine()!;

switch(feladat1)
{
    case "a": Console.WriteLine(szoveg.Length);break;
    case "b": Console.WriteLine();break;
    case "c": Console.WriteLine();break;
    case "d": Console.WriteLine(szoveg.ToUpperInvariant);break;
    case "e": Console.WriteLine(szoveg.ToLowerInvariant);break;
    case "f": Console.WriteLine();break;
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

else
{
    Console.WriteLine("\nA második szó hosszabb, mint az első\n");
}

//c
Console.WriteLine(szo1 + ";" + szo2);

//d
string vszsz2 = string.Join(" ", szo2.Split(' ').Select(x => new String(x.Reverse().ToArray())));
Console.WriteLine(szo1 + vszsz2);

//e

#endregion

#region 3. Feladat
Console.WriteLine("\n3.Feladat\n");
Console.Write("Írj be egy szöveget: ");
string szoveg3 = Console.ReadLine()!;

//a
string vszsz3 = string.Join(" ", szoveg3.Split(' ').Select(x => new String(x.Reverse().ToArray())));
Console.WriteLine(vszsz3);

//b


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
    string azonosito = csaladnev + keresztnev + msdkkeresztnev;

    
    //b

}

else if (knevkerd == "nem")
{
    Console.WriteLine("Rendben, akkor nem fogom figyelembe venni.");
}

else 
{
    Console.WriteLine("Sajnos ezt nem értem...");
}
#endregion

#region 5. Feladat

// A feladatot a következőképen lehet megoldanni, amely a "madárnyelv"-en alapul, szóval megad a felhasználó egy szöveget, ahol a beírt szöveg: 'szöveg', de nekünk át kell alakítanni: 'szövöveveg'-re, amiben a logika a magánhangók utáni mássalhangók duplázzása...


Console.WriteLine("\n5. Feladat\n");
Console.Write("Írj be egy szöveget: ");
string madarnyelv = Console.ReadLine()!;


#endregion

#region 6. Feladat
Console.WriteLine("\n6. Feladat\n");
Console.Write("Adj meg egy email címet: ");
string email = Console.ReadLine()!;

//a
if (email.Contains = '@')
{
    if (email.Contains = '.')
    {
        if (email[0 != '@'])
        {
            if (email.Contains = '.___@')
            {
                if (email.Contains != ' ')
                {
                    Console.WriteLine("Tökéletes az emailed!");
                }
                else
                {
                    Console.WriteLine("Az emailben nem lehet szóközt rakni!");
                }
            }
            else
            {
                Console.WriteLine("Az email előtt legalább egy pont legyen!");
            }
        }
        else
        {
            Console.WriteLine("Az email nem kezdődhet @-al!");
        }
    }
    else
    {
        Console.WriteLine("Tartalmazzon legalább egy pontot!");
    }
}
else 
{
    Console.WriteLine("Az email tartalamzon @-ot!");
}
#endregion

#region 7. Feladat
Console.WriteLine("\n7. Feladat\n");
Console.Write("Adj meg egy jelszót, ami:\n • több mint 8 karakter,\n • tartalmaz legalább egy nagy betűt,\n • van benne kisbetű,\n • tartalmaz speciális karaktert,\n • tartalmaz számot. Bekérés: ");
string jelszo1 = Console.ReadLine()!;
Console.Write("Add meg újra a jelszót: ");
string jelszo2 = Console.ReadLine()!;

List<char> nagyBetuk = new List<char>
{
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z','Á', 'É', 'Í', 'Ó', 'Ö', 'Ő', 'Ú', 'Ü', 'Ű'
};

List<char> kisBetuk = new List<char>
{
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'á', 'é', 'í', 'ó', 'ö', 'ő', 'ú', 'ü', 'ű'
};

List<char> specialisKarakterek = new List<char>
{
    '!', '?', '.', '@', '#', '$'
};

List<char> szamokLista = new List<char>
{
    '1', '2', '3', '4', '5', '6', '7', '8', '9'
};

if (jelszo1 == jelszo2)
{
    if (jelszo1.Length < 8)
    {
        if (jelszo1.Contains == nagyBetuk)
        {
            if (jelszo1.Contains == kisBetuk)
            {
                if (jelszo1.Contains == specialisKarakterek)
                {
                    if (jelszo1.Contains == szamokLista)
                    {
                        Console.WriteLine("Tökéletes jelszó!");
                    }
                    else
                    {
                        Console.WriteLine("A jelszó nem tartalmaz számot!");
                    }
                }
                else
                {
                    Console.WriteLine("A jelszó nem tartalmaz speciális karaktert!");
                }
            }
            else
            {
                Console.WriteLine("A jelszó nem tartalmaz kisbetűt!");
            }
        }
        else
        {
            Console.WriteLine("A jelszó nem tartalmaz nagybetűt!");
        }
    }
    else
    {
        Console.WriteLine("A jelszó sajnos 8 karaktelnél rövidebb!");
    }
}
else
{
    Console.WriteLine("A két jelszó nem egyezik!");
}
#endregion

#region 8. Feladat
Console.WriteLine("\n8. Feladat\n");
Console.Write("Adj meg egy max 15 karakteres szót: ");
string titkositas = Console.ReadLine()!;

if (titkositas[titkositas.Length > 15])

#endregion