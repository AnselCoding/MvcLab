using Microsoft.AspNetCore.Mvc;
using MvcLab.Interface;

namespace MvcLab.ViewComponents
{
    public class WeatherBlockViewComponent : ViewComponent
    {
        private readonly IWeatherService _weatherService;
        public WeatherBlockViewComponent(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        public IViewComponentResult Invoke(string zoneName)
        {
            var data = _weatherService.GetWeatherFromOpenDataApi(zoneName);
            return View(data);
        }
    }
}