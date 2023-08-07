using Microsoft.Extensions.Configuration;

namespace Tools;

public static class AppSettings
{
    private static IConfiguration Configuration { get; set; }
    public static string FileNameOfSqliteDb { get; set; }

    static AppSettings()
    {
        var path = Directory.GetCurrentDirectory();

        if (!path.ToString().Contains("API"))
        {
            path = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).Parent.FullName}\\API";
        }

        Configuration = new ConfigurationBuilder().SetBasePath(path)
                                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                          .Build();
        GetSettings();
    }
    public static void Set() { }
    private static void GetSettings()
    {
        FileNameOfSqliteDb = Configuration.GetSection("FileNameOfSqliteDb").Value;
    }
}