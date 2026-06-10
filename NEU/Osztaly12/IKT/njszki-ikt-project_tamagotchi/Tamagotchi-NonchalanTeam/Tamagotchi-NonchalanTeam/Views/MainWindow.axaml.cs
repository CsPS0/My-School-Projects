using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.ComponentModel;
using Tamagotchi_NonchalanTeam.ViewModels;

namespace Tamagotchi_NonchalanTeam.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            var trayIcons = new TrayIcons();
            var trayIcon = new TrayIcon();

            try
            {
                var uri = new Uri("avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico");
                if (AssetLoader.Exists(uri))
                {
                    using var stream = AssetLoader.Open(uri);
                    trayIcon.Icon = new WindowIcon(stream);
                }
            }
            catch { }

            trayIcon.ToolTipText = "Tamagotchi NonchalanTeam";
            trayIcon.Clicked += TrayIcon_Clicked;

            var menu = new NativeMenu();
            
            var showItem = new NativeMenuItem("Show");
            showItem.Click += Show_Clicked;
            menu.Items.Add(showItem);

            var exitItem = new NativeMenuItem("Exit");
            exitItem.Click += (s, e) =>
            {
                if (Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
                else
                {
                    Environment.Exit(0);
                }
            };
            menu.Items.Add(exitItem);

            trayIcon.Menu = menu;
            trayIcons.Add(trayIcon);

            if (Application.Current != null)
            {
                TrayIcon.SetIcons(Application.Current, trayIcons);
            }
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);
        }

        private void TrayIcon_Clicked(object? sender, System.EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void Show_Clicked(object? sender, System.EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }
    }
}