using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Tamagotchi_NonchalanTeam.Converters
{
    public class XoColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                if (s == "X") return Brushes.DodgerBlue;
                if (s == "O") return Brushes.Crimson;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

