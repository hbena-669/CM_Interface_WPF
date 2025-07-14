
using System.Globalization;
using System.Windows.Data;
using global::Interface_WPF.Content.Models;


namespace Interface_WPF.Utils
{
    public class EnumCategoryDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Toutes les catégories";

            if (value is EnumCategories category)
                return category.ToString(); // Ou remplace par une version plus lisible si besoin

            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // Pas nécessaire ici
        }
    }
}

