namespace Budgetify.Common.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Azure;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Blobs.Specialized;
    using Azure.Storage.Sas;

    using Budgetify.Common.Extensions;

    public class AzureStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task DeleteDirectoryAsync(string containerId, string directory)
        {
            CheckStringArgument(containerId, nameof(containerId));
            CheckStringArgument(directory, nameof(directory));

            BlobContainerClient container = await GetContainerAsync(containerId);

            IAsyncEnumerator<BlobHierarchyItem> blobEnumerator =
                container.GetBlobsByHierarchyAsync(prefix: directory).GetAsyncEnumerator();

            List<BlobItem> result = new();

            while (await blobEnumerator.MoveNextAsync())
            {
                if (blobEnumerator.Current.IsBlob)
                {
                    result.Add(blobEnumerator.Current.Blob);
                }
            }

            foreach (BlobItem blob in result)
            {
                await container.GetBlobClient(blob.Name).DeleteIfExistsAsync();
            }
        }

        public async Task DeleteFileAsync(string containerId, string fileId)
        {
            CheckStringArgument(containerId, nameof(containerId));
            CheckStringArgument(fileId, nameof(fileId));

            BlobContainerClient container = await GetContainerAsync(containerId);

            BlobClient blob = container.GetBlobClient(fileId);

            await blob.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadAsync(string containerId, string fileId)
        {
            CheckStringArgument(containerId, nameof(containerId));
            CheckStringArgument(fileId, nameof(fileId));

            BlobContainerClient container = await GetContainerAsync(containerId);

            BlobClient blob = container.GetBlobClient(fileId);

            if (!await blob.ExistsAsync())
            {
                throw new FileNotFoundException($"File with name {fileId} does not exist.");
            }

            Response<BlobDownloadResult> stream = await blob.DownloadContentAsync();

            return stream.Value.Content.ToStream();
        }

        public async Task<Uri> GetSignedUrlAsync(string containerId, string fileId, DateTime expiresOn)
        {
            CheckStringArgument(containerId, nameof(containerId));
            CheckStringArgument(fileId, nameof(fileId));
            CheckExpiresOnArgument(expiresOn, nameof(expiresOn));

            BlobContainerClient container = await GetContainerAsync(containerId);

            BlobClient blob = container.GetBlobClient(fileId);

            if (!await blob.ExistsAsync())
            {
                throw new FileNotFoundException($"File with name {fileId} does not exist.");
            }

            if (!blob.CanGenerateSasUri)
            {
                throw new AccessViolationException(
                    $"BlobClient must be authorized with Shared Key credentials to create a service SAS.");
            }

            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = blob.GetParentBlobContainerClient().Name,
                BlobName = blob.Name,
                ExpiresOn = expiresOn
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            return blob.GenerateSasUri(sasBuilder);
        }

        public async Task<UploadedFileResponse> UploadAsync(string containerId, string fileId, byte[] content, string contentType)
        {
            CheckStringArgument(containerId, nameof(containerId));
            CheckStringArgument(fileId, nameof(fileId));
            CheckContentArgument(content, nameof(content));
            CheckStringArgument(contentType, nameof(contentType));

            BlobContainerClient container = await GetContainerAsync(containerId);

            string fileName = RemoveUnsupportedCharacters(fileId);

            BlobClient blob = container.GetBlobClient(fileName);

            using Stream fileStream = new MemoryStream(content);

            BlobHttpHeaders blobHttpHeader = GetBlobHttpHeaders(contentType);

            await blob.UploadAsync(fileStream, blobHttpHeader);

            return new UploadedFileResponse(
                FileUri: blob.Uri,
                FileName: fileName);
        }

        /// <summary>
        /// Returns the blob container client if found.
        /// </summary>
        private async Task<BlobContainerClient> GetContainerAsync(string containerId)
        {
            BlobContainerClient container =
                _blobServiceClient.GetBlobContainerClient(containerId);

            if (!await container.ExistsAsync())
            {
                throw new DirectoryNotFoundException($"Container with name {containerId} does not exist.");
            }

            return container;
        }

        /// <summary>
        /// Removes unsupported characters.
        /// </summary>
        private static string RemoveUnsupportedCharacters(string name)
        {
            IEnumerable<char> invalidChars = Path.GetInvalidPathChars()
                .Union(Path.GetInvalidFileNameChars())
                .Where(x => x != '/');

            return string.Concat(name.Split(invalidChars.ToArray()));
        }

        /// <summary>
        /// Returns Blob HTTP Headers with the given parameters.
        /// </summary>
        private static BlobHttpHeaders GetBlobHttpHeaders(string contentType)
        {
            BlobHttpHeaders blobHttpHeaders = new();

            blobHttpHeaders.ContentType = contentType;

            return blobHttpHeaders;
        }

        private static void CheckStringArgument(string argumentValue, string argumentName)
        {
            if (argumentValue.IsEmpty())
            {
                throw new ArgumentException($"Argument {argumentName} is required.");
            }
        }

        private static void CheckContentArgument<T>(T argumentValue, string argumentName)
        {
            if (Equals(argumentValue, default(T)))
            {
                throw new ArgumentException($"Argument {argumentName} is required.");
            }
        }

        private static void CheckExpiresOnArgument(DateTime expiresOn, string argumentName)
        {
            if (expiresOn.Kind is not DateTimeKind.Utc || expiresOn < DateTime.UtcNow)
            {
                throw new ArgumentException($"Argument {argumentName} must be UTC kind and reference moment in the future.");
            }
        }
    }
}
