using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MVVMXamarinApp.Converters
{
    public class TextToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          if(value != null)
          {      
            var text = value.ToString();

            if(text.Contains("UI"))
            {
                return Color.Violet;
            }
         }
            return Color.Firebrick;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
