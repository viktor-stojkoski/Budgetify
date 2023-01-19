namespace Budgetify.Common.Storage;

using System.Text.Json.Serialization;

using Budgetify.Common.Converters;

public class FileForUploadRequest
{
    public FileForUploadRequest(string name, string type, long size, byte[] content)
    {
        Name = name;
        Type = type;
        Size = size;
        Content = content;
    }

    public string Name { get; set; }

    public string Type { get; set; }

    public long Size { get; set; }

    [JsonConverter(typeof(ByteArrayConverter))]
    public byte[] Content { get; set; }
}
