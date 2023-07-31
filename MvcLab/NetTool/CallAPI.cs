using Microsoft.AspNetCore.SignalR;
using System.Net.Http;

namespace MvcLab.NetTool
{
    public class CallAPI : ICallAPI
    {
        static readonly HttpClient httpClient = new HttpClient();
        public string Get(string URL)
        {
            var json = httpClient.GetAsync(URL).Result.Content.ReadAsStringAsync().Result;
            return json;
        }
    }
}
