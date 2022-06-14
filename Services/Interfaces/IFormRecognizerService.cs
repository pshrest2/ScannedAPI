using Microsoft.AspNetCore.Http;
using ScannedAPI.Dtos;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Interfaces
{
    public interface IFormRecognizerService
    {
        Task<ReceiptDto> AnalyzeReceipt(IFormFile file);
    }
}
