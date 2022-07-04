using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.AspNetCore.Http;
using ScannedAPI.Dtos;
using ScannedAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScannedAPI.Services
{
    public class FormRecognizerService : IFormRecognizerService
    {
        private readonly FormRecognizerClient _client; 

        public FormRecognizerService(FormRecognizerClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async Task<ReceiptDto> AnalyzeReceipt(IFormFile file)
        {
            Stream fileStream = file.OpenReadStream();
            var receiptData = await _client.StartRecognizeReceiptsAsync(fileStream).WaitForCompletionAsync();
            return GetReceiptData(receiptData);
        }

        public async Task<ReceiptDto> AnalyzeReceipt(string url)
        {
            var receiptData = await _client.StartRecognizeReceiptsFromUriAsync(new Uri(url)).WaitForCompletionAsync();
            return GetReceiptData(receiptData);
        }

        private static ReceiptDto GetReceiptData(RecognizedFormCollection receipts)
        {
            ReceiptDto receiptDto = new();
            foreach (RecognizedForm receipt in receipts)
            {
                if (receipt.Fields.TryGetValue("MerchantName", out FormField merchantNameField))
                {
                    if (merchantNameField.Value.ValueType == FieldValueType.String)
                    {
                        string merchantName = merchantNameField.Value.AsString();
                        receiptDto.Merchant = merchantName;

                        Console.WriteLine($"Merchant Name: '{merchantName}', with confidence {merchantNameField.Confidence}");
                    }
                }

                if (receipt.Fields.TryGetValue("TransactionDate", out FormField transactionDateField))
                {
                    if (transactionDateField.Value.ValueType == FieldValueType.Date)
                    {
                        DateTime transactionDate = transactionDateField.Value.AsDate();
                        receiptDto.TransactionDate = transactionDate;

                        Console.WriteLine($"Transaction Date: '{transactionDate}', with confidence {transactionDateField.Confidence}");
                    }
                }

                if (receipt.Fields.TryGetValue("Items", out FormField itemsField))
                {
                    if (itemsField.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField itemField in itemsField.Value.AsList())
                        {
                            var item = new Item();
                            Console.WriteLine("Item:");

                            if (itemField.Value.ValueType == FieldValueType.Dictionary)
                            {
                                IReadOnlyDictionary<string, FormField> itemFields = itemField.Value.AsDictionary();

                                if (itemFields.TryGetValue("Name", out FormField itemNameField))
                                {
                                    if (itemNameField.Value.ValueType == FieldValueType.String)
                                    {
                                        string itemName = itemNameField.Value.AsString();
                                        item.Name = itemName;

                                        Console.WriteLine($"    Name: '{itemName}', with confidence {itemNameField.Confidence}");
                                    }
                                }

                                if (itemFields.TryGetValue("TotalPrice", out FormField itemTotalPriceField))
                                {
                                    if (itemTotalPriceField.Value.ValueType == FieldValueType.Float)
                                    {
                                        float itemTotalPrice = itemTotalPriceField.Value.AsFloat();
                                        item.Price = itemTotalPrice;

                                        Console.WriteLine($"    Total Price: '{itemTotalPrice}', with confidence {itemTotalPriceField.Confidence}");
                                    }
                                }
                                item.Id = Guid.NewGuid();
                                receiptDto.Items.Add(item);
                            }
                        }
                    }
                }
                if (receipt.Fields.TryGetValue("Total", out FormField totalField))
                {
                    if (totalField.Value.ValueType == FieldValueType.Float)
                    {
                        float total = totalField.Value.AsFloat();
                        receiptDto.Total = total;

                        Console.WriteLine($"Total: '{total}', with confidence '{totalField.Confidence}'");
                    }
                }
            }
            return receiptDto;
        }
    }
}
