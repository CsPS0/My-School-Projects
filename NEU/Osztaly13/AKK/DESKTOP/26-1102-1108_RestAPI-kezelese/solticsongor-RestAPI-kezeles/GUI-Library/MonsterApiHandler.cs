namespace GUI_Library;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class MonsterApiHandler
{
    readonly HttpClient _httpClient;

    public MonsterApiHandler(string base_url)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(base_url)
        };
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public record MonsterItem(string index, string name);
    public record MonsterListResponse(int count, MonsterItem[] results);
    public record MonsterDetail(string index, string name, string type, int hit_points);

    public async Task<MonsterListResponse?> GetMonstersAsync(string path, CancellationToken cancellationToken)
    {
        MonsterListResponse? response = null;
        try
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(path, cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                string str = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
                response = JsonSerializer.Deserialize<MonsterListResponse>(str);
            }
        }
        catch { }

        return response;
    }

    public async Task<MonsterDetail?> GetMonsterDetailAsync(string path, CancellationToken cancellationToken)
    {
        MonsterDetail? response = null;
        try
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(path, cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                string str = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
                response = JsonSerializer.Deserialize<MonsterDetail>(str);
            }
        }
        catch { }

        return response;
    }
}
