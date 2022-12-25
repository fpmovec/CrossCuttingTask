using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.Builder.BuilderBase;

public interface IFileProduct
{
    FileBuilder InFilePathBuilder(string? path);
    FileBuilder InArchiveTypeBuilder(string? inArchiveType);
    FileBuilder InFileTypeBuilder(string? inFileType);
    FileBuilder OutFileTypeBuilder(string? outFileType);
    FileBuilder OutArchiveTypeBuilder(string? outArchiveType);
    FileItem GetFileProduct();
}