using Microsoft.AspNetCore.SignalR;
using System.Net.Http;

namespace MvcLab.Interface
{
    public interface ICallAPI
    {
        public Task<string> Get(string URL);
    }
}
