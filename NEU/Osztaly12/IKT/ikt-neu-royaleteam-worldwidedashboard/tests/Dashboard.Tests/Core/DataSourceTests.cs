using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Core;

public class DataSourceTests
{
    [Fact]
    public void GetCacheKey_UsesCategoryAndId()
    {
        var source = new FakeDataSource("steam_players", "Steam", "Games");
        Assert.Equal("cache_Games_steam_players", source.GetCacheKey());
    }

    [Fact]
    public void BaseDataSource_StoresProviderMetadata()
    {
        var source = new FakeDataSource("id1", "Test Provider", "Finance");
        Assert.Equal("id1", source.Id);
        Assert.Equal("Test Provider", source.Name);
        Assert.Equal("Finance", source.Category);
    }

    [Fact]
    public async Task FetchDataAsync_ReturnsConfiguredData()
    {
        var expected = new { value = 42 };
        var source = new FakeDataSource("id", "Provider", "Cat", expected);
        var result = await source.FetchDataAsync();
        Assert.Same(expected, result);
    }
}
