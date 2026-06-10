using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RusztikusAdmin.Models;

namespace RusztikusAdmin.Services
{
    public class SettingsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:3000";
        private readonly JsonSerializerSettings _jsonSettings;

        public SettingsService()
        {
            _httpClient = new HttpClient();
            _jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<RestaurantSettings> LoadSettingsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/settings");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RestaurantSettings>(json, _jsonSettings) ?? new RestaurantSettings();
                }
                return new RestaurantSettings();
            }
            catch
            {
                return new RestaurantSettings();
            }
        }

        public async Task SaveSettingsAsync(RestaurantSettings settings)
        {
            if (string.IsNullOrEmpty(AuthService.Token))
            {
                throw new UnauthorizedAccessException("Nincs bejelentkezve!");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/settings");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            request.Content = JsonContent.Create(settings);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Beállítások mentése sikertelen: {response.StatusCode}");
            }
        }
    }
}