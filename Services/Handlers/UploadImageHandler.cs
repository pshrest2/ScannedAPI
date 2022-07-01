using Microsoft.AspNetCore.SignalR;
using RSMessageProcessor.RabbitMQ.Interface;
using ScannedAPI.SignalR;
using ScannedAPI.SignalR.Interfaces;
using System;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Handlers
{
    public class UploadImageHandler : IRabbitHandler<string>
    {
        private readonly IHubContext<MessageHub, IImageClient> _hubContext;

        public UploadImageHandler(IHubContext<MessageHub, IImageClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleAsync(string message)
        {
            // Here we can actually write the code to register a User  
            Console.WriteLine($"Consuming receipt-image URI: {message}");

            await _hubContext.Clients.All.ReceiveImage(message);
        }
    }
}
