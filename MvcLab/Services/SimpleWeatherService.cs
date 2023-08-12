using MvcLab.Factory;
using MvcLab.Interface;
using MvcLab.Models;
using System.Text.Json;

namespace MvcLab.Services
{
    public class SimpleWeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        private ICallAPI _callAPI { get { return CallAPIFactory.Generate(); } }

        public SimpleWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region ViewComponents Test1
        //public WeatherData GetTaipeiWeatherFromOpenDataApi()
        //{
        //    //TODO 應改為 async/await
        //    var json = _callAPI.Get(_configuration["openDataApiUrl"]);
        //    using (var doc = JsonDocument.Parse(json,
        //        new JsonDocumentOptions { AllowTrailingCommas = true }))
        //    {
        //        var taipeiData = doc
        //            .RootElement
        //            .GetProperty("records")
        //            .GetProperty("location")
        //            .EnumerateArray()
        //            .Single(o => o.GetProperty("locationName").GetString() == "臺北市")
        //            .GetProperty("weatherElement")
        //            .EnumerateArray();

        //        Func<string, string> readParameterName =
        //            (elemName) =>
        //            taipeiData.Single(o => o.GetProperty("elementName").GetString() == elemName)
        //                .GetProperty("time").EnumerateArray().First()
        //                .GetProperty("parameter").GetProperty("parameterName").GetString();

        //        return new WeatherData
        //        {
        //            Status = readParameterName("Wx"),
        //            MaxTemp = readParameterName("MaxT"),
        //            MinTemp = readParameterName("MinT")
        //        };
        //    }
        //}
        #endregion

        #region ViewComponents Test2 同步
        //public WeatherData GetWeatherFromOpenDataApi(string zoneName)
        //{
        //    var json = _callAPI.Get(_configuration["openDataApiUrl"]);
        //    using (var doc = JsonDocument.Parse(json,
        //        new JsonDocumentOptions { AllowTrailingCommas = true }))
        //    {
        //        var taipeiData = doc
        //            .RootElement
        //            .GetProperty("records")
        //            .GetProperty("location")
        //            .EnumerateArray()
        //            //TODO: 省略比對不到縣市名稱之錯誤處理
        //            .Single(o => o.GetProperty("locationName").GetString() == zoneName)
        //            .GetProperty("weatherElement")
        //            .EnumerateArray();

        //        Func<string, string> readParameterName =
        //            (elemName) =>
        //            taipeiData.Single(o => o.GetProperty("elementName").GetString() == elemName)
        //                .GetProperty("time").EnumerateArray().First()
        //                .GetProperty("parameter").GetProperty("parameterName").GetString();

        //        return new WeatherData
        //        {
        //            ZoneName = zoneName,
        //            Status = readParameterName("Wx"),
        //            MaxTemp = readParameterName("MaxT"),
        //            MinTemp = readParameterName("MinT")
        //        };
        //    }
        //}
        #endregion

        //ViewComponents Test2 非同步
        public async Task<WeatherData> GetWeatherFromOpenDataApi(string zoneName)
        {
            var json = await _callAPI.Get(_configuration["openDataApiUrl"]);
            using (var doc = JsonDocument.Parse(json,
                new JsonDocumentOptions { AllowTrailingCommas = true }))
            {
                var taipeiData = doc
                    .RootElement
                    .GetProperty("records")
                    .GetProperty("location")
                    .EnumerateArray()
                    //TODO: 省略比對不到縣市名稱之錯誤處理
                    .Single(o => o.GetProperty("locationName").GetString() == zoneName)
                    .GetProperty("weatherElement")
                    .EnumerateArray();

                Func<string, string> readParameterName =
                    (elemName) =>
                    taipeiData.Single(o => o.GetProperty("elementName").GetString() == elemName)
                        .GetProperty("time").EnumerateArray().First()
                        .GetProperty("parameter").GetProperty("parameterName").GetString();

                return new WeatherData
                {
                    ZoneName = zoneName,
                    Status = readParameterName("Wx"),
                    MaxTemp = readParameterName("MaxT"),
                    MinTemp = readParameterName("MinT")
                };
            }
        }
    }
}