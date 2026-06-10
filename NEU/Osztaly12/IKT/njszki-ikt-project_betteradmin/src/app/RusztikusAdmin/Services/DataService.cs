using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RusztikusAdmin.Models;

namespace RusztikusAdmin.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl;
        private readonly JsonSerializerSettings _jsonSettings;

        public DataService()
        {
            _httpClient = new HttpClient();
            _baseUrl = "http://localhost:3000"; // Default
            ReloadConfig();

            _jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public void ReloadConfig()
        {
            try
            {
                string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server_config.json");
                if (System.IO.File.Exists(configPath))
                {
                    var json = System.IO.File.ReadAllText(configPath);
                    var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (config != null && config.ContainsKey("serverUrl"))
                    {
                        _baseUrl = config["serverUrl"];
                        Console.WriteLine($"[DataService] Using server URL: {_baseUrl}");
                    }
                }
                else
                {
                    // Create default config if it doesn't exist
                    var defaultConfig = new Dictionary<string, string> { { "serverUrl", "http://localhost:3000" } };
                    System.IO.File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultConfig, Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DataService] Error loading config: {ex.Message}");
            }
        }

        private void EnsureLoggedIn()
        {
            if (string.IsNullOrEmpty(AuthService.Token))
            {
                throw new Exception("Not logged in (Token missing). Please log in first.");
            }
        }

        // Bookings
        public async Task<List<Booking>> LoadBookingsAsync()
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/bookings");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Booking>>(json, _jsonSettings) ?? new List<Booking>();
            }
            else
            {
                Console.WriteLine($"[DataService] Error loading bookings: {response.StatusCode}");
                return new List<Booking>();
            }
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/bookings", booking);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add booking: {response.StatusCode}");
            }
        }

        public async Task DeleteBookingAsync(string id)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/bookings/{id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete booking: {response.StatusCode}");
            }
        }

        public async Task SaveBookingsAsync(List<Booking> bookings)
        {
            // No-op, since we use individual operations
        }

        // Menu Items
        public async Task<List<MenuItem>> LoadMenuAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/menu");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MenuItem>>(json, _jsonSettings) ?? new List<MenuItem>();
            }
            else
            {
                Console.WriteLine($"[DataService] Error loading menu: {response.StatusCode}");
                return new List<MenuItem>();
            }
        }

        public async Task AddMenuItemAsync(MenuItem item)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/menu");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            request.Content = JsonContent.Create(item);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add menu item: {response.StatusCode}");
            }
        }

        public async Task DeleteMenuItemAsync(long id)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/menu/{id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete menu item: {response.StatusCode}");
            }
        }

        public async Task SaveMenuAsync(List<MenuItem> menuItems)
        {
            // No-op
        }

        // Tables
        public async Task<List<Table>> LoadTablesAsync()
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/tables");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Table>>(json, _jsonSettings) ?? new List<Table>();
            }
            else
            {
                Console.WriteLine($"[DataService] Error loading tables: {response.StatusCode}");
                return new List<Table>();
            }
        }

        public async Task AddTableAsync(Table table)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/tables");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            request.Content = JsonContent.Create(table);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add table: {response.StatusCode}");
            }
        }

        public async Task DeleteTableAsync(long id)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/tables/{id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete table: {response.StatusCode}");
            }
        }

        public async Task SaveTablesAsync(List<Table> tables)
        {
            // No-op
        }

        // Users
        public async Task<List<User>> LoadUsersAsync()
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/users");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(json, _jsonSettings) ?? new List<User>();
            }
            else
            {
                Console.WriteLine($"[DataService] Error loading users: {response.StatusCode}");
                return new List<User>();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            EnsureLoggedIn();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/users/{id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to delete user: {errorMsg}");
            }
        }
    }
}