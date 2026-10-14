namespace xmasFairLib;

public class Stand
{
    private readonly List<string> _productTypes;
    private readonly List<Good> _goods = [];

    public string Name { get; }
    public int NumGoods => _goods.Count;
    public int DailyIncome { get; private set; }

    public Stand(string name, string productType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A stand neve nem lehet üres.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(productType))
        {
            throw new ArgumentException("A terméktípus nem lehet üres.", nameof(productType));
        }

        Name = name;
        _productTypes = [productType];
    }

    public Stand(string name, List<string> productTypes)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A stand neve nem lehet üres.", nameof(name));
        }

        if (productTypes == null || productTypes.Count == 0 || productTypes.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("A terméktípus nem lehet üres.", nameof(productTypes));
        }

        Name = name;
        _productTypes = [..productTypes];
    }

    public void Add(Good? good)
    {
        if (good == null)
        {
            throw new ArgumentNullException(nameof(good));
        }

        if (!_productTypes.Contains(good.ProductType))
        {
            throw new ArgumentException("Nem árul a stand ilyen terméket.", nameof(good));
        }

        if (_goods.Any(g => g.Name == good.Name))
        {
            throw new ArgumentException("Ilyen nevű termék már hozzá lett adva a standhoz.", nameof(good));
        }

        _goods.Add(good);
    }

    public bool Sell(string productName, int quantity)
    {
        Good? good = _goods.FirstOrDefault(g => g.Name == productName);
        if (good == null)
        {
            throw new ArgumentException("Nincs ilyen termék, nem lehet eladni.", nameof(productName));
        }

        if (quantity <= 0)
        {
            return false;
        }

        DailyIncome += good.Value * quantity;
        return true;
    }
}