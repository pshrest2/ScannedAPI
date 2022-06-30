using Microsoft.AspNetCore.SignalR;
using RSMessageProcessor.Kafka.Interface;
using ScannedAPI.SignalR;
using ScannedAPI.SignalR.Interfaces;
using System;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Handlers
{
    public class UploadImageHandler : IKafkaHandler<string, string>
    {
        private readonly IHubContext<MessageHub, IImageClient> _hubContext;

        public UploadImageHandler(IHubContext<MessageHub, IImageClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(string key, string value)
        {
            // Here we can actually write the code to register a User  
            Console.WriteLine($"Consuming receipt-image URI: {value}");

            await _hubContext.Clients.All.ReceiveImage(value);
        }
    }
}
