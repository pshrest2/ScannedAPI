using Microsoft.Extensions.Hosting;
using RSMessageProcessor.RabbitMQ.Interface;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ScannedAPI.Services
{
    public class UploadImageService : BackgroundService
    {
        private readonly IRabbitConsumer<string> _rabbitConsumer;

        public UploadImageService(IRabbitConsumer<string> rabbitConsumer)
        {
            _rabbitConsumer = rabbitConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _rabbitConsumer.ConsumeMessage("ReceiptImage", cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError}: {ex}");
            }
        }
    }
}
