#region files
string[] file = File.ReadAllLines("torpek.txt");
List<Smurf> Smurfs = new List<Smurf>();

for (int i = 1; i < file.Length; i++)
{
    string[] attr = file[i].Split(';');
    string name = attr[0];
    string clan = attr[1];
    string sex = attr[2];
    int weight = int.Parse(attr[3]);
    int height = int.Parse(attr[4]);
    Smurfs.Add(new Smurf(name, clan, sex, weight, height));
}
#endregion

#region 2.fel
int list_ct = 0;
for (int i = 0; i < Smurfs.Count; i++)
{
    list_ct++;
}
Console.WriteLine($"2. feladat: Az állományban található törpék száma: {list_ct} db");
#endregion

#region 3.fel
double smurf_avg = 0;
for (int i = 0;i < Smurfs.Count; i++)
{
    smurf_avg += Smurfs[i].Weight;
}
smurf_avg /= (Smurfs.Count);
Console.WriteLine($"3. feladat: A törpék átlagos súlya: {Math.Round(smurf_avg, 1)} kg");
#endregion

#region 4.fel
int hgst_smf = int.MinValue;
string hgst_smf_name = "";
string hgst_smf_clan = "";
string hgst_smf_sex = "";
int hgst_smf_weight = 0;

for (int i = 0;i < Smurfs.Count ; i++)
{
    if (Smurfs[i].Height > hgst_smf)
    {
        hgst_smf = Smurfs[i].Height;
        hgst_smf_name = Smurfs[i].Name;
        hgst_smf_clan = Smurfs[i].Clan;
        hgst_smf_sex = Smurfs[i].Sex;
        hgst_smf_weight = Smurfs[i].Weight;
    }
}
Console.WriteLine($"A legmagasabb törpe adatai: \n\t Név: {hgst_smf_name} \n\t Klán: {hgst_smf_clan} \n\t Nem: {hgst_smf_sex} \n\t Súly: {hgst_smf_weight} kg \n\t Magasság: {hgst_smf} cm");
#endregion

#region 5.fel
Console.Write("Adj meg egy klán nevet: ");
string clan_inp = Convert.ToString(Console.ReadLine()!);

bool byClan = false;
for (int i = 0; i < Smurfs.Count ; i++)
{
    if (byClan = true)
    {
        break;
    }
}
#endregion

#region 6.fel

#endregion

#region 7.fel

#endregion

#region 8.fel

#endregion

#region 9.fel

#endregion

#region 10.fel

#endregion

public record Smurf(string Name, string Clan, string Sex, int Weight, int Height);