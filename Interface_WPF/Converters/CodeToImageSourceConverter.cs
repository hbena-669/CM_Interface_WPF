using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Interface_WPF.Converters
{
    class CodeToImageSourceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            string code = value.ToString();
            string path = $"pack://application:,,,/Assets/Etablissements/{code}.jpg";

            try
            {
                return new BitmapImage(new Uri(path));
            }
            catch
            {
                return null; // ou une image par défaut
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
