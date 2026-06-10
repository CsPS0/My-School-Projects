using Dashboard.Tests.TestDoubles;

namespace Dashboard.Tests.Providers;

public class FinanceProviderTests
{
    [Fact]
    public void Metadata_IsCorrect()
    {
        var provider = new FinanceProviderForTests();
        Assert.Equal("finance_markets", provider.Id);
        Assert.Equal("Global Finance Markets", provider.Name);
        Assert.Equal("Finance", provider.Category);
    }

    [Fact]
    public async Task FetchDataAsync_ReturnsCryptoAndCurrencyData()
    {
        var provider = new FinanceProviderForTests();
        var result = await provider.FetchDataAsync();
        Assert.True(result.Crypto.Bitcoin.Usd > 0);
        Assert.True(result.Crypto.Ethereum.Eur > 0);
        Assert.True(result.Currency.EurToHuf > 0);
    }

    [Fact]
    public void ValidateResponse_ReturnsTrue_ForFinanceData()
    {
        var provider = new FinanceProviderForTests();
        var data = new FinanceData(new CryptoData(new CryptoCoin(1, 1), new CryptoCoin(1, 1)), new CurrencyData(390, 1));
        Assert.True(provider.ValidateResponse(data));
    }

    [Fact]
    public void ValidateResponse_ReturnsFalse_ForWrongShape()
    {
        var provider = new FinanceProviderForTests();
        Assert.False(provider.ValidateResponse(new { crypto = "bad" }));
    }
}
