using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class ResolutionOption : ObservableObject
    {
        public string Label { get; set; } = "";
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public enum WindowMode
    {
        Windowed,
        Borderless,
        BorderlessFullscreen,
        Fullscreen
    }

    public partial class SettingsViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private List<ResolutionOption> _resolutions;

        [ObservableProperty]
        private ResolutionOption _selectedResolution;

        [ObservableProperty]
        private List<string> _windowModes;

        [ObservableProperty]
        private string _selectedWindowMode;

        public bool IsResolutionEnabled => SelectedWindowMode == "Windowed" || SelectedWindowMode == "Borderless";

        public SettingsViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
            
            _resolutions = new List<ResolutionOption>
            {
                new ResolutionOption { Label = "1280 x 720", Width = 1280, Height = 720 },
                new ResolutionOption { Label = "1600 x 900", Width = 1600, Height = 900 },
                new ResolutionOption { Label = "1920 x 1080", Width = 1920, Height = 1080 },
                new ResolutionOption { Label = "2560 x 1440", Width = 2560, Height = 1440 }
            };

            _windowModes = new List<string> { "Windowed", "Borderless", "Borderless Fullscreen", "Fullscreen" };

            var window = GetMainWindow();
            if (window != null)
            {
                if (window.WindowState == WindowState.FullScreen)
                    _selectedWindowMode = "Fullscreen";
                else if (window.WindowState == WindowState.Maximized && window.SystemDecorations == SystemDecorations.None)
                    _selectedWindowMode = "Borderless Fullscreen";
                else if (window.SystemDecorations == SystemDecorations.None)
                    _selectedWindowMode = "Borderless";
                else
                    _selectedWindowMode = "Windowed";

                _selectedResolution = _resolutions.FirstOrDefault(r => 
                    (int)window.Width == r.Width && (int)window.Height == r.Height) ?? _resolutions[0];
            }
            else
            {
                _selectedWindowMode = "Windowed";
                _selectedResolution = _resolutions[0];
            }
        }

        private Window? GetMainWindow()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return desktop.MainWindow;
            }
            return null;
        }

        partial void OnSelectedResolutionChanged(ResolutionOption value)
        {
            var window = GetMainWindow();
            if (window != null && (SelectedWindowMode == "Windowed" || SelectedWindowMode == "Borderless"))
            {
                window.Width = value.Width;
                window.Height = value.Height;
                CenterWindow(window, value.Width, value.Height);
            }
        }

        partial void OnSelectedWindowModeChanged(string value)
        {
            OnPropertyChanged(nameof(IsResolutionEnabled));
            var window = GetMainWindow();
            if (window == null) return;

            switch (value)
            {
                case "Windowed":
                    window.WindowState = WindowState.Normal;
                    window.SystemDecorations = SystemDecorations.Full;
                    window.Width = SelectedResolution.Width;
                    window.Height = SelectedResolution.Height;
                    CenterWindow(window, SelectedResolution.Width, SelectedResolution.Height);
                    break;
                case "Borderless":
                    window.WindowState = WindowState.Normal;
                    window.SystemDecorations = SystemDecorations.None;
                    window.Width = SelectedResolution.Width;
                    window.Height = SelectedResolution.Height;
                    CenterWindow(window, SelectedResolution.Width, SelectedResolution.Height);
                    break;
                case "Borderless Fullscreen":
                    window.SystemDecorations = SystemDecorations.None;
                    window.WindowState = WindowState.Maximized;
                    break;
                case "Fullscreen":
                    window.SystemDecorations = SystemDecorations.Full;
                    window.WindowState = WindowState.FullScreen;
                    break;
            }
        }

        private void CenterWindow(Window window, int width, int height)
        {
            var screen = window.Screens.ScreenFromVisual(window);
            if (screen != null)
            {
                window.Position = new PixelPoint(
                    (screen.WorkingArea.Width - (int)(width * screen.Scaling)) / 2,
                    (screen.WorkingArea.Height - (int)(height * screen.Scaling)) / 2
                );
            }
        }

        [RelayCommand]
        private void OpenManageSaves()
        {
            _navigateTo(new ManageSavesViewModel(_navigateTo));
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new MainMenuViewModel(_navigateTo));
        }
    }
}
