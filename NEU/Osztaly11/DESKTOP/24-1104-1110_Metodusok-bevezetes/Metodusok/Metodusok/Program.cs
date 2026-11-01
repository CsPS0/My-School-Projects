#region
Random random = new();
#endregion

#region 1. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

void TeglalapRajzolo()
{
    Console.Write("Add meg az 'a' oldal méretét !cm: ");
    int a = int.Parse(Console.ReadLine()!);
    Console.Write("Add meg az 'b' oldal méretét !cm: ");
    int b = int.Parse(Console.ReadLine()!);

    Console.Write("Add meg a rajzolás színét: ");
    string szin = Console.ReadLine();

    switch (szin)
    {
        case "blue" or "kék": Console.ForegroundColor = ConsoleColor.Blue; break;
        case "green" or "zöld": Console.ForegroundColor = ConsoleColor.Green; break;
        case "cyan" or "türkizkék": Console.ForegroundColor = ConsoleColor.Cyan; break;
        case "red" or "piros": Console.ForegroundColor = ConsoleColor.Red; break;
        case "magenta" or "rózsaszín" or "lila": Console.ForegroundColor = ConsoleColor.Magenta; break;
        case "yellow" or "sárga": Console.ForegroundColor = ConsoleColor.Yellow; break;
        case "white" or "fehér": Console.ForegroundColor = ConsoleColor.White; break;
        case "gray" or "grey" or "szürke": Console.ForegroundColor = ConsoleColor.Gray; break;
        case "black" or "fekete": Console.ForegroundColor = ConsoleColor.Black; break;
        case "darkblue" or "sötétkék": Console.ForegroundColor = ConsoleColor.DarkBlue; break;
        case "darkgreen" or "sötétzöld": Console.ForegroundColor = ConsoleColor.DarkGreen; break;
        case "darkcyan" or "sötéttürkizkék": Console.ForegroundColor = ConsoleColor.DarkCyan; break;
        case "darkred" or "sötétpiros": Console.ForegroundColor = ConsoleColor.DarkRed; break;
        case "darkmagenta" or "sötétrózsaszín" or "sötétlila": Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
        case "darkyellow" or "sötétsárga": Console.ForegroundColor = ConsoleColor.Yellow; break;
        case "darkgray" or "darkgrey" or "sötétszürke": Console.ForegroundColor = ConsoleColor.DarkGray; break;
        default: Console.WriteLine("Nem tudom értelmezni, ezért az alapértelmezet szín lesz a rajzolási színed"); break;
    }

    for (int i = 0; i < a; i++)
    {
        for (int j = 0; j < b; j++)
        {
            Console.Write("# ");
        }
        Console.WriteLine();
    }

    Console.ResetColor();
}

TeglalapRajzolo();
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 2. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

void RombuszRajzolo(int magassag)
{
    for (int i = 0; i < magassag; i++)
    {
        Console.Write(new string(' ', magassag - i - 1));
        Console.WriteLine(new string('*', i * 2 + 1));
    }
    for (int i = magassag - 2; i >= 0; i--)
    {
        Console.Write(new string(' ', magassag - i - 1));
        Console.WriteLine(new string('*', i * 2 + 1));
    }
}
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

//#region 3. Feladat
//Console.ForegroundColor = ConsoleColor.Red;
//Console.WriteLine("3. Feladat");
//Console.ResetColor();

//void FibonacciSorozat(int n)
//{
//    int a = 0, b = 1, c;

//    Console.WriteLine("A Fibonacci sorozat:");
//    Console.Write($"{a} {b} ");

//    for (int i = 2; i < n; i++)
//    {
//        c = a + b;
//        Console.Write($"{c} ");
//        a = b;
//        b = c;
//    }
//    Console.WriteLine();
//}

//Console.Write("Add meg hány Fibonacci számot szeretnél: ");
//int szam = Convert.ToInt32(Console.ReadLine());
//Console.WriteLine($"A Fibonacciszámod {FibonacciSorozat(szam)} lett");
//#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 4. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

string Madar(string szoveg)
{
    string madarnyelv = "";
    string maganhangzok = "aáeéiíoóöőuúüű";

    for (int i = 0; i < szoveg.Length; i++)
    {
        madarnyelv += szoveg[i];
        string karakter = szoveg[i].ToString();

        if (maganhangzok.Contains(karakter.ToLower()))
        {
            madarnyelv += "v" + karakter;
        }
    }
    return madarnyelv;
}

Console.Write("Adj meg egy szöveget: ");
string szoveg = Console.ReadLine();
Console.WriteLine($"Madárnyelven: {Madar(szoveg)}");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 5. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

int LegnagyobbKozosOszto(int szam1, int szam2)
{
    int eredmeny = szam1;

    while (szam2 != 0)
    {
        int maradek = eredmeny % szam2;
        eredmeny = szam2;
        szam2 = maradek;
    }
    return eredmeny;
}

Console.Write("Add meg az 1. számot: ");
int szam1 = Convert.ToInt32(Console.ReadLine());
Console.Write("Add meg a 2. számot: ");
int szam2 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"A legnagyobb közös osztó: {LegnagyobbKozosOszto(szam1, szam2)}");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 6. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

bool PrimE(int szam)
{
    bool eredmeny = true;

    if (szam <= 1)
    {
        eredmeny = false;
    }
    else
    {
        for (int i = 2; i < szam; i++)
        {
            if (szam % i == 0)
            {
                eredmeny = false;
                break;
            }
        }
    }
    return eredmeny;
}

Console.Write("Add meg a prímszámot: ");
int primszam = Convert.ToInt32(Console.ReadLine());
if (PrimE(primszam))
{
    Console.WriteLine($"{primszam} egy prímszám!");
}

else
{
    Console.WriteLine($"{primszam} nem prímszám!");
}
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 7. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

string ForditottSzolista(List<string> szolista)
{
    string eredmeny = "";
    for (int i = szolista.Count - 1; i >= 0; i--)
    {
        string szo = szolista[i];
        if (szo.Length > 0)
        {
            string nagybetusSzo = szo.Substring(0, 1).ToUpper() + szo.Substring(1).ToLower();
            eredmeny += nagybetusSzo + " ";
        }
    }
    return eredmeny.TrimEnd();
}

Console.Write("Adj meg szavakat vesszővel elválasztva (például: alma,banán,körte): ");
string input = Console.ReadLine();
List<string> szolista = new List<string>(input.Split(','));
Console.WriteLine($"Fordított szólista: {ForditottSzolista(szolista)}");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 8. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

int MasodfokuEgyenletMegoldasainakSzama(double a = 1, double b = 0, double c = 0)
{
    double diszkriminans = b * b - 4 * a * c;
    int megoldasokSzama = 0;

    if (diszkriminans > 0)
    {
        megoldasokSzama = 2;
    }
    else if (diszkriminans == 0)
    {
        megoldasokSzama = 1;
    }
    return megoldasokSzama;
}

Console.Write("Add meg az 'a' értékét:");
double msd_a = Convert.ToDouble(Console.ReadLine());

Console.Write("Add meg a 'b' értékét:");
double msd_b = Convert.ToDouble(Console.ReadLine());

Console.Write("Add meg a 'c' értékét:");
double msd_c = Convert.ToDouble(Console.ReadLine());

Console.WriteLine($"Megoldások száma: {MasodfokuEgyenletMegoldasainakSzama(msd_a, msd_b, msd_c)}");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 9. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

int AtlagnakMegfeleloSzamok(List<int> szamok)
{
    int osszeg = 0;
    for (int i = 0; i < szamok.Count; i++)
    {
        osszeg += szamok[i];
    }
    double atlag = (double)osszeg / szamok.Count;

    int szamlalo = 0;
    for (int i = 0; i < szamok.Count; i++)
    {
        double elteres = Math.Abs(szamok[i] - atlag);
        if (elteres <= 10)
        {
            szamlalo++;
        }
    }
    return szamlalo;
}

Console.Write("Adj meg számokat vesszővel elválasztva (például: 10,20,30,40,50): ");
string szamokBemenet = Console.ReadLine();
List<int> szamok = new List<int>(Array.ConvertAll(szamokBemenet.Split(','), int.Parse));
Console.WriteLine($"Az átlaghoz közeli számok száma: {AtlagnakMegfeleloSzamok(szamok)}");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 10. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat");
Console.ResetColor();

bool EmailE(string szoveg)
{
    bool vanKukac = szoveg.Contains("@");
    bool vanPont = szoveg.Contains(".");

    bool megfelelE = (vanKukac && vanPont);
    return megfelelE;
}

Console.Write("Add meg az e-mail címet: ");
string email = Console.ReadLine();

if (EmailE(email))
{
    Console.WriteLine("Az e-mail cím formátuma helyes!");
}
else
{
    Console.WriteLine("Az e-mail cím formátuma hibás!");
}

#endregion