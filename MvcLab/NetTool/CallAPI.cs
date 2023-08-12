using Microsoft.AspNetCore.SignalR;
using MvcLab.Interface;
using System.Net.Http;

namespace MvcLab.NetTool
{
    public class CallAPI : ICallAPI
    {
        static readonly HttpClient httpClient = new HttpClient();

        //同步作法
        //public string Get(string URL)
        //{
        //    // 加上 .Result 就可以將非同步方法轉為同步化(註：更完美的做法是 GetAwaiter().GetResult())
        //    var json = httpClient.GetAsync(URL).Result.Content.ReadAsStringAsync().Result;
        //    return json;
        //}

        //非同步作法
        public async Task<string> Get(string URL)
        {
            var resp = await httpClient.GetAsync(URL);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return json;
        }
    }
}
