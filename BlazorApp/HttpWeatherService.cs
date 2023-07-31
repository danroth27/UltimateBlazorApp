using System.Net.Http.Json;

namespace BlazorApp;

public class HttpWeatherService : IWeatherService
{
    HttpClient httpClient;

    public HttpWeatherService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<WeatherForecast[]> GetForecastAsync()
    {
        var weather = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weather");
        return weather ?? Array.Empty<WeatherForecast>();
    }
}
