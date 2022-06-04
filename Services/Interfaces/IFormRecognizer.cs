using Azure.AI.FormRecognizer.Models;
using Microsoft.AspNetCore.Http;
using ScannedAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScannedAPI.Services.Interfaces
{
    public interface IFormRecognizer
    {
        Task<ReceiptDto> AnalyzeReceipt(IFormFile file);
    }
}
