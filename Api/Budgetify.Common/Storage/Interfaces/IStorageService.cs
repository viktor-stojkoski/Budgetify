namespace Budgetify.Common.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public interface IStorageService
    {
        /// <summary>
        /// Uploads byte array to storage.
        /// </summary>
        /// <param name="containerId">Name of the storage container.</param>
        /// <param name="fileId">Name of the file.</param>
        /// <param name="content">Byte array of the content.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>Uploaded file</returns>
        Task<UploadedFileResponse> UploadAsync(string containerId, string fileId, byte[] content, string contentType);

        /// <summary>
        /// Downloads file from storage as stream.
        /// </summary>
        /// <param name="containerId">Name of the storage container.</param>
        /// <param name="fileId">Name of the file.</param>
        /// <returns>Stream of the file.</returns>
        Task<Stream> DownloadAsync(string containerId, string fileId);

        /// <summary>
        /// Tries to returns signed URL of the given file using SAS.
        /// If the file is not found, it throws an exception.
        /// </summary>
        /// <param name="containerId">Name of the storage container.</param>
        /// <param name="fileId">Name of the file.</param>
        /// <param name="expiresOn">DateTime after which the signed URL is no longer valid.</param>
        /// <returns>Signed URL</returns>
        Task<Uri> GetSignedUrlAsync(string containerId, string fileId, DateTime expiresOn);

        /// <summary>
        /// Deletes file from storage.
        /// </summary>
        /// <param name="containerId">Name of the storage container.</param>
        /// <param name="fileId">Name of the file.</param>
        /// <returns></returns>
        Task DeleteFileAsync(string containerId, string fileId);

        /// <summary>
        /// Deletes directory from storage.
        /// </summary>
        /// <param name="containerId">Name of the storage container.</param>
        /// <param name="directory">Directory with files/blobs to be deleted.</param>
        /// <returns></returns>
        Task DeleteDirectoryAsync(string containerId, string directory);
    }
}
