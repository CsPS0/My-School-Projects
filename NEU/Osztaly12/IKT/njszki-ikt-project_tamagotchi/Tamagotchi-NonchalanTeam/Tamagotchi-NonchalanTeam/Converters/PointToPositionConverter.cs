using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Tamagotchi_NonchalanTeam.ViewModels;

namespace Tamagotchi_NonchalanTeam.Converters
{
    public class PointToPositionConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int x) return (double)x * 20;
            if (value is double dx) return dx * 20;
            if (value is SnakePart sp) return (double)sp.X * 20;
            if (value is Point p) return p.X * 20;
            return 0.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class PointToYPositionConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int y) return (double)y * 20;
            if (value is double dy) return dy * 20;
            if (value is SnakePart sp) return (double)sp.Y * 20;
            if (value is Point p) return p.Y * 20;
            return 0.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

