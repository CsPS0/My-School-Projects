using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Providers;

public class PoliticsProviderTests
{
    [Fact]
    public void Metadata_IsCorrect()
    {
        var provider = new PoliticsProviderForTests();
        Assert.Equal("hungarian_elections", provider.Id);
        Assert.Equal("Hungarian Elections (OGY 2026)", provider.Name);
        Assert.Equal("Politics", provider.Category);
    }

    [Fact]
    public async Task FetchDataAsync_ReturnsFallbackPresentationData()
    {
        var result = await new PoliticsProviderForTests().FetchDataAsync();
        Assert.Equal(68.45, result.TurnoutPercentage);
        Assert.Equal("TISZA", result.LeadingParty);
        Assert.Equal(99.98, result.ProcessedPercentage);
    }

    [Fact]
    public void ValidateResponse_ReturnsTrue_ForPoliticsData()
    {
        var provider = new PoliticsProviderForTests();
        Assert.True(provider.ValidateResponse(new PoliticsData(10, "Party", 50)));
    }

    [Fact]
    public void ValidateResponse_ReturnsFalse_ForWrongData()
    {
        var provider = new PoliticsProviderForTests();
        Assert.False(provider.ValidateResponse(new { turnoutPercentage = 10 }));
    }
}
