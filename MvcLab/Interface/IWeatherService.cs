using MvcLab.Models;

namespace MvcLab.Interface
{
    public interface IWeatherService
    {
        WeatherData GetWeatherFromOpenDataApi(string zoneName);
    }
}