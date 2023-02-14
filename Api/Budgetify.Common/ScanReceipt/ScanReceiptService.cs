namespace Budgetify.Common.ScanReceipt;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

public class ScanReceiptService : IScanReceiptService
{
    private readonly DocumentAnalysisClient _formRecognizerClient;
    private readonly string _modelId;

    public ScanReceiptService(Uri endpoint, string key, string modelId)
    {
        _formRecognizerClient = new DocumentAnalysisClient(
            endpoint: endpoint,
            credential: new AzureKeyCredential(key));
        _modelId = modelId;
    }

    public async Task<ScanReceiptResponse> ScanReceiptAsync(Uri receiptUrl)
    {
        ScanReceiptResponse response = new();

        AnalyzeDocumentOperation analyzedDocuments =
            await _formRecognizerClient.AnalyzeDocumentFromUriAsync(
                waitUntil: WaitUntil.Completed,
                modelId: _modelId,
                documentUri: receiptUrl);

        foreach (AnalyzedDocument? form in analyzedDocuments.Value.Documents)
        {
            response.AttachmentFields = GetAttachmentFields(form);
        }

        return response;
    }

    private static AttachmentFields GetAttachmentFields(AnalyzedDocument document)
    {
        AttachmentFields attachmentFields = new();

        foreach (KeyValuePair<string, DocumentField> field in document.Fields.Where(x => x.Value is not null))
        {
            switch (field.Key)
            {
                case "MerchantName":
                    attachmentFields.MerchantName = field.Value.Content;
                    break;
                case "TotalAmount":
                    attachmentFields.TotalAmount = decimal.Parse(field.Value.Content);
                    break;
                case "Date":
                    attachmentFields.Date = DateTime.Parse(field.Value.Content);
                    break;
                default:
                    break;
            }
        }

        return attachmentFields;
    }
}
