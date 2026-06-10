namespace Dashboard.Tests.TestDoubles;

public interface IDataSource<T>
{
    string Id { get; }
    string Name { get; }
    string Category { get; }
    Task<T> FetchDataAsync();
    bool ValidateResponse(object? data);
    string GetCacheKey();
}

public abstract class BaseDataSource<T> : IDataSource<T>
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract string Category { get; }
    public abstract Task<T> FetchDataAsync();
    public abstract bool ValidateResponse(object? data);

    public string GetCacheKey() => $"cache_{Category}_{Id}";

    protected Task<T> HandleFetchError(Exception error)
    {
        throw new InvalidOperationException($"Failed to fetch data from {Name}: {error.Message}", error);
    }
}

public sealed class FakeDataSource : BaseDataSource<object>
{
    private readonly object _data;
    private readonly bool _isValid;
    private readonly bool _throws;

    public FakeDataSource(string id, string name, string category, object? data = null, bool isValid = true, bool throws = false)
    {
        Id = id;
        Name = name;
        Category = category;
        _data = data ?? new { value = 1 };
        _isValid = isValid;
        _throws = throws;
    }

    public override string Id { get; }
    public override string Name { get; }
    public override string Category { get; }
    public int FetchCount { get; private set; }

    public override Task<object> FetchDataAsync()
    {
        FetchCount++;
        if (_throws) throw new Exception("Provider failed");
        return Task.FromResult(_data);
    }

    public override bool ValidateResponse(object? data) => _isValid && data is not null;
}

public sealed record CacheEntry(string Data, DateTime UpdatedAt);

public sealed class FakeCacheStore
{
    private readonly Dictionary<string, CacheEntry> _entries = new();
    public int UpsertCount { get; private set; }

    public CacheEntry? Find(string key) => _entries.TryGetValue(key, out var value) ? value : null;

    public void Upsert(string key, object data)
    {
        UpsertCount++;
        _entries[key] = new CacheEntry(System.Text.Json.JsonSerializer.Serialize(data), DateTime.UtcNow);
    }

    public void Set(string key, object data, DateTime updatedAt)
    {
        _entries[key] = new CacheEntry(System.Text.Json.JsonSerializer.Serialize(data), updatedAt);
    }
}

public sealed class DashboardManagerForTests
{
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);
    private readonly Dictionary<string, IDataSource<object>> _providers = new();
    private readonly FakeCacheStore _cache;

    public DashboardManagerForTests(FakeCacheStore? cache = null)
    {
        _cache = cache ?? new FakeCacheStore();
    }

    public void RegisterProvider(IDataSource<object> provider)
    {
        _providers[provider.Id] = provider;
    }

    public async Task<Dictionary<string, object>> FetchAllDataAsync()
    {
        var results = new Dictionary<string, object>();
        foreach (var provider in _providers.Values)
        {
            try
            {
                var data = await FetchWithCacheAsync(provider);
                if (data is not null) results[provider.Id] = data;
            }
            catch { }
        }
        return results;
    }

    public async Task<object?> FetchByProviderIdAsync(string id)
    {
        if (!_providers.TryGetValue(id, out var provider)) return null;
        try { return await FetchWithCacheAsync(provider); }
        catch { return null; }
    }

    public IReadOnlyList<IDataSource<object>> GetProvidersByCategory(string category)
        => _providers.Values.Where(p => p.Category == category).ToList();

    private async Task<object?> FetchWithCacheAsync(IDataSource<object> provider)
    {
        var key = provider.GetCacheKey();
        var cached = _cache.Find(key);
        if (cached is not null && DateTime.UtcNow - cached.UpdatedAt < CacheTtl)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(cached.Data)!;
        }

        var data = await provider.FetchDataAsync();
        if (!provider.ValidateResponse(data))
        {
            return cached is null ? null : System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(cached.Data)!;
        }

        _cache.Upsert(key, data);
        return data;
    }
}

public sealed record FinanceData(CryptoData Crypto, CurrencyData Currency);
public sealed record CryptoData(CryptoCoin Bitcoin, CryptoCoin Ethereum);
public sealed record CryptoCoin(decimal Usd, decimal Eur);
public sealed record CurrencyData(decimal EurToHuf, decimal EurToUsd);

public sealed class FinanceProviderForTests : BaseDataSource<FinanceData>
{
    public override string Id => "finance_markets";
    public override string Name => "Global Finance Markets";
    public override string Category => "Finance";
    public override Task<FinanceData> FetchDataAsync() => Task.FromResult(new FinanceData(new CryptoData(new CryptoCoin(100, 90), new CryptoCoin(50, 45)), new CurrencyData(390, 1.08m)));
    public override bool ValidateResponse(object? data) => data is FinanceData f && f.Crypto is not null && f.Currency is not null;
}

public sealed record MusicTrack(int Rank, string Title, string Artist, long SpotifyStreams, long SoundcloudStreams, long TotalStreams);
public sealed class MusicProviderForTests : BaseDataSource<List<MusicTrack>>
{
    public override string Id => "global_music";
    public override string Name => "Global Top 10 Music";
    public override string Category => "Entertainment";
    public override Task<List<MusicTrack>> FetchDataAsync()
    {
        var raw = new[]
        {
            ("Blinding Lights", "The Weeknd", 4100000000L, 250000000L),
            ("Shape of You", "Ed Sheeran", 3800000000L, 190000000L),
            ("One Dance", "Drake", 2900000000L, 400000000L)
        };
        var list = raw.Select(t => new MusicTrack(0, t.Item1, t.Item2, t.Item3, t.Item4, t.Item3 + t.Item4))
            .OrderByDescending(t => t.TotalStreams)
            .Select((t, i) => t with { Rank = i + 1 })
            .ToList();
        return Task.FromResult(list);
    }
    public override bool ValidateResponse(object? data) => data is List<MusicTrack> { Count: > 0 } tracks && tracks[0].Title.Length > 0 && tracks[0].Artist.Length > 0 && tracks[0].TotalStreams > 0;
}

public sealed record PoliticsData(double TurnoutPercentage, string LeadingParty, double ProcessedPercentage);
public sealed class PoliticsProviderForTests : BaseDataSource<PoliticsData>
{
    public override string Id => "hungarian_elections";
    public override string Name => "Hungarian Elections (OGY 2026)";
    public override string Category => "Politics";
    public Task<PoliticsData> ParseOrFallbackAsync(string? html)
    {
        if (!string.IsNullOrWhiteSpace(html) && html.Contains("turnout-value") && html.Contains("processed-value") && html.Contains("leading-party-name"))
        {
            return Task.FromResult(new PoliticsData(70.5, "TEST", 88.2));
        }
        return FetchDataAsync();
    }
    public override Task<PoliticsData> FetchDataAsync() => Task.FromResult(new PoliticsData(68.45, "TISZA", 99.98));
    public override bool ValidateResponse(object? data) => data is PoliticsData p && p.LeadingParty.Length > 0;
}

public sealed record SteamPlayerData(int PlayerCount, int Result);
public sealed class SteamProviderForTests : BaseDataSource<SteamPlayerData>
{
    public int AppId { get; }
    public SteamProviderForTests(int appId) => AppId = appId;
    public override string Id => "steam_players";
    public override string Name => "Steam Player Count";
    public override string Category => "Games";
    public string BuildUrl() => $"https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid={AppId}";
    public SteamPlayerData ParseResponse(Dictionary<string, object> response) => (SteamPlayerData)response["response"];
    public override Task<SteamPlayerData> FetchDataAsync() => Task.FromResult(new SteamPlayerData(1234, 1));
    public override bool ValidateResponse(object? data) => data is SteamPlayerData;
}

public sealed record ChannelInfo(string Name, string Id);
public sealed record ChannelStats(string Name, int SubscriberCount, long ViewCount, int VideoCount);
public sealed class YouTubeProviderForTests : BaseDataSource<Dictionary<string, ChannelStats>>
{
    private readonly List<ChannelInfo> _channels;
    public YouTubeProviderForTests(IEnumerable<ChannelInfo> channels) => _channels = channels.ToList();
    public override string Id => "youtube_stats";
    public override string Name => "YouTube Channel Stats";
    public override string Category => "Video";
    public string BuildUrl(string apiKey) => $"https://www.googleapis.com/youtube/v3/channels?part=statistics&id={string.Join(',', _channels.Select(c => c.Id))}&key={apiKey}";
    public override Task<Dictionary<string, ChannelStats>> FetchDataAsync() => Task.FromResult(_channels.ToDictionary(c => c.Name, c => new ChannelStats(c.Name, 260000000, 45000000000, 800)));
    public Dictionary<string, ChannelStats> ParseItems(IEnumerable<(string id, string subscriberCount, string viewCount, string videoCount)> items)
    {
        var result = new Dictionary<string, ChannelStats>();
        foreach (var item in items)
        {
            var channel = _channels.FirstOrDefault(c => c.Id == item.id);
            if (channel is null) continue;
            result[channel.Name] = new ChannelStats(channel.Name, int.Parse(item.subscriberCount), long.Parse(item.viewCount), int.Parse(item.videoCount));
        }
        return result;
    }
    public override bool ValidateResponse(object? data) => data is Dictionary<string, ChannelStats> d && d.Count > 0;
}
