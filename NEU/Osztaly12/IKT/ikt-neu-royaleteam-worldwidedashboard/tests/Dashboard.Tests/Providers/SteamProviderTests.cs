using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Providers;

public class SteamProviderTests
{
    [Fact]
    public void Metadata_IsCorrect()
    {
        var provider = new SteamProviderForTests(730);
        Assert.Equal("steam_players", provider.Id);
        Assert.Equal("Steam Player Count", provider.Name);
        Assert.Equal("Games", provider.Category);
    }

    [Fact]
    public void Constructor_StoresAppIdInRequestUrl()
    {
        var provider = new SteamProviderForTests(730);
        Assert.Contains("appid=730", provider.BuildUrl());
    }

    [Fact]
    public void ValidateResponse_ReturnsTrue_ForSteamPlayerData()
    {
        var provider = new SteamProviderForTests(730);
        Assert.True(provider.ValidateResponse(new SteamPlayerData(123, 1)));
    }

    [Fact]
    public void ValidateResponse_ReturnsFalse_ForInvalidData()
    {
        var provider = new SteamProviderForTests(730);
        Assert.False(provider.ValidateResponse(new { player_count = 123 }));
    }
}
