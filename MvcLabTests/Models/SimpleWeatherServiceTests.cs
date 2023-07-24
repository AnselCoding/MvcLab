using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcLab.Models.Tests
{
    [TestClass()]
    public class SimpleWeatherServiceTests
    {
        [TestMethod()]
        public void GetTaipeiWeatherFromOpenDataApiTest()
        {
            // get config data from appsettings.json
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();

            // create SimpleWeatherService object
            var svc = new SimpleWeatherService(config);
            var data = svc.GetTaipeiWeatherFromOpenDataApi();

            // check weather data is not null
            Assert.IsNotNull(data);
        }
    }
}