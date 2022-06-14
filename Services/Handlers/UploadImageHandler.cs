using RSMessageProcessor.Kafka.Interface;
using System;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Handlers
{
    public class UploadImageHandler : IKafkaHandler<string, string>
    {
        public Task HandleAsync(string key, string value)
        {
            // Here we can actually write the code to register a User  
            Console.WriteLine($"Consuming receipt-image topic message with the below data\n value: {value}");

            // after receiving image data, use signalR to send it to the React App.
            return Task.CompletedTask;
        }
    }
}
