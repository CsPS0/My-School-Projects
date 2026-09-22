namespace kolcsonzoLib;

public class HibasFoglalasException(string message = "A kért időszakban a sporteszköz nem szabad!")
    : Exception(message);