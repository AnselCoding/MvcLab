using MvcLab.Interface;
using MvcLab.Models;

namespace MvcLab.Services
{
    public class TestWeatherService : IWeatherService
    {
        public WeatherData GetWeatherFromOpenDataApi(string zoneName)
        {
            return new WeatherData()
            {
                ZoneName = zoneName,
                Status = "Good Day",
                MinTemp = "-18",
                MaxTemp = "38"
            };
        }
    }
}
