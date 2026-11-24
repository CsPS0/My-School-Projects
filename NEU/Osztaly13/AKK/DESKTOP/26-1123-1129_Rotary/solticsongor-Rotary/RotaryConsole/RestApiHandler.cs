using System.Text;
using System.Text.Json;
using RotaryLib;

namespace RotaryConsole;

internal class RestApiHandler : IVersenyzoStore
{
    HttpClient client;
    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public RestApiHandler(string url = "http://localhost:5250/")
    {
        client = new HttpClient() { BaseAddress = new Uri(url) };
    }

    public IEnumerable<Versenyzo> GetAll()
    {
        var res = client.GetAsync("api/results").Result;
        if (!res.IsSuccessStatusCode)
        {
            return [];
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<Versenyzo[]>(body, options) ?? [];
    }

    public Versenyzo? GetById(string rajtsz)
    {
        var res = client.GetAsync("api/results/" + Uri.EscapeDataString(rajtsz)).Result;
        if (!res.IsSuccessStatusCode)
        {
            return null;
        }
        var body = res.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<Versenyzo>(body, options);
    }

    public bool AddNew(Versenyzo versenyzo)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(versenyzo), Encoding.UTF8, "application/json");
        var res = client.PostAsync("api/results", content).Result;
        return res.IsSuccessStatusCode;
    }
}
