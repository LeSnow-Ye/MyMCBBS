using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace MyMCBBS.Utils
{
    class EnumToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (byte)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
