using Microsoft.AspNetCore.SignalR;
using ScannedAPI.SignalR.Interfaces;
using System.Threading.Tasks;

namespace ScannedAPI.SignalR
{
    public class MessageHub : Hub<IImageClient>, IMessageHub
    {
        public async Task UploadImage(string uri)
        {
            await Clients.All.ReceiveImage(uri);
        }
    }
}
