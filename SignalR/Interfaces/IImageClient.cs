using ScannedAPI.SignalR.MessageModel;
using System.Threading.Tasks;

namespace ScannedAPI.SignalR.Interfaces
{
    public interface IImageClient
    {
        Task ReceiveImage(string uri);
    }
}
