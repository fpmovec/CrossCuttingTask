using Cross_Cutting_Task.Builder.BuilderBase;

namespace Cross_Cutting_Task.Builder.Directors;

public class InRarDirector
{
    private readonly IFileProduct _build;

    public InRarDirector(IFileProduct build)
        => _build = build;

    public void Build(string inPath, string inFileType, string outFileType, string outArchive)
        => _build.InFilePathBuilder(inPath)
            .InArchiveTypeBuilder("rar")
            .InFileTypeBuilder(inFileType)
            .OutFileTypeBuilder(outFileType)
            .OutArchiveTypeBuilder(outArchive);
}