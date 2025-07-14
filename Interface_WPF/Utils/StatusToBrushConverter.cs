using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Interface_WPF.Utils
{
    public class StatusToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string status)
            {
                return status switch
                {
                    "Validée" => Brushes.Green,
                    "En attente" => Brushes.Gold,
                    "En préparation" => Brushes.Yellow,
                    "Supprimée" => Brushes.Red,
                    _ => Brushes.Gray
                };
            }
            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
