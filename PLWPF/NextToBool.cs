using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Text;

namespace PLWPF
{
    class NextToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strVal = (string)value;
            if (strVal == "3")
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
