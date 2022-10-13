namespace Gomar.Utils
{
    public static class AppSettings
    {
        public static string ApplicationUrl { get; } = ConfigurationHelper.config.GetSection("ApplicationUrl").Value;
    }
}
