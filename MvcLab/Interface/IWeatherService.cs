using MvcLab.Models;

namespace MvcLab.Interface
{
    public interface IWeatherService
    {
        //同步作法
        //WeatherData GetWeatherFromOpenDataApi(string zoneName);

        //非同步作法
        Task<WeatherData> GetWeatherFromOpenDataApi(string zoneName);
    }
}