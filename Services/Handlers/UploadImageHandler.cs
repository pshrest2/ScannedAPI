using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using RSMessageProcessor.Kafka.Interface;
using ScannedAPI.SignalR.Interfaces;
using System;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Handlers
{
    public class UploadImageHandler : IKafkaHandler<string, string>
    {
        private readonly IMessageHub _messageHub;

        public UploadImageHandler(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }

        public async Task HandleAsync(string key, string value)
        {
            // Here we can actually write the code to register a User  
            Console.WriteLine($"Consuming receipt-image URI: {value}");

            await _messageHub.UploadImage(value);
        }
    }
}
