using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcLab.Models;

namespace MvcLab.ViewComponents
{
    public class WeatherBlockViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public WeatherBlockViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IViewComponentResult Invoke(string zoneName)
        {
            var svc = new SimpleWeatherService(_configuration);
            var data = svc.GetWeatherFromOpenDataApi(zoneName);
            return View(data);
        }
    }
}