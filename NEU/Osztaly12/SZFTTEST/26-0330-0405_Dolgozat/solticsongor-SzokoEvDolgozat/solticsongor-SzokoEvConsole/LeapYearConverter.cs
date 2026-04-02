namespace solticsongor_SzokoEvConsole;

public class LeapYearConverter
{
    public bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
