using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ScannedAPI.Dtos;
using ScannedAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScannedAPI.Services
{
    public class FormRecognizer : IFormRecognizer
    {
        private FormRecognizerClient _client;

        public FormRecognizer(FormRecognizerClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async Task<ReceiptDto> AnalyzeReceipt(IFormFile file)
        {
            Stream fileStream = file.OpenReadStream();
            var receipts = await _client.StartRecognizeReceipts(fileStream).WaitForCompletionAsync();
            var receiptDto = GetReceiptData(receipts);
            return receiptDto;
        }

        private ReceiptDto GetReceiptData(RecognizedFormCollection receipts)
        {
            ReceiptDto receiptDto = new ReceiptDto();
            foreach (RecognizedForm receipt in receipts)
            {
                FormField merchantNameField;
                if (receipt.Fields.TryGetValue("MerchantName", out merchantNameField))
                {
                    if (merchantNameField.Value.ValueType == FieldValueType.String)
                    {
                        string merchantName = merchantNameField.Value.AsString();
                        receiptDto.Merchant = merchantName;

                        Console.WriteLine($"Merchant Name: '{merchantName}', with confidence {merchantNameField.Confidence}");
                    }
                }

                FormField transactionDateField;
                if (receipt.Fields.TryGetValue("TransactionDate", out transactionDateField))
                {
                    if (transactionDateField.Value.ValueType == FieldValueType.Date)
                    {
                        DateTime transactionDate = transactionDateField.Value.AsDate();
                        receiptDto.TransactionDate = transactionDate;

                        Console.WriteLine($"Transaction Date: '{transactionDate}', with confidence {transactionDateField.Confidence}");
                    }
                }

                FormField itemsField;
                if (receipt.Fields.TryGetValue("Items", out itemsField))
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

                                FormField itemNameField;
                                if (itemFields.TryGetValue("Name", out itemNameField))
                                {
                                    if (itemNameField.Value.ValueType == FieldValueType.String)
                                    {
                                        string itemName = itemNameField.Value.AsString();
                                        item.Name = itemName;

                                        Console.WriteLine($"    Name: '{itemName}', with confidence {itemNameField.Confidence}");
                                    }
                                }

                                FormField itemTotalPriceField;
                                if (itemFields.TryGetValue("TotalPrice", out itemTotalPriceField))
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
                FormField totalField;
                if (receipt.Fields.TryGetValue("Total", out totalField))
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
