using MvcLab.Factory;
using MvcLab.NetTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcLab.Models
{
    public class SimpleWeatherService
    {
        private readonly IConfiguration _configuration;
        //private readonly ICallAPI _callAPI;
        private ICallAPI _callAPI { get { return CallAPIFactory.Generate(); } }
        public class WeatherData
        {
            public string ZoneName { get; set; }
            public string Status { get; set; }
            public string MaxTemp { get; set; }
            public string MinTemp { get; set; }
        }

        public SimpleWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WeatherData GetTaipeiWeatherFromOpenDataApi()
        {
            //TODO 應改為 async/await
            var json = _callAPI.Get(_configuration["openDataApiUrl"]);
            using (var doc = JsonDocument.Parse(json,
                new JsonDocumentOptions { AllowTrailingCommas = true }))
            {
                var taipeiData = doc
                    .RootElement
                    .GetProperty("records")
                    .GetProperty("location")
                    .EnumerateArray()
                    .Single(o => o.GetProperty("locationName").GetString() == "臺北市")
                    .GetProperty("weatherElement")
                    .EnumerateArray();

                Func<string, string> readParameterName =
                    (elemName) =>
                    taipeiData.Single(o => o.GetProperty("elementName").GetString() == elemName)
                        .GetProperty("time").EnumerateArray().First()
                        .GetProperty("parameter").GetProperty("parameterName").GetString();

                return new WeatherData
                {
                    Status = readParameterName("Wx"),
                    MaxTemp = readParameterName("MaxT"),
                    MinTemp = readParameterName("MinT")
                };
            }
        }

        public WeatherData GetWeatherFromOpenDataApi(string zoneName)
        {
            var json = _callAPI.Get(_configuration["openDataApiUrl"]);
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