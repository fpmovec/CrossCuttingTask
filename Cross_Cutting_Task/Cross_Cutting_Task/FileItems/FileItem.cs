using System.Text;
using AngouriMath;
using AngouriMath.Extensions;
using Cross_Cutting_Task.Decorator;
using Newtonsoft.Json;

namespace Cross_Cutting_Task.FileItems;

public class FileItem : IFileImprovement
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
    
    public override string ToString() 
       => new StringBuilder()
            .Append(InFilePath + "\n")
            .Append(InArchiveType + "\n")
            .Append(InFileType + "\n")
            .Append(OutFileType + "\n")
            .Append(OutArchiveType)
            .ToString();

    public double ExpressionParsing()
        =>  (double)((Entity)GetExpression()).EvalNumerical();
    public FileItem FileImprovement()
        => this;
    public void SetExpression(string exp)
        => Expression = exp;

    public string? GetExpression()
        => Expression;
}