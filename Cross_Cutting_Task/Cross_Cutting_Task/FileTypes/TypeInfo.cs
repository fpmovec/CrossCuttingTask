using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.FileTypes;

public class TypeInfo
{
    private static Dictionary<string, IFileTypes> _types;

    public TypeInfo()
    {
        _types = new Dictionary<string, IFileTypes>(3);
        _types.Add("TXT", new TxtType { });
        _types.Add("JSON", new JsonType { });
        _types.Add("XML", new XmlType { });
    }

    public void TypeOut(string type, FileItem item)
    {
        _types.TryGetValue(type.ToUpper(), out IFileTypes? fileTypes);
        fileTypes?.Type(item);
    }
}