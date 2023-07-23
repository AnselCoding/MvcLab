using MvcLab.Models;

namespace MvcLab.ViewComponents
{
    public class TaipeiWeatherViewComponent
    {
        private readonly IConfiguration _configuration;
        public TaipeiWeatherViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Invoke()
        {
            //TODO: 應改用 DI https://blog.darkthread.net/blog/aspnet-core-di-notes/
            var svc = new SimpleWeatherService(_configuration);
            var data = svc.GetTaipeiWeatherFromOpenDataApi();

            return $"現在台北天氣：{data.Status} /氣溫：{data.MinTemp}° - {data.MaxTemp}°";
        }
    }
}