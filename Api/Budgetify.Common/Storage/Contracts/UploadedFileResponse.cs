namespace Budgetify.Common.Storage
{
    using System;

    public record UploadedFileResponse(Uri FileUri, string FileName);
}
