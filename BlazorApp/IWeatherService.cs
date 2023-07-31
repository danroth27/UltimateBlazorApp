namespace BlazorApp;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetForecastAsync();
}
