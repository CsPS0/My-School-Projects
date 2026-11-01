using Microsoft.VisualStudio.TestTools.UnitTesting;
using selfCheckoutLib;

namespace solticsongor_SelfCheckoutUnitTest;

[TestClass]
public class SelfCheckoutTests
{
    private SelfCheckout _checkout = null!;

    [TestInitialize]
    public void Setup()
    {
        _checkout = new SelfCheckout();
    }

    [TestMethod]
    public void StartPurchase_SetsActive()
    {
        _checkout.StartNewPurchase();
        Assert.IsTrue(_checkout.IsActive);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void StartPurchase_ThrowsIfActive()
    {
        _checkout.StartNewPurchase();
        _checkout.StartNewPurchase();
    }

    [TestMethod]
    public void ScanProduct_AddsToList()
    {
        _checkout.StartNewPurchase();
        _checkout.ScanProduct("Tej", 400);
        
        var items = _checkout.GetItems();
        Assert.AreEqual(1, items.Count);
        Assert.AreEqual("Tej", items[0].Name);
        Assert.AreEqual(400, items[0].Price);
    }

    [TestMethod]
    public void DeleteProduct_RemovesItem()
    {
        _checkout.StartNewPurchase();
        _checkout.ScanProduct("Tej", 400);
        _checkout.ScanProduct("Kenyér", 600);
        
        bool removed = _checkout.DeleteProduct("Tej");
        
        Assert.IsTrue(removed);
        var items = _checkout.GetItems();
        Assert.AreEqual(1, items.Count);
        Assert.AreEqual("Kenyér", items[0].Name);
    }

    [TestMethod]
    public void GetTotal_ReturnsCorrectSum()
    {
        _checkout.StartNewPurchase();
        _checkout.ScanProduct("Tej", 400);
        _checkout.ScanProduct("Kenyér", 600);
        
        Assert.AreEqual(1000, _checkout.GetTotal());
    }

    [TestMethod]
    public void Pay_ReturnsChangeAndResets()
    {
        _checkout.StartNewPurchase();
        _checkout.ScanProduct("Tej", 400);
        
        double change = _checkout.Pay(500);
        
        Assert.AreEqual(100, change);
        Assert.IsFalse(_checkout.IsActive);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Pay_ThrowsIfInsufficient()
    {
        _checkout.StartNewPurchase();
        _checkout.ScanProduct("Tej", 400);
        _checkout.Pay(300);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ScanProduct_ThrowsIfInactive()
    {
        _checkout.ScanProduct("Tej", 400);
    }
}
