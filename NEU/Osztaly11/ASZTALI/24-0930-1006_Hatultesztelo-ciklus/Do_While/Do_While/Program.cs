Random rnd = new Random();

#region 1.Feladat
Console.WriteLine("1. Feladat:");
int dobás1 = 0, próbálkozás1 = 0;

do
{
    dobás1 = rnd.Next(1, 7);
    próbálkozás1++;
    Console.WriteLine($"{próbálkozás1}. dobás: {dobás1}");
} while (dobás1 != 6);

Console.WriteLine($"{próbálkozás1}. dobásra sikerült 6-ost dobni.");
#endregion

#region 2.Feladat
Console.WriteLine("2. Feladat:");
int kocka2_1 = 0, kocka2_2 = 0, próbálkozás2 = 0;

do
{
    kocka2_1 = rnd.Next(1, 7);
    kocka2_2 = rnd.Next(1, 7);
    próbálkozás2++;
    Console.WriteLine($"{próbálkozás2}. dobás: {kocka2_1}; {kocka2_2}");
} while (kocka2_1 != kocka2_2);

Console.WriteLine($"{próbálkozás2}. dobásra sikerült ugyanolyan értékeket dobni.");
#endregion

#region 3.Feladat
Console.WriteLine("3. Feladat:");
int kocka3_1 = 0, kocka3_2 = 0, kocka3_3 = 0, próbálkozás3 = 0;

do
{
    kocka3_1 = rnd.Next(1, 7);
    kocka3_2 = rnd.Next(1, 7);
    kocka3_3 = rnd.Next(1, 7);
    próbálkozás3++;
    Console.WriteLine($"{próbálkozás3}. dobás: {kocka3_1}; {kocka3_2}; {kocka3_3}");
} while (kocka3_1 != 6 && kocka3_2 != 6 && kocka3_3 != 6);

Console.WriteLine($"{próbálkozás3}. dobásra sikerült legalább egy 6-ost dobni.");
#endregion

#region 4.Feladat
Console.WriteLine("4. Feladat:");
int kocka4_1 = 0, kocka4_2 = 0, kocka4_3 = 0, próbálkozás4 = 0;

do
{
    kocka4_1 = rnd.Next(1, 7);
    kocka4_2 = rnd.Next(1, 7);
    kocka4_3 = rnd.Next(1, 7);
    próbálkozás4++;
    Console.WriteLine($"{próbálkozás4}. dobás: {kocka4_1}; {kocka4_2}; {kocka4_3}");
} while (kocka4_1 < 5 || kocka4_2 < 5 || kocka4_3 < 5);

Console.WriteLine($"{próbálkozás4}. dobásra lett mindegyik érték legalább 5.");
#endregion

#region 5.Feladat
Console.WriteLine("5. Feladat:");
int kocka5_1 = 0, kocka5_2 = 0, kocka5_3 = 0, próbálkozás5 = 0;

do
{
    kocka5_1 = rnd.Next(1, 7);
    kocka5_2 = rnd.Next(1, 7);
    kocka5_3 = rnd.Next(1, 7);
    próbálkozás5++;
    Console.WriteLine($"{próbálkozás5}. dobás: {kocka5_1}; {kocka5_2}; {kocka5_3}");
} while (kocka5_1 % 3 != 0 || kocka5_2 % 3 != 0 || kocka5_3 % 3 != 0);

Console.WriteLine($"{próbálkozás5}. dobásra lett mindegyik érték 3-mal osztható.");
#endregion

#region 6.Feladat
Console.WriteLine("6. Feladat:");
int kocka6_1 = 0, kocka6_2 = 0, kocka6_3 = 0, próbálkozás6 = 0;

do
{
    kocka6_1 = rnd.Next(1, 7);
    kocka6_2 = rnd.Next(1, 7);
    kocka6_3 = rnd.Next(1, 7);
    próbálkozás6++;
    Console.WriteLine($"{próbálkozás6}. dobás: {kocka6_1}; {kocka6_2}; {kocka6_3}");
} while (!(kocka6_1 == kocka6_2 || kocka6_1 == kocka6_3 || kocka6_2 == kocka6_3));

Console.WriteLine($"{próbálkozás6}. dobásra lett legalább két kockán ugyanaz az érték.");
#endregion

#region 7.Feladat
Console.WriteLine("7. Feladat:");
int kocka7_1 = 0, kocka7_2 = 0, kocka7_3 = 0, próbálkozás7 = 0;

do
{
    kocka7_1 = rnd.Next(1, 7);
    kocka7_2 = rnd.Next(1, 7);
    kocka7_3 = rnd.Next(1, 7);
    próbálkozás7++;
    Console.WriteLine($"{próbálkozás7}. dobás: {kocka7_1}; {kocka7_2}; {kocka7_3}");
} while (kocka7_1 == kocka7_2 || kocka7_1 == kocka7_3 || kocka7_2 == kocka7_3);

Console.WriteLine($"{próbálkozás7}. dobásra lett mindhárom kockán különböző az érték.");
#endregion