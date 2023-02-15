namespace Budgetify.Common.ScanReceipt;

using System;
using System.Threading.Tasks;

public interface IScanReceiptService
{
    /// <summary>
    /// Scans receipt and returns all the fields.
    /// </summary>
    Task<ScanReceiptResponse> ScanReceiptAsync(Uri receiptUrl);
}
