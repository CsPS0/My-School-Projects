namespace selfCheckoutLib;

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price:C}";
    }
}

public class SelfCheckout
{
    private List<Product> _items = new List<Product>();
    private bool _isActive;

    public bool IsActive => _isActive;

    public void StartNewPurchase()
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Egy vásárlás már folyamatban van.");
        }
        _items.Clear();
        _isActive = true;
    }

    public void ScanProduct(string name, double price)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Nincs aktív vásárlás.");
        }
        if (price < 0)
        {
            throw new ArgumentException("Az ár nem lehet negatív.");
        }
        _items.Add(new Product(name, price));
    }

    public bool DeleteProduct(string name)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Nincs aktív vásárlás.");
        }
        var item = _items.FirstOrDefault(p => p.Name == name);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }

    public double GetTotal()
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Nincs aktív vásárlás.");
        }
        return _items.Sum(p => p.Price);
    }

    public List<Product> GetItems()
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Nincs aktív vásárlás.");
        }
        return new List<Product>(_items);
    }

    public double Pay(double amountPaid)
    {
        if (!_isActive)
        {
            throw new InvalidOperationException("Nincs aktív vásárlás.");
        }
        
        double total = GetTotal();
        if (amountPaid < total)
        {
            throw new ArgumentException("A befizetett összeg kevesebb, mint a végösszeg.");
        }

        double change = amountPaid - total;
        _isActive = false;
        _items.Clear();
        return change;
    }
}
