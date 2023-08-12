using MvcLab.Interface;
using MvcLab.Models;

namespace MvcLab.Services
{
    public class TestWeatherService : IWeatherService
    {
        // 同步作法
        //public WeatherData GetWeatherFromOpenDataApi(string zoneName)
        //{
        //    return new WeatherData()
        //    {
        //        ZoneName = zoneName,
        //        Status = "Good Day",
        //        MinTemp = "-18",
        //        MaxTemp = "38"
        //    };
        //}

        // 非同步作法
        public Task<WeatherData> GetWeatherFromOpenDataApi(string zoneName)
        {
            // 同步轉非同步
            return Task.FromResult<WeatherData>(new WeatherData()
            {
                ZoneName = zoneName,
                Status = "Good Day",
                MinTemp = "-18",
                MaxTemp = "38"
            });
        }
    }
}
