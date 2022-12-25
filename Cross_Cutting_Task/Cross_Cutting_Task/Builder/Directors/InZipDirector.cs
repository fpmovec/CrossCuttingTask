using Cross_Cutting_Task.Builder.BuilderBase;

namespace Cross_Cutting_Task.Builder.Directors;

public class InZipDirector
{
    private readonly IFileProduct _build;

    public InZipDirector(IFileProduct build)
        => _build = build;

    public void Build(string inPath, string inFileType, string outFileType, string outArchive)
        => _build.InFilePathBuilder(inPath)
                 .InArchiveTypeBuilder("zip")
                 .InFileTypeBuilder(inFileType)
                 .OutFileTypeBuilder(outFileType)
                 .OutArchiveTypeBuilder(outArchive);
    
}