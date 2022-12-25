using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.FileTypes;

public interface IFileTypes
{
    void Type(FileItem obj);
}

public class XmlType : IFileTypes
{
    public void Type(FileItem obj)
    {
        
    }
}