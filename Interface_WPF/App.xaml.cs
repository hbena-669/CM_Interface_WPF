using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors.ColorManipulation;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Interface_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var paletteHelper = new PaletteHelper();

            var theme = Theme.Create(
                BaseTheme.Light, 
                SwatchHelper.Lookup[MaterialDesignColor.BlueGrey],
                SwatchHelper.Lookup[MaterialDesignColor.Lime]
    );
            //theme.Acce
            //    (baseTheme, primary, accent);
            paletteHelper.SetTheme(theme);
        }
    }

}
