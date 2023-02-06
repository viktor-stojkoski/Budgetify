namespace Budgetify.Common.ScanReceipt;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;

public class ScanReceiptService : IScanReceiptService
{
    private readonly FormRecognizerClient _formRecognizerClient;

    public ScanReceiptService(Uri endpoint, string key)
    {
        _formRecognizerClient = new FormRecognizerClient(
            endpoint: endpoint,
            credential: new AzureKeyCredential(key));
    }

    public async Task<ScanReceiptResponse> ScanReceiptAsync(Uri receiptUrl)
    {
        ScanReceiptResponse response = new();

        Response<RecognizedFormCollection> recogizedForms =
            await _formRecognizerClient.StartRecognizeReceiptsFromUriAsync(
                receiptUri: receiptUrl,
                recognizeReceiptsOptions: new RecognizeReceiptsOptions
                {
                    IncludeFieldElements = true
                }).WaitForCompletionAsync();

        foreach (RecognizedForm form in recogizedForms.Value)
        {
            response.AttachmentFields = GetAttachmentFields(form.Fields);
        }

        return response;
    }

    private static AttachmentFields GetAttachmentFields(IReadOnlyDictionary<string, FormField> formFields)
    {
        AttachmentFields attachmentFields = new();

        foreach (KeyValuePair<string, FormField> field in formFields.Where(x => x.Value is not null))
        {
            switch (field.Key)
            {
                case "MerchantName":
                    attachmentFields.MerchantName = field.Value.ValueData;
                    break;
                case "TotalAmount":
                    attachmentFields.TotalAmount = decimal.Parse(field.Value.ValueData);
                    break;
                case "Date":
                    attachmentFields.Date = DateTime.Parse(field.Value.ValueData);
                    break;
                default:
                    break;
            }
        }

        return attachmentFields;
    }
}
