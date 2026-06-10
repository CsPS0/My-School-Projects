using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using RusztikusAdmin.Models;

namespace RusztikusAdmin.Services
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public User? User { get; set; }
    }

    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "http://localhost:3000";

        // Static token to be shared across services
        public static string? Token { get; private set; }
        public static User? CurrentUser { get; private set; }

        public AuthService()
        {
            _httpClient = new HttpClient();
            ReloadConfig();
        }

        public void ReloadConfig()
        {
            try
            {
                string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server_config.json");
                if (System.IO.File.Exists(configPath))
                {
                    var json = System.IO.File.ReadAllText(configPath);
                    // Use Newtonsoft.Json which is already available in the project
                    var config = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, string>>(json);
                    if (config != null && config.ContainsKey("serverUrl"))
                    {
                        _baseUrl = config["serverUrl"];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService] Error loading config: {ex.Message}");
            }
        }

        public async Task<AuthResult> AdminLogin(string username, string password)
        {
            try
            {
                var loginData = new { username, password };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // Response is expected to be { token: "...", user: { ... } }
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        Token = result.Token;
                        CurrentUser = result.User;

                        // Verify role (although server checks credentials, we double check for app logic)
                        if (CurrentUser?.Role != "admin")
                        {
                            return new AuthResult
                            {
                                Success = false,
                                Message = "Csak adminisztrátorok léphetnek be!"
                            };
                        }

                        return new AuthResult
                        {
                            Success = true,
                            Message = "Sikeres bejelentkezés",
                            User = CurrentUser
                        };
                    }
                }
                
                return new AuthResult
                {
                    Success = false,
                    Message = "Hibás felhasználónév vagy jelszó!"
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = $"Hiba történt: {ex.Message}"
                };
            }
        }

        public void Logout()
        {
            Token = null;
            CurrentUser = null;
        }

        private class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
            public User User { get; set; } = new User();
        }
    }
}