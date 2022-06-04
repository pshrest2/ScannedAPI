using Microsoft.AspNetCore.SignalR;
using ScannedAPI.SignalR.Interfaces;
using ScannedAPI.SignalR.MessageModel;
using System.Threading.Tasks;

namespace ScannedAPI.SignalR
{
    public class MessageHub : Hub<IImageClient>
    {
        public async Task UploadImage(ImageMessage message)
        {
            await Clients.All.ReceiveImage(message);
        }
    }
}
