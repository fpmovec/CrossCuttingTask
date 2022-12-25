using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.Builder.BuilderBase;

public class FileBuilder : IFileProduct
{
    private FileItem file;

    public FileBuilder()
        => file = new FileItem();

    public FileBuilder InFilePathBuilder(string? path)
    {
        file.InFilePath = path;
        return this;
    }

    public FileBuilder InArchiveTypeBuilder(string? aType)
    {
        file.InArchiveType = aType;
        return this;
    }

    public FileBuilder InFileTypeBuilder(string? fType)
    {
        file.InFileType = fType;
        return this;
    }

    public FileBuilder OutFileTypeBuilder(string? ofType)
    {
        file.OutFileType = ofType;
        return this;
    }

    public FileBuilder OutArchiveTypeBuilder(string? oaType)
    {
        file.OutArchiveType = oaType;
        return this;
    }

    public FileItem GetFileProduct()
    {
        FileItem fileProduct = file;
        file = new FileItem();
        return fileProduct;
    }
}