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
        static readonly HttpClient httpClient = new HttpClient();
        private readonly IConfiguration _configuration;
        public class WeatherData
        {
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
            var json = httpClient.GetAsync(_configuration["openDataApiUrl"]).Result.Content.ReadAsStringAsync().Result;
            //https://blog.darkthread.net/blog/httpclient-sigleton/
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
    }
}