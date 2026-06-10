using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Tamagotchi_NonchalanTeam.Converters
{
    public class ImagePathToDownscaledBitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string path || string.IsNullOrEmpty(path))
                return null;

            int targetWidth = 128;
            if (parameter is string paramStr && int.TryParse(paramStr, out int pWidth))
            {
                targetWidth = pWidth;
            }

            string[] assemblyNames = { "Tamagotchi-NonchalanTeam", "Tamagotchi_NonchalanTeam" };

            foreach (var assemblyName in assemblyNames)
            {
                try
                {
                    string currentPath = path;
                    if (path.StartsWith("avares://"))
                    {
                        currentPath = path.Replace("Tamagotchi-NonchalanTeam", assemblyName).Replace("Tamagotchi_NonchalanTeam", assemblyName);
                    }

                    var uri = new Uri(currentPath);
                    if (AssetLoader.Exists(uri))
                    {
                        using (var stream = AssetLoader.Open(uri))
                        {
                            return Bitmap.DecodeToWidth(stream, targetWidth);
                        }
                    }
                }
                catch
                {
                    // ha nem megy hát nem megy
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

