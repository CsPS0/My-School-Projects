namespace kolcsonzoLib;

public interface IBerles
{
    string SporteszkozID { get; }
    DateOnly Berleskezdet { get; }
    int NapokSzama { get; }
}