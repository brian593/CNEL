using System;
namespace LaLuz.Utils
{
    public static class DBConection
    {
        public static string GetDatabaseRoute(string DatabaseName)
        {
            string databaseRoute = string.Empty;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                databaseRoute = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                databaseRoute = Path.Combine(databaseRoute, DatabaseName);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                databaseRoute = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                databaseRoute = Path.Combine(databaseRoute, "..", "Library", DatabaseName);
            }
            return databaseRoute;

        }

    }
}

