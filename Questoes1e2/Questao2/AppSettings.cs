using Microsoft.Extensions.Configuration;

namespace Questao2;

public static class AppSettings
{
    private static IConfiguration Configuration { get; set; }
    public static string UrlApi { get; set; }

    static AppSettings()
    {
        Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                          .Build();
        SetAppSettings();
    }
    public static void Set() { }
    private static void SetAppSettings()
    {
        UrlApi = Configuration.GetSection("UrlApi").Value;
    }
}