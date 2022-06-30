using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using RSMessageProcessor.Kafka.Interface;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ScannedAPI.Services
{
    public class UploadImageService : BackgroundService
    {
        private readonly string topic = "receipt-image";
        private readonly IKafkaConsumer<string, string> _kafkaConsumer;
        public UploadImageService(IKafkaConsumer<string, string> kafkaConsumer)
        {
            _kafkaConsumer = kafkaConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaConsumer.Consume(topic, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {topic}, {ex}");
            }
        }
    }
}
