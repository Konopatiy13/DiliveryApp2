using System;
using System.Globalization;
using System.Windows.Data;

namespace DiliveryApp2._0
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null; // возвращает true, если значение не null
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // не реализуйте обратное преобразование, если это не нужно
        }
    }
}

