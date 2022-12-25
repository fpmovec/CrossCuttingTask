using Cross_Cutting_Task.Builder.BuilderBase;

namespace Cross_Cutting_Task.Builder.Directors;

public class WithoutInArchiveDirector
{
    private readonly IFileProduct _build;

    public WithoutInArchiveDirector(IFileProduct build)
        => _build = build;

    public void Build(string inPath, string inFileType, string outFileType, string outArchive)
        => _build.InFilePathBuilder(inPath)
            .InFileTypeBuilder(inFileType)
            .OutFileTypeBuilder(outFileType)
            .OutArchiveTypeBuilder(outArchive);
}