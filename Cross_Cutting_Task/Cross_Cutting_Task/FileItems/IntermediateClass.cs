using System.Reflection;
using Cross_Cutting_Task.Builder.BuilderBase;
using Cross_Cutting_Task.Builder.Directors;
using Cross_Cutting_Task.Decorator;


namespace Cross_Cutting_Task.FileItems;

public class IntermediateClass
{
    public int Id { get; set; }
    public string? InFilePath { get; set; }
    public string? InArchiveType { get; set; }
    public string? InFileType { get; set; }
    public string? OutFileType { get; set; }
    public string? OutFileName { get; set; }
    public string? OutArchiveType { get; set; }

    public FileItem FileItemBuild()
    {
        var builder = new FileBuilder();

        if (string.Equals(InArchiveType, "zip"))
        {
            var director = new InZipDirector(builder);
            director.Build(InFilePath, InFileType, OutFileType, OutArchiveType);
        }
        else if (string.Equals(InArchiveType, "rar"))
        {
            var director = new InRarDirector(builder);
            director.Build(InFilePath, InFileType, OutFileType, OutArchiveType);
        }
        else
        {
            var director = new WithoutInArchiveDirector(builder);
            director.Build(InFilePath, InFileType, OutFileType, OutArchiveType);
        }

        return builder.GetFileProduct();    
    }

    // public FileItem FIleOutput(FileItem item)
    // {
    //     // if (item.OutArchiveType.ToUpper().Equals("ZIP"))
    //     // {
    //     //     ZipOutDecorator zipOutDecorator = new ZipOutDecorator(item);
    //     //     
    //     // }
    //     if (item.OutFileType.ToUpper().Equals("TXT"))
    //     {
    //         //TxtOutDecorator
    //     }
    // }
    private void ExpressionParse(FileItem item)
        => item.Result = item.ExpressionParsing();

    private void WriterResults(FileItem item)
    {
        OutFileTypes.TypeInfo typeInfo = new OutFileTypes.TypeInfo();
        typeInfo.TypeOut(item.OutFileType, item);
    }
    public FileItem ArchiveTypeIn(FileItem obj)
    {
        InFileTypes.TypeInfo typeInfo = new InFileTypes.TypeInfo();
        if (obj.InArchiveType.ToUpper().Equals("ZIP"))
        {
            ZipInDecorator zipInDecorator = new ZipInDecorator(obj);
            typeInfo.TypeOut(zipInDecorator.FileImprovement().InFileType,
                zipInDecorator.FileImprovement());
        }
        else if (obj.InArchiveType.ToUpper().Equals("RAR"))
        {
            RarInDecorator rarInDecorator = new RarInDecorator(obj);
            typeInfo.TypeOut(rarInDecorator.FileImprovement().InFileType,
                rarInDecorator.FileImprovement());
        }
        else
        {
            typeInfo.TypeOut(obj.FileImprovement().InFileType,
                obj.FileImprovement());
        }
        ExpressionParse(obj);
        Md5OutDecorator md5 = new Md5OutDecorator(obj);
        obj = md5.FileImprovement();
        WriterResults(obj);
        return obj;
    }
    
}