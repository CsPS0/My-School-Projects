using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Tamagotchi_NonchalanTeam.ViewModels;

namespace Tamagotchi_NonchalanTeam.Views
{
    public partial class SnakeView : UserControl
    {
        public SnakeView()
        {
            InitializeComponent();
            AttachedToVisualTree += OnAttachedToVisualTree;
        }

        private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
            Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (DataContext is SnakeViewModel vm)
            {
                switch (e.Key)
                {
                    case Key.Up:
                    case Key.W:
                        vm.ChangeDirection("up");
                        break;
                    case Key.Down:
                    case Key.S:
                        vm.ChangeDirection("down");
                        break;
                    case Key.Left:
                    case Key.A:
                        vm.ChangeDirection("left");
                        break;
                    case Key.Right:
                    case Key.D:
                        vm.ChangeDirection("right");
                        break;
                }
            }
            base.OnKeyDown(e);
        }
    }

    public class PointToXConverter : IValueConverter
    {
        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is double d) return d * 20;
            if (value is int i) return i * 20.0;
            return 0.0;
        }

        public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is double d) return d / 20;
            return 0;
        }
    }

    public class PointToYConverter : IValueConverter
    {
        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is double d) return d * 20;
            if (value is int i) return i * 20.0;
            return 0.0;
        }

        public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is double d) return d / 20;
            return 0;
        }
    }
}
