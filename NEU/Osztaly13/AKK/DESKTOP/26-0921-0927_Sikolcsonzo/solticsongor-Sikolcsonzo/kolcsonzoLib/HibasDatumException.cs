namespace kolcsonzoLib;

public class HibasDatumException(string message = "A megadott napokon a síkölcsönző nincs nyitva!")
    : Exception(message);