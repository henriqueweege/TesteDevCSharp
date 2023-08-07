using Microsoft.Extensions.Configuration;

namespace Questao1;

public static class AppSettings
{
    private static IConfiguration Configuration { get; set; }
    public static double Fee { get; set; }

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
        Fee = double.Parse(Configuration.GetSection("Fee").Value);

    }
}