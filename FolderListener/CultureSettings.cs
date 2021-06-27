using System.Globalization;

namespace BCLTaskApp
{
    public class CultureSettings
    {
        public void SetCultureInfo()
        {
            var fileWatcherSettings = new FileWatcherSettings();
            var currentCulture = fileWatcherSettings.GetCurrentCulture();

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture(currentCulture.Name);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture(currentCulture.Name);
        }
    }
}
