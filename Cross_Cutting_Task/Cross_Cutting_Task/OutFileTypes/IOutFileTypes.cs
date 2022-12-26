using Cross_Cutting_Task.Decorator;
using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.OutFileTypes;

public interface IOutFileTypes
{
    void Type(FileItem obj);
}

public class OutTxtType : IOutFileTypes
{
    public void Type(FileItem obj)
        => new TxtOutDecorator(obj).FileImprovement();
}

public class OutXmlType : IOutFileTypes
{
    public void Type(FileItem obj)
        => new XmlOutDecorator(obj).FileImprovement();
}

public class OutJsonType : IOutFileTypes
{
    public void Type(FileItem obj)
        => new JsonOutDecorator(obj).FileImprovement();
}