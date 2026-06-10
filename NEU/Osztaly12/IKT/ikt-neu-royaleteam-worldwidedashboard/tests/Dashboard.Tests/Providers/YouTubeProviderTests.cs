using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Providers;

public class YouTubeProviderTests
{
    [Fact]
    public void Metadata_IsCorrect()
    {
        var provider = new YouTubeProviderForTests([new ChannelInfo("MrBeast", "abc")]);
        Assert.Equal("youtube_stats", provider.Id);
        Assert.Equal("YouTube Channel Stats", provider.Name);
        Assert.Equal("Video", provider.Category);
    }

    [Fact]
    public void BuildUrl_ContainsAllChannelIdsAndApiKey()
    {
        var provider = new YouTubeProviderForTests([new ChannelInfo("A", "id1"), new ChannelInfo("B", "id2")]);
        var url = provider.BuildUrl("KEY123");
        Assert.Contains("id=id1,id2", url);
        Assert.Contains("key=KEY123", url);
    }

    [Fact]
    public async Task FetchDataAsync_ReturnsMockStatsForEveryChannel()
    {
        var provider = new YouTubeProviderForTests([new ChannelInfo("A", "id1"), new ChannelInfo("B", "id2")]);
        var result = await provider.FetchDataAsync();
        Assert.Equal(2, result.Count);
        Assert.True(result["A"].SubscriberCount > 0);
    }

    [Fact]
    public void ParseItems_MapsStatisticsToChannelNames()
    {
        var provider = new YouTubeProviderForTests([new ChannelInfo("A", "id1")]);
        var result = provider.ParseItems([("id1", "100", "2000", "30")]);
        Assert.Equal(100, result["A"].SubscriberCount);
        Assert.Equal(2000, result["A"].ViewCount);
        Assert.Equal(30, result["A"].VideoCount);
    }

    [Fact]
    public void ValidateResponse_ReturnsFalse_ForEmptyObject()
    {
        var provider = new YouTubeProviderForTests([new ChannelInfo("A", "id1")]);
        Assert.False(provider.ValidateResponse(new Dictionary<string, ChannelStats>()));
    }
}
