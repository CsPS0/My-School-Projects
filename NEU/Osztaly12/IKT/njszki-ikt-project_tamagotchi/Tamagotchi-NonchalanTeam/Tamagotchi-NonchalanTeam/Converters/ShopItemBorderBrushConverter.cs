using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Tamagotchi_NonchalanTeam.Converters
{
    public class ShopItemBorderBrushConverter : IMultiValueConverter
    {
        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count < 2) return Brushes.Transparent;

            bool isSelected = values[0] is bool s && s;
            bool isOwned = values[1] is bool o && o;

            if (isSelected) return Brushes.Orange;
            if (isOwned) return Brushes.Green;
            return Brushes.Red;
        }
    }
}

