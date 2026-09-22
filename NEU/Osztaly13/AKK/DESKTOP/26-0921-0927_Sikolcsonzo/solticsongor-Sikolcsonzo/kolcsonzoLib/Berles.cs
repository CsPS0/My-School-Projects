namespace kolcsonzoLib;

public class Berles : IBerles
{
    private static readonly DateOnly SzezonKezdet = new(2024, 12, 2);
    private static readonly DateOnly SzezonVeg = new(2025, 4, 21);

    public string SporteszkozID { get; }
    public DateOnly Berleskezdet { get; }
    public int NapokSzama { get; }
    public string BerloNeve { get; }

    public DateOnly BerlesVege => Berleskezdet.AddDays(NapokSzama - 1);

    public Berles(string sporteszkozID, DateOnly berleskezdet, int napokSzama, string berloNeve)
    {
        DateOnly utolsoNap = berleskezdet.AddDays(napokSzama - 1);
        if (berleskezdet < SzezonKezdet || utolsoNap > SzezonVeg)
        {
            throw new HibasDatumException();
        }

        SporteszkozID = sporteszkozID;
        Berleskezdet = berleskezdet;
        NapokSzama = napokSzama;
        BerloNeve = berloNeve;
    }

    public override string ToString() =>
        $"{Berleskezdet:yyyy. MM. dd.} - {BerlesVege:yyyy. MM. dd.} {BerloNeve}";
}