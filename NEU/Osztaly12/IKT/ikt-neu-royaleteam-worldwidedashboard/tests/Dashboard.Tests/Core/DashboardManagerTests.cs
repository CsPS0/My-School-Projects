using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Core;

public class DashboardManagerTests
{
    [Fact]
    public async Task FetchByProviderId_ReturnsNull_WhenProviderDoesNotExist()
    {
        var manager = new DashboardManagerForTests();
        var result = await manager.FetchByProviderIdAsync("missing");
        Assert.Null(result);
    }

    [Fact]
    public async Task FetchByProviderId_ReturnsProviderData_WhenProviderExists()
    {
        var manager = new DashboardManagerForTests();
        var provider = new FakeDataSource("p1", "Provider 1", "Test", new { value = 10 });
        manager.RegisterProvider(provider);
        var result = await manager.FetchByProviderIdAsync("p1");
        Assert.NotNull(result);
        Assert.Equal(1, provider.FetchCount);
    }

    [Fact]
    public async Task FetchAllData_ReturnsDataFromEveryValidProvider()
    {
        var manager = new DashboardManagerForTests();
        manager.RegisterProvider(new FakeDataSource("a", "A", "Cat"));
        manager.RegisterProvider(new FakeDataSource("b", "B", "Cat"));
        var result = await manager.FetchAllDataAsync();
        Assert.True(result.ContainsKey("a"));
        Assert.True(result.ContainsKey("b"));
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task FetchAllData_SkipsProvider_WhenValidationFailsAndNoCacheExists()
    {
        var manager = new DashboardManagerForTests();
        manager.RegisterProvider(new FakeDataSource("bad", "Bad", "Cat", isValid: false));
        var result = await manager.FetchAllDataAsync();
        Assert.Empty(result);
    }

    [Fact]
    public void GetProvidersByCategory_ReturnsOnlyMatchingProviders()
    {
        var manager = new DashboardManagerForTests();
        manager.RegisterProvider(new FakeDataSource("steam", "Steam", "Games"));
        manager.RegisterProvider(new FakeDataSource("music", "Music", "Entertainment"));
        var result = manager.GetProvidersByCategory("Games");
        Assert.Single(result);
        Assert.Equal("steam", result[0].Id);
    }
}
