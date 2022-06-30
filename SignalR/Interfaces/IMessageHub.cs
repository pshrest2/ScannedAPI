using System.Threading.Tasks;

namespace ScannedAPI.SignalR.Interfaces
{
    public interface IMessageHub
    {
        Task UploadImage(string uri);
    }
}
