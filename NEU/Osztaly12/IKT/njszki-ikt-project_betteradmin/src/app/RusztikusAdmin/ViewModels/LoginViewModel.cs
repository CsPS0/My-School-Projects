using System.Windows.Input;
using ReactiveUI;
using RusztikusAdmin.Services;
using RusztikusAdmin.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Linq;

namespace RusztikusAdmin.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _hasError = false;
        private string _serverUrl = "http://localhost:3000";
        private bool _isSettingsOpen = false;
        private string _connectionStatus = string.Empty;
        private bool _isConnecting = false;

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = ReactiveCommand.CreateFromTask(Login);
            ToggleSettingsCommand = ReactiveCommand.Create(() => IsSettingsOpen = !IsSettingsOpen);
            TestConnectionCommand = ReactiveCommand.CreateFromTask(TestConnection);
            
            LoadCurrentConfig();
        }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public bool HasError
        {
            get => _hasError;
            set => this.RaiseAndSetIfChanged(ref _hasError, value);
        }

        public string ServerUrl
        {
            get => _serverUrl;
            set => this.RaiseAndSetIfChanged(ref _serverUrl, value);
        }

        public bool IsSettingsOpen
        {
            get => _isSettingsOpen;
            set => this.RaiseAndSetIfChanged(ref _isSettingsOpen, value);
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => this.RaiseAndSetIfChanged(ref _connectionStatus, value);
        }

        public bool IsConnecting
        {
            get => _isConnecting;
            set => this.RaiseAndSetIfChanged(ref _isConnecting, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand ToggleSettingsCommand { get; }
        public ICommand TestConnectionCommand { get; }
        public event System.Action? LoginSuccessful;

        private void LoadCurrentConfig()
        {
            try
            {
                string configPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "server_config.json");
                if (System.IO.File.Exists(configPath))
                {
                    var json = System.IO.File.ReadAllText(configPath);
                    var config = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, string>>(json);
                    if (config != null && config.ContainsKey("serverUrl"))
                    {
                        ServerUrl = config["serverUrl"];
                    }
                }
            }
            catch { }
        }

        private async System.Threading.Tasks.Task TestConnection()
        {
            IsConnecting = true;
            ConnectionStatus = "Kapcsolódás...";
            
            try
            {
                using var client = new System.Net.Http.HttpClient();
                client.Timeout = System.TimeSpan.FromSeconds(5);
                var response = await client.GetAsync($"{ServerUrl}/test");
                
                if (response.IsSuccessStatusCode)
                {
                    ConnectionStatus = "✅ Kapcsolat OK!";
                    SaveConfig();
                    // Refresh current auth service base URL
                    _authService.ReloadConfig();
                }
                else
                {
                    ConnectionStatus = "❌ Szerver elérhető, de hiba történt.";
                }
            }
            catch (System.Exception ex)
            {
                ConnectionStatus = $"❌ Hiba: {ex.Message}";
            }
            finally
            {
                IsConnecting = false;
            }
        }

        private void SaveConfig()
        {
            try
            {
                string configPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "server_config.json");
                var config = new System.Collections.Generic.Dictionary<string, string> { { "serverUrl", ServerUrl } };
                System.IO.File.WriteAllText(configPath, Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented));
            }
            catch { }
        }

        private async System.Threading.Tasks.Task Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Kérjük, töltse ki az összes mezőt!";
                HasError = true;
                return;
            }

            var loginResult = await _authService.AdminLogin(Username, Password);

            if (loginResult.Success)
            {
                LoginSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = loginResult.Message;
                HasError = true;
                Password = string.Empty;
            }
        }
    }
}