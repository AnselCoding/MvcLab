using Microsoft.AspNetCore.SignalR;
using System.Net.Http;

namespace MvcLab.NetTool
{
    public interface ICallAPI
    {
        public string Get(string URL);
    }
}
