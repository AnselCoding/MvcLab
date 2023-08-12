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

        //同步作法
        //public IViewComponentResult Invoke(string zoneName)
        //{
        //    var data = _weatherService.GetWeatherFromOpenDataApi(zoneName);
        //    return View(data);
        //}

        //非同步作法
        public async Task<IViewComponentResult> InvokeAsync(string zoneName)
        {
            var data = await _weatherService.GetWeatherFromOpenDataApi(zoneName);
            return View(data);
        }
    }
}