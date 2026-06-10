# Dashboard.Tests

C# xUnit tests based on the logic of the `src/lib/core` and `src/lib/providers` TypeScript files.

## Core

### DashboardManagerTests.cs

* **GetProviders_ReturnsAllRegisteredProviders**

  * Verifies that the DashboardManager returns all registered providers.

* **GetProviderById_ReturnsCorrectProvider**

  * Verifies that the correct provider is returned for a given identifier.

* **GetProviderById_ReturnsNullForUnknownProvider**

  * Verifies that a null value is returned for a non-existent provider.

* **AddProvider_IncreasesProviderCount**

  * Verifies that the stored provider count increases when a new provider is added.

### DataSourceTests.cs

* **Metadata_IsStoredCorrectly**

  * Verifies that the data source metadata is stored correctly.

* **FetchData_ReturnsExpectedResult**

  * Verifies that the data fetching method returns the expected data.

* **FetchData_ReturnsCollection**

  * Verifies that the returned result is in a list or collection format.

* **FetchData_HandlesEmptyData**

  * Verifies that no error occurs even with an empty dataset.

---

## Providers

### FinanceProviderTests.cs

* **Metadata_IsCorrect**

  * Verifies the provider's identifier, name, and category.

* **FetchData_ReturnsItems**

  * Verifies that the data fetch returns a result.

* **FetchData_ContainsRequiredFields**

  * Verifies that every item contains the necessary fields.

* **FetchData_ReturnsSortedResults**

  * Verifies that the data arrives in the correct order.

### MusicProviderTests.cs

* **Metadata_IsCorrect**

  * Verifies the provider's basic metadata.

* **FetchDataAsync_AssignsRanksStartingFromOne**

  * Verifies that ranking starts from one.

* **FetchDataAsync_SortsByTotalStreamsDescending**

  * Verifies that tracks are listed in descending order based on stream count.

* **FetchDataAsync_ReturnsExpectedTrackCount**

  * Verifies that the expected number of tracks is returned.

### PoliticsProviderTests.cs

* **Metadata_IsCorrect**

  * Verifies the provider's identifier and category.

* **FetchData_ReturnsPoliticalArticles**

  * Verifies that political-themed data is returned.

* **FetchData_ReturnsNonEmptyCollection**

  * Verifies that the returned list is not empty.

* **FetchData_ContainsValidTitles**

  * Verifies that every record has a title.

### SteamProviderTests.cs

* **Metadata_IsCorrect**

  * Verifies the metadata of the Steam provider.

* **FetchData_ReturnsGames**

  * Verifies that a list of games is returned.

* **FetchData_SortsGamesByPlayers**

  * Verifies that the games are sorted by player count.

* **FetchData_ReturnsExpectedGameCount**

  * Verifies the number of games returned.

### YouTubeProviderTests.cs

* **Metadata_IsCorrect**

  * Verifies the basic data of the YouTube provider.

* **FetchData_ReturnsVideos**

  * Verifies that a list of videos is returned.

* **FetchData_ContainsVideoTitles**

  * Verifies that every video has a title.

* **FetchData_ReturnsSortedResults**

  * Verifies that the videos arrive in the desired order.

---

## Execution

### Rider

1. Open the `Dashboard.Tests.csproj` file.
2. Wait for the NuGet packages to download.
3. Right-click on the project → **Run Tests**.

### Terminal

```bash
cd Dashboard.Tests
dotnet test
```
