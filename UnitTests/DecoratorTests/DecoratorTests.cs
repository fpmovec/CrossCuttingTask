using Cross_Cutting_Task.FileItems;
using Cross_Cutting_Task.Decorator;


namespace UnitTests.DecoratorTests;

public class DecoratorTests
{
    private FileItem _item;
    private TxtOutDecorator? _txtOutDecorator;
    private XmlOutDecorator? _xmlOutDecorator;
    private JsonOutDecorator? _jsonOutDecorator;
    private ZipOutDecorator? _zipOutDecorator;

    public DecoratorTests()
    {
        _item = new FileItem()
        {
            InFilePath = "D:\\input.xml",
            InArchiveType = "None",
            InFileType = "xml",
            OutFileName = "name",
            OutFileType = "xml",
            OutArchiveType = "None",
            Result = 9,
            EncryptedResult = "encResult"
        };
    }
    [Fact]
    public void XmlOutDecoratorTest()
    {
        _xmlOutDecorator = new XmlOutDecorator(_item);
        var obj = _xmlOutDecorator.FileImprovement();
        var expectedExtension = ".XML";

        var isExist = File.Exists(obj.OutFileName);
        var actualExtension = Path.GetExtension(obj.OutFileName);
        Assert.True(isExist);
        Assert.Equal(expectedExtension, actualExtension?.ToUpper());
    }

    [Fact]
    public void JsonOutDecoratorTest()
    {
        _jsonOutDecorator = new JsonOutDecorator(_item);
        var obj = _jsonOutDecorator.FileImprovement();
        var expectedExtension = ".JSON";
        
        var isExist = File.Exists(obj.OutFileName);
        var actualExtension = Path.GetExtension(obj.OutFileName);
        
        Assert.True(isExist);
        Assert.Equal(expectedExtension, actualExtension?.ToUpper());
    }

    [Fact]
    public void TxtOutDecoratorTest()
    {
        _txtOutDecorator = new TxtOutDecorator(_item);
        var obj = _txtOutDecorator.FileImprovement();
        var expectedExtension = ".TXT";

        var isExist = File.Exists(obj.OutFileName);
        var actualExtension = Path.GetExtension(obj.OutFileName);
        
        Assert.True(isExist);
        Assert.Equal(expectedExtension, actualExtension?.ToUpper());
    }

    [Fact]
    public void ZipOutDecoratorTest()
    {
        _zipOutDecorator = new ZipOutDecorator(new TxtOutDecorator(_item).FileImprovement());
        var obj = _zipOutDecorator.FileImprovement();
        var expectedExtension = ".ZIP";

        var isExist = File.Exists(obj.OutFileName);
        var actualExtension = Path.GetExtension(obj.OutFileName);
        
        Assert.True(isExist);
        Assert.Equal(expectedExtension, actualExtension?.ToUpper());
    }
}