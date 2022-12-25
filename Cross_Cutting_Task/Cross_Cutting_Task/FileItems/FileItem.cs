using Newtonsoft.Json;
using System.Text.Json.Serialization;
namespace Cross_Cutting_Task.FileItems;

public class FileItem
{
    public int Id { get; set; }
    public string? InFilePath { get; set; }
    public string? InArchiveType { get; set; }
    public string? InFileType { get; set; }
    public string? OutFileType { get; set; }
    public string? OutFileName { get; set; }
    public string? OutArchiveType { get; set; }
    [JsonProperty("MD5_Result")]
    public string? EncryptedResult { get; set; }
    [JsonProperty("Expression")]
    private string? Expression { get; set; }
    public Stream? archiveInStream;
    public Stream? archiveOutStream;
}