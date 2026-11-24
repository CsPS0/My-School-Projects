using System.Text;
using System.Text.Json;
using VedettLib;

namespace VedettConsole;

internal class RestApiHandler : IVedettAllatStore
{
    HttpClient client;
    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public RestApiHandler(string url = "http://localhost:5243/")
    {
        client = new HttpClient() { BaseAddress = new Uri(url) };
    }

    public int AddNew(VedettAllat allat)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(allat), Encoding.UTF8, "application/json");
        var res = client.PostAsync("api/animals", content).Result;
        if (!res.IsSuccessStatusCode)
        {
            return 0;
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return int.Parse(body);
    }

    public bool Delete(int id)
    {
        var res = client.DeleteAsync("api/animals/" + id).Result;
        return res.IsSuccessStatusCode;
    }

    public IEnumerable<VedettAllat> GetAll()
    {
        var res = client.GetAsync("api/animals").Result;
        if (!res.IsSuccessStatusCode)
        {
            return [];
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<VedettAllat[]>(body, options) ?? [];
    }

    public VedettAllat? GetById(int id)
    {
        var res = client.GetAsync("api/animalsById/" + id).Result;
        if (!res.IsSuccessStatusCode)
        {
            return null;
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<VedettAllat>(body, options);
    }

    public IEnumerable<VedettAllat> GetByYear(int year)
    {
        var res = client.GetAsync("api/animalsByYear/" + year).Result;
        if (!res.IsSuccessStatusCode)
        {
            return [];
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<VedettAllat[]>(body, options) ?? [];
    }

    public IEnumerable<VedettAllat> GetByCategory(string category)
    {
        var res = client.GetAsync("api/animalsByCategory/" + Uri.EscapeDataString(category)).Result;
        if (!res.IsSuccessStatusCode)
        {
            return [];
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<VedettAllat[]>(body, options) ?? [];
    }

    public IEnumerable<string> GetCategories()
    {
        var res = client.GetAsync("api/categories").Result;
        if (!res.IsSuccessStatusCode)
        {
            return [];
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<string[]>(body) ?? [];
    }

    public bool Update(int id, VedettAllat newAllat)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(newAllat), Encoding.UTF8, "application/json");
        var res = client.PutAsync("api/animals/" + id, content).Result;
        return res.IsSuccessStatusCode;
    }
}
