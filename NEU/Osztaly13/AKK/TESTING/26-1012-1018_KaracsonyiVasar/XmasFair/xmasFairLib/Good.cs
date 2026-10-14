namespace xmasFairLib;

public class Good
{
    public string Name { get; }
    public string ProductType { get; }
    public int Value { get; }

    public Good(string name, string productType, int value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A termék neve nem lehet üres.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(productType))
        {
            throw new ArgumentException("A terméktípus nem lehet üres.", nameof(productType));
        }

        Name = name;
        ProductType = productType;
        Value = value < 0 ? 0 : value;
    }
}