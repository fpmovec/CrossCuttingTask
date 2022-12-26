using Cross_Cutting_Task.Decorator;
using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.FileTypes;

public interface IFileTypes
{
    void Type(FileItem obj);
}

public class XmlType : IFileTypes
{
    public void Type(FileItem obj)
        => new XmlInDecorator(obj).FileImprovement();
}
public class JsonType : IFileTypes
{
    public void Type(FileItem obj)
        => new JsonInDecorator(obj).FileImprovement();
}
public class TxtType : IFileTypes
{
    public void Type(FileItem obj)
        => new TxtInDecorator(obj).FileImprovement();
}
