namespace solticsongor_Autoverseny;

public abstract class Racer
{
    public string Name { get; set; }
    public int Fuel { get; set; } = 100;
    public bool IsOut { get; set; } = false;
    protected int OvertakeAttempts { get; set; } = 0;

    public Racer(string name)
    {
        Name = name;
    }

    public void DecreaseFuel(int amount)
    {
        Fuel -= amount;
    }

    public void Refuel()
    {
        Fuel = 100;
    }

    public virtual bool ShouldPit()
    {
        return Fuel < 10;
    }

    public abstract bool ShouldOvertake(int currentLap);
    public abstract bool IsOvertakeSuccessful();
    public abstract bool IsDangerous();
}

public class AggressiveRacer : Racer
{
    public AggressiveRacer(string name) : base(name) { }

    public override bool ShouldOvertake(int currentLap)
    {
        return currentLap % 2 == 0;
    }

    public override bool IsOvertakeSuccessful()
    {
        OvertakeAttempts++;
        return OvertakeAttempts % 3 == 0;
    }

    public override bool IsDangerous() => false;
}

public class MomentumRacer : Racer
{
    public MomentumRacer(string name) : base(name) { }

    public override bool ShouldOvertake(int currentLap)
    {
        return currentLap % 5 == 0;
    }

    public override bool IsOvertakeSuccessful()
    {
        OvertakeAttempts++;
        return OvertakeAttempts % 2 == 0;
    }

    public override bool ShouldPit()
    {
        return Fuel < 20;
    }

    public override bool IsDangerous() => false;
}

public class DangerousRacer : Racer
{
    public DangerousRacer(string name) : base(name) { }

    public override bool ShouldOvertake(int currentLap)
    {
        return currentLap % 4 == 0;
    }

    public override bool IsOvertakeSuccessful()
    {
        OvertakeAttempts++;
        return OvertakeAttempts % 4 == 0;
    }

    public override bool ShouldPit()
    {
        return Fuel < 5;
    }

    public override bool IsDangerous() => true;
}

public class CautiousRacer : Racer
{
    public CautiousRacer(string name) : base(name) { }

    public override bool ShouldOvertake(int currentLap)
    {
        return false;
    }

    public override bool IsOvertakeSuccessful()
    {
        return false;
    }

    public override bool ShouldPit()
    {
        return Fuel < 20;
    }

    public override bool IsDangerous() => false;
}