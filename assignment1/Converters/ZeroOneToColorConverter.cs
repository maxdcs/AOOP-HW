using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace assignment1.Converters
{
    public class ZeroOneToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() == "1" ? Brushes.Black : Brushes.White;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
