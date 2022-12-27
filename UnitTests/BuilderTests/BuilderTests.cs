using Cross_Cutting_Task.Builder.BuilderBase;
using Cross_Cutting_Task.Builder.Directors;
using Cross_Cutting_Task.FileItems;

namespace UnitTests.BuilderTests;

public class BuilderTests
{
    private FileBuilder _builder;
    public BuilderTests()
    {
        _builder = new FileBuilder();
    }

    [Fact]
    public void InZipDirectorTest()
    {
        var zipDirector = new InZipDirector(_builder);
        zipDirector.Build("path", "inFileType", "outFileType", "outArchive");

        var expected = new FileItem
        {
            InFilePath = "path",
            InArchiveType = "zip",
            InFileType = "inFileType",
            OutFileType = "outFileType",
            OutArchiveType = "outArchive"
        };
        var actual = _builder.GetFileProduct();
            
            Assert.NotNull(actual);
            Assert.Equal(expected.ToString(), actual.ToString());
    }

    [Fact]
    public void InRarDirectorTest()
    {
        var rarDirector = new InRarDirector(_builder);
        rarDirector.Build("path", "inFileType", "outFileType", "outArchive");

        var expected = new FileItem
        {
            InFilePath = "path",
            InArchiveType = "rar",
            InFileType = "inFileType",
            OutFileType = "outFileType",
            OutArchiveType = "outArchive"
        };
        var actual = _builder.GetFileProduct();
        
        Assert.NotNull(actual);
        Assert.Equal(expected.ToString(), actual.ToString());
    }

    [Fact]
    public void WithoutInArchiveTest()
    {
        var withoutDirector = new WithoutInArchiveDirector(_builder);
        withoutDirector.Build("path", "inFileType", "outFileType", "outArchive");

        var expected = new FileItem
        {
            InFilePath = "path",
            InArchiveType = "None",
            InFileType = "inFileType",
            OutFileType = "outFileType",
            OutArchiveType = "outArchive"
        };
        var actual = _builder.GetFileProduct();
        
        Assert.NotNull(actual);
        Assert.Equal(expected.ToString(), actual.ToString());
    }
}