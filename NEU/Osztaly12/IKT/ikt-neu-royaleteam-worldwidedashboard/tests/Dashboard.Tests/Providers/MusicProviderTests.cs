using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Providers;

public class MusicProviderTests
{
    [Fact]
    public void Metadata_IsCorrect()
    {
        var provider = new MusicProviderForTests();
        Assert.Equal("global_music", provider.Id);
        Assert.Equal("Global Top 10 Music", provider.Name);
        Assert.Equal("Entertainment", provider.Category);
    }

    [Fact]
    public async Task FetchDataAsync_AssignsRanksStartingFromOne()
    {
        var tracks = await new MusicProviderForTests().FetchDataAsync();
        Assert.Equal(1, tracks[0].Rank);
        Assert.Equal(2, tracks[1].Rank);
    }

    [Fact]
    public async Task FetchDataAsync_SortsByTotalStreamsDescending()
    {
        var tracks = await new MusicProviderForTests().FetchDataAsync();
        Assert.True(tracks[0].TotalStreams >= tracks[1].TotalStreams);
        Assert.True(tracks[1].TotalStreams >= tracks[2].TotalStreams);
    }

    [Fact]
    public async Task FetchDataAsync_CalculatesTotalStreams()
    {
        var first = (await new MusicProviderForTests().FetchDataAsync())[0];
        Assert.Equal(first.SpotifyStreams + first.SoundcloudStreams, first.TotalStreams);
    }

    [Fact]
    public void ValidateResponse_ReturnsFalse_ForEmptyList()
    {
        var provider = new MusicProviderForTests();
        Assert.False(provider.ValidateResponse(new List<MusicTrack>()));
    }
}
