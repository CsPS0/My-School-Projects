using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Tamagotchi_NonchalanTeam.Converters
{
    public class FoodStockToOpacityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int foodStock)
            {
                return foodStock > 0 ? 1.0 : 0.5;
            }
            return 1.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

