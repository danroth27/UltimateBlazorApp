using BlazorApp;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066") });
        builder.Services.AddSingleton<IWeatherService, HttpWeatherService>();

        return builder.Build();
    }
}
